using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;

namespace ParkDataLayer.Mappers
{
    public class MapGegevens
    {
        internal static ContactgegevensEF DOMAIN_TO_EF(Contactgegevens cg)
        {
            try
            {
                return new ContactgegevensEF(cg.Email, cg.Phone, cg.Adres);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }

        internal static Contactgegevens EF_TO_DOMAIN(ContactgegevensEF cg)
        {
            try
            {
                if(cg is null)
                {
                    throw new MapperException("these contacts dont't exist.");
                }
                return new Contactgegevens(cg.Email, cg.Phone, cg.Address);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }
    }
}
