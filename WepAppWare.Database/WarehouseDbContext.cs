using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database.Entities;
using WepAppWare.Database.Entities;

namespace WebAppWare.Database;

public partial class WarehouseDbContext : DbContext
{
	public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
		: base(options)
	{
	}

	//public virtual DbSet<MovementsListWithCumulativeSumView> MovementsListWithCumulativeSumViews { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	//public virtual DbSet<ProductSummaryModel> ProductSummaryModels { get; set; }

	//public virtual DbSet<ProductsAmountListView> ProductsAmountListViews { get; set; }

	public virtual DbSet<ProductsFlow> ProductsFlows { get; set; }

	public virtual DbSet<Supplier> Suppliers { get; set; }

	public virtual DbSet<Warehouse> Warehouses { get; set; }

	public virtual DbSet<WarehouseMovement> WarehouseMovements { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Image> Images { get; set; }

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//    => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=WarehouseBase; Trusted_Connection=true; TrustServerCertificate=true");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		////modelBuilder.Entity<ProductFlowModel>(entity =>
		////{
		////    entity
		////        .HasNoKey()
		////        .ToView("MovementsListView");

		////    entity.Property(e => e.ItemCode).HasMaxLength(15);
		////    entity.Property(e => e.Supplier).HasMaxLength(150);
		////    entity.Property(e => e.Warehouse).HasMaxLength(100);
		////});

		//modelBuilder.Entity<MovementsListWithCumulativeSumView>(entity =>
		//{
		//    entity
		//        .HasNoKey()
		//        .ToView("MovementsListWithCumulativeSumView");

		//    entity.Property(e => e.ItemCode).HasMaxLength(15);
		//    entity.Property(e => e.Supplier).HasMaxLength(150);
		//    entity.Property(e => e.Warehouse).HasMaxLength(100);
		//});

		//modelBuilder.Entity<Product>(entity =>
		//{
		//    entity.HasIndex(e => e.ItemCode, "IX_Products_ItemCode").IsUnique();

		//    entity.Property(e => e.Description).HasMaxLength(200);
		//    entity.Property(e => e.ImgUrl).HasMaxLength(400);
		//    entity.Property(e => e.ItemCode).HasMaxLength(15);
		//});

		//modelBuilder.Entity<ProductSummaryModel>(entity =>
		//{
		//    entity
		//        .HasNoKey()
		//        .ToView("ProductSummaryModel");

		//    entity.Property(e => e.ItemCode).HasMaxLength(15);
		//    entity.Property(e => e.Warehouse).HasMaxLength(100);
		//});

		//modelBuilder.Entity<ProductsAmountListView>(entity =>
		//{
		//    entity
		//        .HasNoKey()
		//        .ToView("ProductsAmountListView");
		//});

		//modelBuilder.Entity<ProductsFlow>(entity =>
		//{
		//    entity.HasIndex(e => e.ProductId, "IX_ProductsFlows_ProductId");

		//    entity.HasIndex(e => e.SupplierId, "IX_ProductsFlows_SupplierId");

		//    entity.HasIndex(e => e.WarehouseId, "IX_ProductsFlows_WarehouseId");

		//    entity.HasIndex(e => e.WarehouseMovementId, "IX_ProductsFlows_WarehouseMovementId");

		//    entity.HasOne(d => d.Product).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.ProductId);

		//    entity.HasOne(d => d.Supplier).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.SupplierId);

		//    entity.HasOne(d => d.Warehouse).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.WarehouseId);

		//    entity.HasOne(d => d.WarehouseMovement).WithMany(p => p.ProductsFlows).HasForeignKey(d => d.WarehouseMovementId);
		//});

		//modelBuilder.Entity<Supplier>(entity =>
		//{
		//    entity.Property(e => e.Email).HasMaxLength(150);
		//    entity.Property(e => e.Name).HasMaxLength(150);
		//});

		//modelBuilder.Entity<TwojaTabela>(entity =>
		//{
		//    entity.HasKey(e => e.Id).HasName("PK__TwojaTab__3214EC27ECB68040");

		//    entity.ToTable("TwojaTabela");

		//    entity.Property(e => e.Id)
		//        .ValueGeneratedNever()
		//        .HasColumnName("ID");
		//    entity.Property(e => e.NazwaProduktu).HasMaxLength(255);
		//});

		//modelBuilder.Entity<Warehouse>(entity =>
		//{
		//    entity.Property(e => e.Name).HasMaxLength(100);
		//});

		//modelBuilder.Entity<WarehouseMovement>(entity =>
		//{
		//    entity.HasIndex(e => e.Document, "IX_WarehouseMovements_Document").IsUnique();

		//    entity.Property(e => e.Document).HasMaxLength(100);
		//});

		//modelBuilder.Entity<Order>(e =>
		//{
		//    e.Property(e => e.Document).IsRequired().HasMaxLength(50);
		//    e.Property(e => e.SupplierId).IsRequired();
		//    e.Property(e => e.CreationDate).IsRequired();
		//    e.Property(e => e.Status).IsRequired().HasMaxLength(50);
		//    e.Property(e => e.Remarks).HasMaxLength(300).IsRequired(false);
		//});

		//modelBuilder.Entity<OrderDetails>(e =>
		//{
		//    e.Property(e => e.OrderId).IsRequired();
		//    e.Property(e => e.ProductId).IsRequired();
		//    e.Property(e => e.Quantity).IsRequired();
		//    e.HasOne(e => e.Order).WithMany(e => e.OrderDetails).HasForeignKey(e => e.OrderId);
		//});

		//OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
