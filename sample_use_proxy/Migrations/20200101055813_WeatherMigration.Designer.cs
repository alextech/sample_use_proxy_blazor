﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sample_use_proxy.Data;

namespace sample_use_proxy.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20200101055813_WeatherMigration")]
    partial class WeatherMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("sample_use_proxy.Data.Description", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Description");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Summary = "Freezing"
                        },
                        new
                        {
                            Id = 2,
                            Summary = "Bracing"
                        },
                        new
                        {
                            Id = 3,
                            Summary = "Chilly"
                        },
                        new
                        {
                            Id = 4,
                            Summary = "Cool"
                        },
                        new
                        {
                            Id = 5,
                            Summary = "Mild"
                        },
                        new
                        {
                            Id = 6,
                            Summary = "Warm"
                        },
                        new
                        {
                            Id = 7,
                            Summary = "Balmy"
                        },
                        new
                        {
                            Id = 8,
                            Summary = "Hot"
                        },
                        new
                        {
                            Id = 9,
                            Summary = "Sweltering"
                        },
                        new
                        {
                            Id = 10,
                            Summary = "Scorching"
                        });
                });

            modelBuilder.Entity("sample_use_proxy.Data.WeatherForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionId");

                    b.ToTable("Forecasts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 1, 2, 0, 58, 13, 496, DateTimeKind.Local).AddTicks(9811),
                            DescriptionId = 8,
                            TemperatureC = -17
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 1, 3, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8436),
                            DescriptionId = 4,
                            TemperatureC = 8
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 1, 4, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8461),
                            DescriptionId = 8,
                            TemperatureC = 39
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2020, 1, 5, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8466),
                            DescriptionId = 7,
                            TemperatureC = 31
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2020, 1, 6, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8468),
                            DescriptionId = 7,
                            TemperatureC = 10
                        });
                });

            modelBuilder.Entity("sample_use_proxy.Data.WeatherForecast", b =>
                {
                    b.HasOne("sample_use_proxy.Data.Description", "Description")
                        .WithMany()
                        .HasForeignKey("DescriptionId");
                });
#pragma warning restore 612, 618
        }
    }
}