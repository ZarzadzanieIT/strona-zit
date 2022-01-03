﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZIT.Infrastructure.Persistence;

#nullable disable

namespace ZIT.Infrastructure.Persistence.Migrations.SQLite
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220102231256_InitialDomain")]
    partial class InitialDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("ApplicationRoleApplicationUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsersInRoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("RolesId", "UsersInRoleId");

                    b.HasIndex("UsersInRoleId");

                    b.ToTable("ApplicationRoleApplicationUser");
                });

            modelBuilder.Entity("ZIT.Core.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Entitlements")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentRoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentRoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ZIT.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Entitlements")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CoordinatorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("SlugTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ZIT.Core.Entities.SliderElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SliderElements");
                });

            modelBuilder.Entity("ApplicationRoleApplicationUser", b =>
                {
                    b.HasOne("ZIT.Core.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZIT.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UsersInRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZIT.Core.Entities.ApplicationRole", b =>
                {
                    b.HasOne("ZIT.Core.Entities.ApplicationRole", "ParentRole")
                        .WithMany("ChildrenRoles")
                        .HasForeignKey("ParentRoleId");

                    b.Navigation("ParentRole");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Department", b =>
                {
                    b.HasOne("ZIT.Core.Entities.Member", "Coordinator")
                        .WithOne()
                        .HasForeignKey("ZIT.Core.Entities.Department", "CoordinatorId");

                    b.Navigation("Coordinator");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Member", b =>
                {
                    b.HasOne("ZIT.Core.Entities.Department", "Department")
                        .WithMany("Members")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ZIT.Core.Entities.ApplicationRole", b =>
                {
                    b.Navigation("ChildrenRoles");
                });

            modelBuilder.Entity("ZIT.Core.Entities.Department", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
