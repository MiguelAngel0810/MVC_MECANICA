using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
namespace DbModel.mecanica
{
    public partial class _mecanicaContext : DbContext
    {
        public _mecanicaContext()
        {
        }

        public _mecanicaContext(DbContextOptions<_mecanicaContext> options)
            : base(options)
        {
        }

        // Tablas principales
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=demoDb",
                    ServerVersion.Parse("8.0.45-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            // Configuración Vehiculos
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.VehiculoId).HasName("PRIMARY");

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Anio)
                    .IsRequired();
            });

            // Configuración Mantenimientos
            modelBuilder.Entity<Mantenimiento>(entity =>
            {
                entity.HasKey(e => e.MantenimientoId).HasName("PRIMARY");

                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.TipoServicio)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Detalle);

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mantenimiento_vehiculo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
