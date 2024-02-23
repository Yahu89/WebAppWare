﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppWare.Database;

#nullable disable

namespace WepAppWare.Database.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    partial class WarehouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAppWare.Database.Entities.MovementsListWithCumulativeSumView", b =>
                {
                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Cumulative")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("MoveId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Warehouse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable((string)null);

                    b.ToView("MovementsListWithCumulativeSumView", (string)null);
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ItemCode" }, "IX_Products_ItemCode")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.ProductSummaryModel", b =>
                {
                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("int");

                    b.Property<string>("Warehouse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable((string)null);

                    b.ToView("ProductSummaryModel", (string)null);
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.ProductsAmountListView", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("ProductsAmountListView", (string)null);
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.ProductsFlow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseMovementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_ProductsFlows_ProductId");

                    b.HasIndex(new[] { "SupplierId" }, "IX_ProductsFlows_SupplierId");

                    b.HasIndex(new[] { "WarehouseId" }, "IX_ProductsFlows_WarehouseId");

                    b.HasIndex(new[] { "WarehouseMovementId" }, "IX_ProductsFlows_WarehouseMovementId");

                    b.ToTable("ProductsFlows");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.TwojaTabela", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<int?>("Ilosc")
                        .HasColumnType("int");

                    b.Property<string>("NazwaProduktu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__TwojaTab__3214EC27ECB68040");

                    b.ToTable("TwojaTabela", (string)null);
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.WarehouseMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MovementType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Document" }, "IX_WarehouseMovements_Document")
                        .IsUnique();

                    b.ToTable("WarehouseMovements");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.ProductsFlow", b =>
                {
                    b.HasOne("WebAppWare.Database.Entities.Product", "Product")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("ProductId");

                    b.HasOne("WebAppWare.Database.Entities.Supplier", "Supplier")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("SupplierId");

                    b.HasOne("WebAppWare.Database.Entities.Warehouse", "Warehouse")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("WarehouseId");

                    b.HasOne("WebAppWare.Database.Entities.WarehouseMovement", "WarehouseMovement")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("WarehouseMovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");

                    b.Navigation("Warehouse");

                    b.Navigation("WarehouseMovement");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.Order", b =>
                {
                    b.HasOne("WebAppWare.Database.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.OrderDetails", b =>
                {
                    b.HasOne("WepAppWare.Database.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppWare.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Product", b =>
                {
                    b.Navigation("ProductsFlows");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Supplier", b =>
                {
                    b.Navigation("ProductsFlows");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Warehouse", b =>
                {
                    b.Navigation("ProductsFlows");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.WarehouseMovement", b =>
                {
                    b.Navigation("ProductsFlows");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
