using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;

namespace ParkDataLayer.Mappers
{
    public class MapPark
    {
        internal static Park EF_TO_DOMAIN_WITHOUT_HOUSES(ParkEF parkEF)
        {
            try
            {
                return new Park(parkEF.ID, parkEF.Name, parkEF.Location);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }

        internal static ParkEF DOMAIN_TO_EF(Park park)
        {
            try
            {
                if(park is null)
                {
                    throw new MapperException("this park does not exist");
                }

                return new ParkEF(park.Id, park.Naam, park.Locatie);
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message);
            }
        }
    }
}
