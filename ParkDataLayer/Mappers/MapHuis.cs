using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Mappers
{
    public class MapHuis
    {
        internal static Huis EF_TO_DOMAIN(HuisEF huisEF)
        {
            try
            {
                if(huisEF is null)
                {
                    throw new MapperException("House with this id does not exist.");
                }

                Park park = MapPark.EF_TO_DOMAIN_WITHOUT_HOUSES(huisEF.Park);
                Huis huis = new(huisEF.ID, huisEF.Street, huisEF.Nr, huisEF.IsRentable, park);

                Dictionary<Huurder, List<Huurcontract>> contracten = new();

                foreach (HuurcontractEF hcef in huisEF.Contracts)
                {
                    Huurder huurder = MapHuurder.EF_TO_DOMAIN(hcef.Huurder);

                    if (!contracten.ContainsKey(huurder))
                    {
                        contracten[huurder] = new List<Huurcontract>();
                    }

                    contracten[huurder].Add(MapHuurcontracten.EF_TO_DOMAIN(hcef, huis));
                }

                return new Huis(huisEF.ID, huisEF.Street, huisEF.Nr, huisEF.IsRentable, park, contracten);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }

        internal static HuisEF DOMAIN_TO_EF(Huis h, ParkbeheerContext ctx)
        {
            try
            {
                ParkEF parkEF = ctx.Parken.Find(h.Park.Id);

                if (parkEF is null)
                {
                    parkEF = MapPark.DOMAIN_TO_EF(h.Park);
                }

                if(h is null)
                {
                    throw new MapperException("this house does not exist.");
                }

                return new HuisEF()
                {
                    ID = h.Id,
                    Nr = h.Nr,
                    Street = h.Straat,
                    IsRentable = h.Actief,
                    ParkID = parkEF.ID,
                    Park = parkEF,
                };
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }


    }
}
