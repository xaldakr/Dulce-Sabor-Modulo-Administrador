using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dulce_Sabor_Modulo_Administrador.Models;

public partial class ParcialContext : DbContext
{
    public ParcialContext()
    {
    }

    public ParcialContext(DbContextOptions<ParcialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CQ730LI; Database=parcial; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK__DetalleV__C08768CF31F041BF");

            entity.Property(e => e.IdDetallePedido).HasColumnName("id_detalle_pedido");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__id_ve__3C69FB99");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK__Detalles__C08768CF852DDDFD");

            entity.Property(e => e.IdDetallePedido).HasColumnName("id_detalle_pedido");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetallesP__id_ve__398D8EEE");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ventas__3213E83FD844340F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
