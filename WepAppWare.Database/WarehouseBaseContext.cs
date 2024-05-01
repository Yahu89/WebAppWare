using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database.Entities;

namespace WebAppWare.Database;

public partial class WarehouseBaseContext : DbContext
{
    public WarehouseBaseContext()
    {
    }

    public WarehouseBaseContext(DbContextOptions<WarehouseBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsFlow> ProductsFlows { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseMovement> WarehouseMovements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data source=localhost\\SQLEXPRESS; Trusted_Connection=true; Initial Catalog=WarehouseBase; TrustServerCertificate=true; Integrated Security= true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.SupplierId, "IX_Orders_SupplierId");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderItems_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ImageId, "IX_Products_ImageId");

            entity.HasOne(d => d.Image).WithMany(p => p.Products).HasForeignKey(d => d.ImageId);
        });

        modelBuilder.Entity<ProductsFlow>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductsFlows_ProductId");

            entity.HasIndex(e => e.SupplierId, "IX_ProductsFlows_SupplierId");

            entity.HasIndex(e => e.WarehouseMovementId, "IX_ProductsFlows_WarehouseMovementId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.WarehouseMovement).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.WarehouseMovementId);
        });

        modelBuilder.Entity<WarehouseMovement>(entity =>
        {
            entity.HasIndex(e => e.WarehouseId, "IX_WarehouseMovements_WarehouseId");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseMovements).HasForeignKey(d => d.WarehouseId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
