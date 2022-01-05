using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Hra.Domain.Entity;

namespace Hra.Infraestructure.Data
{
    public partial class INPUTContext : DbContext
    {
        public INPUTContext()
        {
        }

        public INPUTContext(DbContextOptions<INPUTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tabla> Tabla { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=connectionDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tabla>(entity =>
            {
                entity.HasKey(e => new { e.TablaId, e.ItemId });

                entity.ToTable("Tabla", "Maestro");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DesCorta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
