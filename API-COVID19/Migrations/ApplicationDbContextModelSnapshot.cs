﻿// <auto-generated />
using System;
using API_COVID19;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICOVID19.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_COVID19.Models.Cases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Confirmed")
                        .HasColumnType("numeric");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateReport")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Deaths")
                        .HasColumnType("numeric");

                    b.Property<int?>("ProvinceStateCaseId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProvinceStateId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<decimal>("Recovered")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ProvinceStateId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("API_COVID19.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Combined_Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("API_COVID19.Models.Frecuency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Active")
                        .HasColumnType("numeric");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<int>("DateReport")
                        .HasColumnType("integer");

                    b.Property<decimal>("Deaths")
                        .HasColumnType("numeric");

                    b.Property<int>("FrecuencyTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Recovered")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("FrecuencyTypeId");

                    b.ToTable("Frecuency");
                });

            modelBuilder.Entity("API_COVID19.Models.FrecuencyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Frecuency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Frecuency_Type");
                });

            modelBuilder.Entity("API_COVID19.Models.ProvinceState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Province_state");
                });

            modelBuilder.Entity("API_COVID19.Models.Vaccinateds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AtLeastOneDosis")
                        .HasColumnType("numeric");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Dosis")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Vaccinateds");
                });

            modelBuilder.Entity("API_COVID19.Models.WorldmapData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Worldmap_Data");
                });

            modelBuilder.Entity("API_COVID19.Models.Cases", b =>
                {
                    b.HasOne("API_COVID19.Models.Country", "CountryCaseReport")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_COVID19.Models.ProvinceState", "ProvinceState")
                        .WithMany()
                        .HasForeignKey("ProvinceStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CountryCaseReport");

                    b.Navigation("ProvinceState");
                });

            modelBuilder.Entity("API_COVID19.Models.Frecuency", b =>
                {
                    b.HasOne("API_COVID19.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_COVID19.Models.FrecuencyType", "FrecuencyType")
                        .WithMany()
                        .HasForeignKey("FrecuencyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("FrecuencyType");
                });

            modelBuilder.Entity("API_COVID19.Models.ProvinceState", b =>
                {
                    b.HasOne("API_COVID19.Models.Country", "Country")
                        .WithMany("ProvinceStates")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("API_COVID19.Models.Vaccinateds", b =>
                {
                    b.HasOne("API_COVID19.Models.Country", "CountryCase")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CountryCase");
                });

            modelBuilder.Entity("API_COVID19.Models.Country", b =>
                {
                    b.Navigation("ProvinceStates");
                });
#pragma warning restore 612, 618
        }
    }
}
