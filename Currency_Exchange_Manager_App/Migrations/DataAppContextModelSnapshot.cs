﻿// <auto-generated />
using Currency_Exchange_Manager_App.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Currency_Exchange_Manager_App.Migrations
{
    [DbContext(typeof(DataAppContext))]
    partial class DataAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Currency_Exchange_Manager_App.Model.Convert_Currency", b =>
                {
                    b.Property<int>("Idconvert_currency")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Conversion_Rate")
                        .HasColumnType("double");

                    b.Property<string>("currency_from")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("currency_to")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Idconvert_currency");

                    b.ToTable("Convert_Currency");
                });

            modelBuilder.Entity("Currency_Exchange_Manager_App.Model.CurrencyInfo", b =>
                {
                    b.Property<int>("Idcurrency_info")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Currency_details")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rates")
                        .HasColumnType("int");

                    b.HasKey("Idcurrency_info");

                    b.ToTable("Currency_Info");
                });
#pragma warning restore 612, 618
        }
    }
}
