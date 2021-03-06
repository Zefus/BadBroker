﻿// <auto-generated />
using System;
using BadBroker.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BadBroker.DataAccess.Migrations
{
    [DbContext(typeof(BadBrokerContext))]
    partial class BadBrokerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BadBroker.DataAccess.Models.RatesData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("QuotesData");
                });

            modelBuilder.Entity("BadBroker.DataAccess.Models.RatesPerDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Rate");

                    b.Property<int>("RatesDataId");

                    b.HasKey("Id");

                    b.HasIndex("RatesDataId");

                    b.ToTable("RatesPerDate");
                });

            modelBuilder.Entity("BadBroker.DataAccess.Models.RatesPerDate", b =>
                {
                    b.HasOne("BadBroker.DataAccess.Models.RatesData", "RatesData")
                        .WithMany("RatesPerDate")
                        .HasForeignKey("RatesDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
