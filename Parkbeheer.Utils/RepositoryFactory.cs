using ParkBusinessLayer.Interfaces;
using ParkDataLayer.Repositories;
using System.Configuration;

namespace Parkbeheer.Utils
{
    public static class RepositoryFactory
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["modellingDB"].ConnectionString;

        public static IContractenRepository ContractRepo
        {
            get { return new ContractenRepositoryEF(_connectionString); }
        }

        public static IHuizenRepository HuizenRepo
        {
            get { return new HuizenRepositoryEF(_connectionString); }
        }

        public static IHuurderRepository HuurderRepo
        {
            get { return new HuurderRepositoryEF(_connectionString); }
        }
    }
}