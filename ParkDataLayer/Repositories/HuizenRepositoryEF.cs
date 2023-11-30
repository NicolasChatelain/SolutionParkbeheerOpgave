using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private readonly ParkbeheerContext _ctx;

        public HuizenRepositoryEF(string connstring)
        {
            _ctx = new(connstring);
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }

        public Huis GeefHuis(int id)
        {
            try
            {
                HuisEF huisEF = _ctx.Huizen.Include(huis => huis.Park)
                                           .Include(huis => huis.Contracts)
                                               .ThenInclude(c => c.Huurder)
                                               .ThenInclude(h => h.Contactgegevens)
                                           .Include(huis => huis.Contracts)
                                               .ThenInclude(c => c.Huurperiode)
                                           .AsNoTracking()
                                           .FirstOrDefault(x => x.ID == id);

                Huis huis = MapHuis.EF_TO_DOMAIN(huisEF);

                return huis;
            }
            catch (Exception)
            {
                throw new RepositoryException($"Could not find a house with id: {id}.");
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            try
            {
                return _ctx.Huizen.AsNoTracking().Include(huis => huis.Park).Any(huis => huis.Park.ID == park.Id && huis.Street == straat && huis.Nr == nummer);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public bool HeeftHuis(int id) //asnotracking maybe not necessary?
        {
            try
            {
                return _ctx.Huizen.AsNoTracking().Any(huis => huis.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public void UpdateHuis(Huis huis)
        {
            try
            {
                HuisEF huisEF = MapHuis.DOMAIN_TO_EF(huis, _ctx);
                huisEF.Contracts = _ctx.Contracten.Where(x => x.Huis.ID == huisEF.ID).ToList();

                if (!huisEF.IsRentable)
                {
                    _ctx.Contracten.RemoveRange(huisEF.Contracts);
                }

                _ctx.Huizen.Update(huisEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public Huis VoegHuisToe(Huis h) //huis return or void return?????
        {
            try
            {
                HuisEF huisEF = MapHuis.DOMAIN_TO_EF(h, _ctx);

                _ctx.Huizen.Add(huisEF);
                SaveAndClear();

                h.ZetId(huisEF.ID);
                return h;
            }
            catch (Exception)
            {
                throw new RepositoryException("Failed to add house.");
            }
        }
    }
}
