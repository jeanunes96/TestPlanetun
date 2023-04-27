using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestPlanetun.Models;

namespace TestPlanetun.Context
{
    public partial class testPlanetunContext : DbContext
    {
        public testPlanetunContext()
        {
        }

        public testPlanetunContext(DbContextOptions<testPlanetunContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inspection> Inspection { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inspection>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK__Inspecti__2D971CAC1ACB6D37");

                entity.Property(e => e.BrokerCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InspectionNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
