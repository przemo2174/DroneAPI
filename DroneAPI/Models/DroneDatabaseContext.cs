using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DroneAPI.Models
{
    public partial class DroneDatabaseContext : DbContext
    {
        public DroneDatabaseContext()
        {
        }

        public DroneDatabaseContext(DbContextOptions<DroneDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=droneserver.database.windows.net;Database=DroneDatabaseDeveloper;User Id=drone;Password=Droniada2018;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Ankieta2015).HasMaxLength(255);

                entity.Property(e => e.Ankieta2016).HasMaxLength(255);

                entity.Property(e => e.Ankieta2017).HasMaxLength(255);

                entity.Property(e => e.Base).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Industry).HasMaxLength(255);

                entity.Property(e => e.Krs)
                    .HasColumnName("KRS")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nip)
                    .HasColumnName("NIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.Pkd)
                    .HasColumnName("PKD")
                    .HasMaxLength(255);

                entity.Property(e => e.Pkdcode)
                    .HasColumnName("PKDCode")
                    .HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.Street).HasMaxLength(255);

                entity.Property(e => e.Tag2015).HasMaxLength(255);

                entity.Property(e => e.Tag2016).HasMaxLength(255);

                entity.Property(e => e.Tag2017).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");

                entity.Property(e => e.Voivodeship).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(255);
            });
        }
    }
}
