using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer
{
    public class ParkbeheerContext : DbContext
    {

        private readonly string connection;
        public ParkbeheerContext(string connection)
        {
            this.connection = connection;
        }



        public DbSet<HuisEF> Huizen { get; set; }
        public DbSet<HuurderEF> Huurders { get; set; }
        public DbSet<HuurcontractEF> Contracten {  get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection).LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreator CreateModel = new(modelBuilder);

            CreateModel.HuisEF();
            CreateModel.ParkEF();
            CreateModel.HuurderEF();
            CreateModel.HuurcontractEF();
        }


    }
}
