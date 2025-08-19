using cazuela_chapina_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cazuela_chapina_api.Context
{
    public class AppDbC: DbContext
    {
        internal IEnumerable<object> supplier;

        public AppDbC(DbContextOptions<AppDbC> options):base(options) 
        {
            
        }

        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<UnitMeasurement> unit_measurement { get; set; }
        public DbSet<Inventory> inventory { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Combo> combo { get; set; }
        public DbSet<SellDetails> sellDetails { get; set; }
        public DbSet<Sell> sell { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Supplier)
                .WithMany(s => s.Inventories)
                .HasForeignKey(i => i.SupplierId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.UnitMeasurement)
                .WithMany(u => u.Inventories)
                .HasForeignKey(i => i.UnitMeasurementId);
            modelBuilder.Entity<Combo>()
                .HasOne(c => c.Masa)
                .WithMany()
                .HasForeignKey(c => c.IdMasa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combo>()
                .HasOne(c => c.Relleno)
                .WithMany()
                .HasForeignKey(c => c.IdRelleno)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combo>()
                .HasOne(c => c.Envoltura)
                .WithMany()
                .HasForeignKey(c => c.IdEnvoltura)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combo>()
                .HasOne(c => c.Picante)
                .WithMany()
                .HasForeignKey(c => c.IdPicante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combo>()
                .HasOne(c => c.Bebida)
                .WithMany()
                .HasForeignKey(c => c.IdBebida)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Masa)
                .WithMany()
                .HasForeignKey(s => s.MasaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Relleno)
                .WithMany()
                .HasForeignKey(s => s.RellenoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Envoltura)
                .WithMany()
                .HasForeignKey(s => s.EnvolturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Picante)
                .WithMany()
                .HasForeignKey(s => s.PicanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Bebida)
                .WithMany()
                .HasForeignKey(s => s.BebidaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Endulzar)
                .WithMany()
                .HasForeignKey(s => s.EndulzarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellDetails>()
                .HasOne(s => s.Combo)
                .WithMany()
                .HasForeignKey(s => s.ComboId)
                .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);
        }

    }
}
