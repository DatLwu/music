﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using demo.Data;

#nullable disable

namespace demo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240510015510_Update_table")]
    partial class Update_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("demo.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("demo.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("demo.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserID"));

                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("demo.Models.songs", b =>
                {
                    b.Property<int>("SongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SongID"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("File_path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ListenCount")
                        .HasColumnType("integer");

                    b.Property<string>("Song_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SongID");

                    b.HasIndex("CategoryID");

                    b.ToTable("songs");
                });

            modelBuilder.Entity("demo.Models.User", b =>
                {
                    b.HasOne("demo.Models.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("demo.Models.songs", b =>
                {
                    b.HasOne("demo.Models.Category", "Category")
                        .WithMany("songs")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("demo.Models.Category", b =>
                {
                    b.Navigation("songs");
                });

            modelBuilder.Entity("demo.Models.Role", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
