using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    internal class MapHuurperiode
    {
        internal static Huurperiode EF_TO_DOMAIN(HuurperiodeEF huurperiode)
        {
            return new Huurperiode(huurperiode.Start, huurperiode.AantalDagenVerhuurd);
        }

        internal static HuurperiodeEF DOMAIN_TO_EF(Huurperiode huurperiode)
        {
            return new HuurperiodeEF()
            {
                Start = huurperiode.StartDatum,
                End = huurperiode.EindDatum,
                AantalDagenVerhuurd = huurperiode.Aantaldagen,
            };
        }
    }
}
