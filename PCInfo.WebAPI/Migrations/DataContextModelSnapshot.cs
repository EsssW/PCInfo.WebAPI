﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PCInfo.WebAPI.Data;

#nullable disable

namespace PCInfo.WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PCInfo.WebAPI.Data.MyDriveInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvailableFreeSpace")
                        .HasColumnType("text");

                    b.Property<string>("DriveFormat")
                        .HasColumnType("text");

                    b.Property<int?>("MyPCInfoId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("TotalSize")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MyPCInfoId");

                    b.ToTable("myDriveInfos");
                });

            modelBuilder.Entity("PCInfo.WebAPI.Data.MyPCInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CPUKernelCount")
                        .HasColumnType("integer");

                    b.Property<string>("CPUManufacturerer")
                        .HasColumnType("text");

                    b.Property<string>("CPUModel")
                        .HasColumnType("text");

                    b.Property<string>("CPUNMaxClockSpeed")
                        .HasColumnType("text");

                    b.Property<string>("CPUName")
                        .HasColumnType("text");

                    b.Property<int>("DriveCount")
                        .HasColumnType("integer");

                    b.Property<string>("HDDName")
                        .HasColumnType("text");

                    b.Property<string>("HDDSize")
                        .HasColumnType("text");

                    b.Property<string>("HResol")
                        .HasColumnType("text");

                    b.Property<string>("OSVersion")
                        .HasColumnType("text");

                    b.Property<string>("PCIPV4")
                        .HasColumnType("text");

                    b.Property<string>("PCName")
                        .HasColumnType("text");

                    b.Property<int>("RAMCount")
                        .HasColumnType("integer");

                    b.Property<int>("ScreenCount")
                        .HasColumnType("integer");

                    b.Property<string>("SystemBitRate")
                        .HasColumnType("text");

                    b.Property<string>("SystemCatalogPath")
                        .HasColumnType("text");

                    b.Property<string>("TotalRAM")
                        .HasColumnType("text");

                    b.Property<string>("VideoCardMemoryAmount")
                        .HasColumnType("text");

                    b.Property<string>("VideoCardName")
                        .HasColumnType("text");

                    b.Property<string>("WResol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("pcInfos");
                });

            modelBuilder.Entity("PCInfo.WebAPI.Data.MyDriveInfo", b =>
                {
                    b.HasOne("PCInfo.WebAPI.Data.MyPCInfo", "MyPCInfo")
                        .WithMany("MyDriveInfos")
                        .HasForeignKey("MyPCInfoId");

                    b.Navigation("MyPCInfo");
                });

            modelBuilder.Entity("PCInfo.WebAPI.Data.MyPCInfo", b =>
                {
                    b.Navigation("MyDriveInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
