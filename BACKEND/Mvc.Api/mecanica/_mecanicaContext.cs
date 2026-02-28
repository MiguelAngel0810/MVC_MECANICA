using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Mvc.Api.mecanica;

public partial class _mecanicaContext : DbContext
{
    public _mecanicaContext()
    {
    }

    public _mecanicaContext(DbContextOptions<_mecanicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mantenimientos> Mantenimientos { get; set; }

    public virtual DbSet<Vehiculos> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=mecanico_1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Mantenimientos>(entity =>
        {
            entity.HasKey(e => e.MantenimientoId).HasName("PRIMARY");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Mantenimientos).HasConstraintName("mantenimientos_ibfk_1");
        });

        modelBuilder.Entity<Vehiculos>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
