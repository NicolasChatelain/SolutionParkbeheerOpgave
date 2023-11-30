using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
        private ParkbeheerContext _ctx;

        public ContractenRepositoryEF(string connstring)
        {
            _ctx = new(connstring);
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }

        public void AnnuleerContract(Huurcontract contract)
        {
            try
            {
                _ctx.Contracten.Remove(new HuurcontractEF() { ID = contract.Id });
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
            
        }

        public Huurcontract GeefContract(string id)
        {
            try
            {
                HuurcontractEF hcef = _ctx.Contracten.Include(c => c.Huis)
                                                     .ThenInclude(h => h.Park)
                                                     .Include(c => c.Huurperiode)
                                                     .Include(c => c.Huurder)
                                                     .ThenInclude(h => h.Contactgegevens)
                                                     .AsNoTracking()
                                                     .FirstOrDefault(c => c.ID == id);

                if (hcef is null)
                {
                    throw new RepositoryException($"No contract found with id {id}");
                }

                Huis huis = MapHuis.EF_TO_DOMAIN(hcef.Huis);
                Huurcontract contract = MapHuurcontracten.EF_TO_DOMAIN(hcef, huis);
                return contract;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {

            try
            {
                IQueryable<HuurcontractEF> query = _ctx.Contracten
                                                             .Include(c => c.Huurder)
                                                                 .ThenInclude(h => h.Contactgegevens)
                                                             .Include(c => c.Huurperiode)
                                                             .Include(c => c.Huis)
                                                                 .ThenInclude(h => h.Park);

                query = dtEinde is null
                    ? query.Where(x => x.Huurperiode.Start >= dtBegin)
                    : query.Where(x => x.Huurperiode.Start >= dtBegin && x.Huurperiode.Start < dtEinde);


                return query.Select(x => MapHuurcontracten.EF_TO_DOMAIN(x, MapHuis.EF_TO_DOMAIN(x.Huis))).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }

        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid) //check voor zelfde huurder op zelfde huis en datum EN check op op zelfde huurder in 
        {                                                                         //zelfde huis dat een contract wilt maken voor de einddatum van dat huis.

            try
            {
                return _ctx.Contracten.Include(c => c.Huurperiode)
                                      .Include(c => c.Huis)
                                      .Include(c => c.Huurder)
                                      .Any(c =>
                                          c.Huurperiode.Start == startDatum &&
                                          c.Huurder.ID == huurderid &&
                                          c.Huis.ID == huisid ||
                                          c.Huurperiode.End < startDatum &&
                                          c.Huurder.ID == huurderid &&
                                          c.Huis.ID == huisid);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public bool HeeftContract(string id)
        {
            try
            {
                return _ctx.Contracten.Any(c => c.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public void UpdateContract(Huurcontract contract)
        {
            try
            {
                HuurcontractEF hcef = MapHuurcontracten.DOMAIN_TO_EF(contract, _ctx);
                _ctx.Contracten.Update(hcef);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            try
            {
                HuurcontractEF hcef = MapHuurcontracten.DOMAIN_TO_EF(contract, _ctx);
                _ctx.Contracten.Add(hcef);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }

        }
    }
}
