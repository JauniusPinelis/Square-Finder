﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SquareFinder.Infrastructure.Db;

namespace SquareFinder.Infrastructure.Migrations
{
    [DbContext(typeof(PointContext))]
    [Migration("20190930135603_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SquareFinder.Infrastructure.Entities.PointEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PointListId");

                    b.Property<int?>("SquareEntityId");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.HasKey("Id");

                    b.HasIndex("PointListId");

                    b.HasIndex("SquareEntityId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("SquareFinder.Infrastructure.Entities.PointListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("SquareId");

                    b.HasKey("Id");

                    b.HasIndex("SquareId");

                    b.ToTable("PointList");
                });

            modelBuilder.Entity("SquareFinder.Infrastructure.Entities.SquareEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("SquareEntity");
                });

            modelBuilder.Entity("SquareFinder.Infrastructure.Entities.PointEntity", b =>
                {
                    b.HasOne("SquareFinder.Infrastructure.Entities.PointListEntity", "PointList")
                        .WithMany("Points")
                        .HasForeignKey("PointListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SquareFinder.Infrastructure.Entities.SquareEntity")
                        .WithMany("Points")
                        .HasForeignKey("SquareEntityId");
                });

            modelBuilder.Entity("SquareFinder.Infrastructure.Entities.PointListEntity", b =>
                {
                    b.HasOne("SquareFinder.Infrastructure.Entities.SquareEntity", "Square")
                        .WithMany()
                        .HasForeignKey("SquareId");
                });
#pragma warning restore 612, 618
        }
    }
}
