using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Linq;

namespace ParkDataLayer.Mappers
{
    public class MapHuurcontracten
    {
        internal static HuurcontractEF DOMAIN_TO_EF(Huurcontract contract, ParkbeheerContext ctx)
        {
            try
            {
                HuisEF huisef = ctx.Huizen.Find(contract.Huis.Id);

                if (huisef is null)
                {
                    huisef = MapHuis.DOMAIN_TO_EF(contract.Huis, ctx);
                }

                HuurderEF huurderef = ctx.Huurders.Find(contract.Huurder.Id);

                if (huurderef is null)
                {
                    huurderef = MapHuurder.DOMAIN_TO_EF(contract.Huurder);
                }

                HuurperiodeEF huurperiodeef = ctx.Periodes.SingleOrDefault(x => x.contractID == contract.Id); //huurperiode geen ID zoals huurperodeEF dus deze manier


                if (huurperiodeef is null)
                {
                    huurperiodeef = MapHuurperiode.DOMAIN_TO_EF(contract.Huurperiode);
                }

                if(huurperiodeef.AantalDagenVerhuurd != contract.Huurperiode.Aantaldagen || huurperiodeef.Start != contract.Huurperiode.StartDatum)
                {
                    huurperiodeef.Start = contract.Huurperiode.StartDatum;
                    huurperiodeef.AantalDagenVerhuurd = contract.Huurperiode.Aantaldagen;
                    huurperiodeef.End = contract.Huurperiode.EindDatum;
                }

                return new HuurcontractEF()
                {
                    ID = contract.Id,
                    Huis = huisef,
                    Huurder = huurderef,
                    Huurperiode = huurperiodeef
                };
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }

        internal static Huurcontract EF_TO_DOMAIN(HuurcontractEF hcef, Huis huis)
        {
            try
            {
                Huis mappedHuis = huis;
                if(huis is null)
                {
                    mappedHuis = MapHuis.EF_TO_DOMAIN(hcef.Huis);
                }


                return new Huurcontract(hcef.ID, MapHuurperiode.EF_TO_DOMAIN(hcef.Huurperiode), MapHuurder.EF_TO_DOMAIN(hcef.Huurder), mappedHuis);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }

    }
}
