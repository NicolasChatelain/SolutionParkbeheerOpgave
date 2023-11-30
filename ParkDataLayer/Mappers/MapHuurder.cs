using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;


namespace ParkDataLayer.Mappers
{
    public class MapHuurder
    {
        internal static HuurderEF DOMAIN_TO_EF(Huurder huurder)
        {
            try
            {
                ContactgegevensEF contact = MapGegevens.DOMAIN_TO_EF(huurder.Contactgegevens);

                return new HuurderEF()
                {
                    ID = huurder.Id,
                    Name = huurder.Naam,
                    Contactgegevens = contact
                };

            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }

        }

        internal static Huurder EF_TO_DOMAIN(HuurderEF huurderEF)
        {
            try
            {
                if (huurderEF is not null)
                {
                    return new Huurder(huurderEF.ID, huurderEF.Name, MapGegevens.EF_TO_DOMAIN(huurderEF.Contactgegevens));
                }
                else
                {
                    throw new MapperException("No huurder with this id");
                }
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }
    }
}
