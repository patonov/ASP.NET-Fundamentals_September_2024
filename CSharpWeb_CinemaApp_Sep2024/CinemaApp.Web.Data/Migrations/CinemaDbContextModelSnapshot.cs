﻿// <auto-generated />
using System;
using CinemaApp.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApp.Web.Data.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    partial class CinemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApp.Web.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "It's a really great production for a little boy readint too much.",
                            Director = "Pesho direktorcheto",
                            Duration = 2,
                            Genre = "Fantasy",
                            ReleaseDate = new DateTime(1999, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Poter and Co."
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is a favorite movie of generations of people that like strong introvert men.",
                            Director = "Ted Kochev",
                            Duration = 3,
                            Genre = "Action",
                            ReleaseDate = new DateTime(1991, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Rambo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
