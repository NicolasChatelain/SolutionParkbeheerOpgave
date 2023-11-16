using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer
{
    internal class ModelCreator
    {
        private ModelBuilder modelbuilder;
        public ModelCreator(ModelBuilder mb)
        {
            modelbuilder = mb;
        }


        internal void HuisEF()
        {
            var x = modelbuilder.Entity<HuisEF>();

            x.HasKey(pk => pk.ID);
            x.Property(pk => pk.ID).ValueGeneratedOnAdd().UseIdentityColumn(seed: 1, increment: 1);
            x.Property(h => h.Street).HasColumnType("varchar(250)").IsRequired(false);
            x.Property(h => h.Nr).HasColumnType("int").IsRequired();
            x.Property(h => h.IsRentable).HasColumnType("bit").IsRequired();
            x.HasOne(h => h.ParkEF).WithMany().IsRequired();
        }

        internal void HuurderEF()
        {
            var x = modelbuilder.Entity<HuurderEF>();

            x.HasKey(pk => pk.ID);
            x.Property(pk => pk.ID).ValueGeneratedOnAdd().UseIdentityColumn(seed: 1, increment: 1);
            x.Property(h => h.Name).HasColumnType("varchar(100)").IsRequired();
            x.Property(h => h.Phone).HasColumnType("varchar(100)");
            x.Property(h => h.Email).HasColumnType("varchar(100)");
            x.Property(h => h.Address).HasColumnType("varchar(100)");

        }    
        
        internal void ParkEF()
        {
            var x = modelbuilder.Entity<ParkEF>();

            x.ToTable("Parken");

            x.HasKey(pk => pk.ID);
            x.Property(pk => pk.ID).HasColumnType("varchar(20)");
            x.Property(p => p.Name).HasColumnType("varchar(250)").IsRequired();
            x.Property(p => p.Location).HasColumnType("varchar(500)").IsRequired(false);

        }

        internal void HuurcontractEF()
        {
            var x = modelbuilder.Entity<HuurcontractEF>();

            x.HasKey(pk => pk.ID);
            x.Property(pk => pk.ID).HasColumnType("varchar(25)");
            x.Property(hc => hc.Start).HasColumnType("datetime2").IsRequired();
            x.Property(hc => hc.End).HasColumnType("datetime2").IsRequired();
            x.Property(hc => hc.AantalDagenVerhuurd).HasColumnType("int").IsRequired();


        }

    }
}
