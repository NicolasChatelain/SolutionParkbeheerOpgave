using Microsoft.EntityFrameworkCore;
using Parkbeheer.Utils;
using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Model;
using ParkDataLayer;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace ConsoleParkbeheer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string connectionstring = ConfigurationManager.ConnectionStrings["modellingDB"].ConnectionString;
            //ParkbeheerContext ctx = new(connectionstring);

            //ctx.Database.EnsureDeleted();
            //ctx.Database.EnsureCreated();


            BeheerContracten ContractenManager = new(RepositoryFactory.ContractRepo);
            BeheerHuizen HuizenManager = new(RepositoryFactory.HuizenRepo);
            BeheerHuurders HuurderManager = new(RepositoryFactory.HuurderRepo);


            try
            {

                //UserStorySwitch uss = new(ContractenManager, HuizenManager, HuurderManager);
                //uss.Run();


                Huurder huurder = HuurderManager.GeefHuurder(1);
                huurder.Contactgegevens.Adres = "Zarlardingestraat 16";

                huurder.ZetContactgegevens(huurder.Contactgegevens);

                HuurderManager.UpdateHuurder(huurder);



            }
            catch (Exception ex)
            {
                PrintError(ex);
            }


        }

        static void PrintError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: ");
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Green;
        }

    }
}