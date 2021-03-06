﻿// <auto-generated />
using System;
using BodyWeightApp.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BodyWeightApp.DataContext.Migrations
{
    [DbContext(typeof(BodyInfoContext))]
    partial class BodyInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BodyWeightApp.DataContext.Entities.BodyWeight", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("MeasuredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("UserProfileID")
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("UserProfileID");

                    b.ToTable("body_weight_measurements");
                });

            modelBuilder.Entity("BodyWeightApp.DataContext.Entities.UserProfile", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.ToTable("user_profiles");
                });

            modelBuilder.Entity("BodyWeightApp.DataContext.Entities.BodyWeight", b =>
                {
                    b.HasOne("BodyWeightApp.DataContext.Entities.UserProfile", null)
                        .WithMany("Measurements")
                        .HasForeignKey("UserProfileID");
                });
#pragma warning restore 612, 618
        }
    }
}
