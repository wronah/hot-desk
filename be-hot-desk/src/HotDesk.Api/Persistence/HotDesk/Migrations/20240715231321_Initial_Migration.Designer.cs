﻿// <auto-generated />
using System;
using HotDesk.Api.Persistence.HotDesk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotDesk.Api.Persistence.HotDesk.Migrations
{
    [DbContext(typeof(HotDeskDbContext))]
    [Migration("20240715231321_Initial_Migration")]
    partial class Initial_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeeRole", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("integer");

                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeesId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("employee_roles", "public");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("add_date");

                    b.Property<DateTime?>("EndReservationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_reservation_date");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("remove_date");

                    b.Property<DateTime?>("StartReservationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_reservation_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("desks", "public");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeskId")
                        .HasColumnType("integer")
                        .HasColumnName("desk_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.HasIndex("DeskId")
                        .IsUnique();

                    b.ToTable("employees", "public");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("add_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("remove_date");

                    b.HasKey("Id");

                    b.ToTable("locations", "public");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", "public");
                });

            modelBuilder.Entity("EmployeeRole", b =>
                {
                    b.HasOne("HotDesk.Api.Persistence.HotDesk.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotDesk.Api.Persistence.HotDesk.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Desk", b =>
                {
                    b.HasOne("HotDesk.Api.Persistence.HotDesk.Entities.Location", "Location")
                        .WithMany("Desks")
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Employee", b =>
                {
                    b.HasOne("HotDesk.Api.Persistence.HotDesk.Entities.Desk", "Desk")
                        .WithOne("Employee")
                        .HasForeignKey("HotDesk.Api.Persistence.HotDesk.Entities.Employee", "DeskId");

                    b.Navigation("Desk");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Desk", b =>
                {
                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HotDesk.Api.Persistence.HotDesk.Entities.Location", b =>
                {
                    b.Navigation("Desks");
                });
#pragma warning restore 612, 618
        }
    }
}
