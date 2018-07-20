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
                optionsBuilder.UseSqlServer("Server=droneserver.database.windows.net;Database=DroneDatabase;User Id=drone;Password=Droniada2018;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.AktualneNaDzien).HasColumnType("date");

                entity.Property(e => e.Ankieta2015).HasMaxLength(255);

                entity.Property(e => e.Ankieta2016).HasMaxLength(255);

                entity.Property(e => e.Ankieta2017).HasMaxLength(255);

                entity.Property(e => e.Baza).HasMaxLength(255);

                entity.Property(e => e.Branza).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Kod).HasMaxLength(10);

                entity.Property(e => e.KodPkd)
                    .HasColumnName("KodPKD")
                    .HasMaxLength(255);

                entity.Property(e => e.Krs)
                    .HasColumnName("KRS")
                    .HasMaxLength(20);

                entity.Property(e => e.Miasto).HasMaxLength(255);

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nip)
                    .HasColumnName("NIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Pkd)
                    .HasColumnName("PKD")
                    .HasMaxLength(255);

                entity.Property(e => e.Stanowisko).HasMaxLength(255);

                entity.Property(e => e.TabelaWraporcie).HasColumnName("TabelaWRaporcie");

                entity.Property(e => e.Tag2015).HasMaxLength(255);

                entity.Property(e => e.Tag2016).HasMaxLength(255);

                entity.Property(e => e.Tag2017).HasMaxLength(255);

                entity.Property(e => e.Telefon).HasMaxLength(255);

                entity.Property(e => e.Ulica).HasMaxLength(255);

                entity.Property(e => e.Witryna).HasMaxLength(255);

                entity.Property(e => e.Wojewodztwo).HasMaxLength(50);
            });
        }
    }
}
