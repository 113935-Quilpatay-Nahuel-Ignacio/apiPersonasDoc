using System;
using System.Collections.Generic;
using ApiPersonasDoc.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonasDoc.Data;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=connectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("persona_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido).HasColumnName("apellido");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.TipoDocumentoId).HasColumnName("tipoDocumentoId");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Personas)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipoDocumento");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipoDocumento_pkey");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
