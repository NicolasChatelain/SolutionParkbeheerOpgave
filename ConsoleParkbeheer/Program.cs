using ParkDataLayer;
using System.Configuration;

namespace ConsoleParkbeheer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["modellingDB"].ConnectionString;
            ParkbeheerContext ctx = new(connectionstring);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

        }
    }
}