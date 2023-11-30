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
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private readonly ParkbeheerContext _ctx;

        public HuurderRepositoryEF(string connstring)
        {
            _ctx = new(connstring);
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }

        public Huurder GeefHuurder(int id)
        {
            try
            {
                HuurderEF huurderEF = _ctx.Huurders.Include(h => h.Contactgegevens).AsNoTracking().FirstOrDefault(h => h.ID == id);

                Huurder huurder = MapHuurder.EF_TO_DOMAIN(huurderEF);

                return huurder;
            }
            catch (Exception)
            {
                throw new RepositoryException($"Could not find a huurder with id: {id}.");
            }
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            try
            {
                List<HuurderEF> HuurdersEF = _ctx.Huurders.AsNoTracking().Include(h => h.Contactgegevens).Where(h => h.Name == naam).ToList();

                List<Huurder> Huurders = new();

                foreach (HuurderEF hef in HuurdersEF)
                {
                    Huurder huurder = MapHuurder.EF_TO_DOMAIN(hef);
                    Huurders.Add(huurder);
                }

                return Huurders;
            }
            catch (Exception)
            {
                throw new RepositoryException("Error when retrieving huurders by name.");
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            try
            {
                return _ctx.Huurders.AsNoTracking()
                               .Include(h => h.Contactgegevens)
                               .Any(x => x.Name == naam && contact.Email == x.Contactgegevens.Email
                                                        && contact.Phone == x.Contactgegevens.Phone
                                                        && contact.Adres == x.Contactgegevens.Address);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public bool HeeftHuurder(int id)
        {
            try
            {
                return _ctx.Huurders.Any(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public void UpdateHuurder(Huurder huurder)
        {
            try
            {
                HuurderEF huurderEF = MapHuurder.DOMAIN_TO_EF(huurder);

                _ctx.Huurders.Update(huurderEF);
                SaveAndClear();
            }
            catch (Exception)
            {
                throw new RepositoryException("Error when updating huurder.");
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            try
            {
                HuurderEF huurderEF = MapHuurder.DOMAIN_TO_EF(h);

                _ctx.Huurders.Add(huurderEF);
                SaveAndClear();

                h.ZetId(huurderEF.ID);
                return h;
            }
            catch (Exception)
            {
                throw new RepositoryException("Error when adding huurder.");
            }
        }

    }
}
