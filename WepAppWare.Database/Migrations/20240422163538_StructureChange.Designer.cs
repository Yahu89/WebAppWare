﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppWare.Database;

#nullable disable

namespace WepAppWare.Database.Migrations
{
    [DbContext(typeof(WarehouseBaseContext))]
    [Migration("20240422163538_StructureChange")]
    partial class StructureChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAppWare.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Products");
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

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("WarehouseId");

                    b.HasIndex("WarehouseMovementId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
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
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovementType")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseMovements");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AbsolutePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.OrderItem", b =>
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

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.Product", b =>
                {
                    b.HasOne("WepAppWare.Database.Entities.Image", "Image")
                        .WithMany("Products")
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.ProductsFlow", b =>
                {
                    b.HasOne("WebAppWare.Database.Entities.Product", "Product")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("ProductId");

                    b.HasOne("WebAppWare.Database.Entities.Supplier", "Supplier")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("SupplierId");

                    b.HasOne("WebAppWare.Database.Entities.Warehouse", null)
                        .WithMany("ProductsFlows")
                        .HasForeignKey("WarehouseId");

                    b.HasOne("WebAppWare.Database.Entities.WarehouseMovement", "WarehouseMovement")
                        .WithMany("ProductsFlows")
                        .HasForeignKey("WarehouseMovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");

                    b.Navigation("WarehouseMovement");
                });

            modelBuilder.Entity("WebAppWare.Database.Entities.WarehouseMovement", b =>
                {
                    b.HasOne("WebAppWare.Database.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
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

            modelBuilder.Entity("WepAppWare.Database.Entities.OrderItem", b =>
                {
                    b.HasOne("WepAppWare.Database.Entities.Order", "Order")
                        .WithMany("Items")
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

            modelBuilder.Entity("WepAppWare.Database.Entities.Image", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WepAppWare.Database.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
