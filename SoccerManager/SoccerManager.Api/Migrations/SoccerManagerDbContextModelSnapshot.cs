﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoccerManager.Api.Shared.DbContexts;

#nullable disable

namespace SoccerManager.Api.Migrations
{
    [DbContext(typeof(SoccerManagerDbContext))]
    partial class SoccerManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.PlayerValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerValues");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.TeamAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamAmounts");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsPlayer")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayers");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSold")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamFromId")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamToId")
                        .HasColumnType("integer");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamFromId");

                    b.HasIndex("TeamToId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.PlayerValue", b =>
                {
                    b.HasOne("SoccerManager.Api.Shared.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.Team", b =>
                {
                    b.HasOne("SoccerManager.Api.Shared.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.TeamAmount", b =>
                {
                    b.HasOne("SoccerManager.Api.Shared.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.TeamPlayer", b =>
                {
                    b.HasOne("SoccerManager.Api.Shared.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("SoccerManager.Api.Shared.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SoccerManager.Api.Shared.Entities.Transfer", b =>
                {
                    b.HasOne("SoccerManager.Api.Shared.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("SoccerManager.Api.Shared.Entities.Team", "TeamFrom")
                        .WithMany()
                        .HasForeignKey("TeamFromId");

                    b.HasOne("SoccerManager.Api.Shared.Entities.Team", "TeamTo")
                        .WithMany()
                        .HasForeignKey("TeamToId");

                    b.Navigation("Player");

                    b.Navigation("TeamFrom");

                    b.Navigation("TeamTo");
                });
#pragma warning restore 612, 618
        }
    }
}