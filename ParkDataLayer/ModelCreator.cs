using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;

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
        }

        internal void HuurderEF()
        {
            var x = modelbuilder.Entity<HuurderEF>();

            x.HasKey(pk => pk.ID);
            x.Property(pk => pk.ID).ValueGeneratedOnAdd().UseIdentityColumn(seed: 1, increment: 1);
            x.Property(h => h.Name).HasColumnType("varchar(100)").IsRequired();
            x.HasOne(h => h.Contactgegevens);
        }    
        
        internal void ParkEF()
        {
            var x = modelbuilder.Entity<ParkEF>();

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

            x.HasOne(hc => hc.Huurperiode)
             .WithOne()
             .HasForeignKey<HuurperiodeEF>(hp => hp.contractID).OnDelete(DeleteBehavior.Cascade);
        }

        internal void HuurPeriodeEF()
        {
            var x = modelbuilder.Entity<HuurperiodeEF>();

            x.Property(hc => hc.Start).HasColumnType("datetime2").IsRequired();
            x.Property(hc => hc.End).HasColumnType("datetime2").IsRequired();
            x.Property(hc => hc.AantalDagenVerhuurd).HasColumnType("int").IsRequired();
        }

        internal void ContactGegevensEF()
        {
            var x = modelbuilder.Entity<ContactgegevensEF>();

            x.HasKey(pk => pk.ID);
            x.Property(h => h.Phone).HasColumnType("varchar(100)");
            x.Property(h => h.Email).HasColumnType("varchar(100)");
            x.Property(h => h.Address).HasColumnType("varchar(100)");
        }

    }
}
