﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppWare.Infrastructure.BaseContext;

public partial class WarehouseBaseContext : DbContext
{
    public WarehouseBaseContext()
    {
    }

    public WarehouseBaseContext(DbContextOptions<WarehouseBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProductFlowModel> ProductFlowModels { get; set; }

    public virtual DbSet<MovementsListWithCumulativeSumView> MovementsListWithCumulativeSumViews { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSummaryModel> ProductSummaryModels { get; set; }

    public virtual DbSet<ProductsAmountListView> ProductsAmountListViews { get; set; }

    public virtual DbSet<ProductsFlow> ProductsFlows { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TwojaTabela> TwojaTabelas { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseMovement> WarehouseMovements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=WarehouseBase; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductFlowModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MovementsListView");

            entity.Property(e => e.ItemCode).HasMaxLength(15);
            entity.Property(e => e.Supplier).HasMaxLength(150);
            entity.Property(e => e.Warehouse).HasMaxLength(100);
        });

        modelBuilder.Entity<MovementsListWithCumulativeSumView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MovementsListWithCumulativeSumView");

            entity.Property(e => e.ItemCode).HasMaxLength(15);
            entity.Property(e => e.Supplier).HasMaxLength(150);
            entity.Property(e => e.Warehouse).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ItemCode, "IX_Products_ItemCode").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ImgUrl).HasMaxLength(400);
            entity.Property(e => e.ItemCode).HasMaxLength(15);
        });

        modelBuilder.Entity<ProductSummaryModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductSummaryModel");

            entity.Property(e => e.ItemCode).HasMaxLength(15);
            entity.Property(e => e.Warehouse).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductsAmountListView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductsAmountListView");
        });

        modelBuilder.Entity<ProductsFlow>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductsFlows_ProductId");

            entity.HasIndex(e => e.SupplierId, "IX_ProductsFlows_SupplierId");

            entity.HasIndex(e => e.WarehouseId, "IX_ProductsFlows_WarehouseId");

            entity.HasIndex(e => e.WarehouseMovementId, "IX_ProductsFlows_WarehouseMovementId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.WarehouseId);

            entity.HasOne(d => d.WarehouseMovement).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.WarehouseMovementId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<TwojaTabela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TwojaTab__3214EC27ECB68040");

            entity.ToTable("TwojaTabela");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NazwaProduktu).HasMaxLength(255);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<WarehouseMovement>(entity =>
        {
            entity.HasIndex(e => e.Document, "IX_WarehouseMovements_Document").IsUnique();

            entity.Property(e => e.Document).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
