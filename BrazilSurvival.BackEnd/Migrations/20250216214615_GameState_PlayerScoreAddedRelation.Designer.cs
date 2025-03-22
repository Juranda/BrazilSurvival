﻿// <auto-generated />
using System;
using BrazilSurvival.BackEnd.Data;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrazilSurvival.BackEnd.Migrations
{
    [DbContext(typeof(GameDbConext))]
    [Migration("20250216214615_GameState_PlayerScoreAddedRelation")]
    partial class GameState_PlayerScoreAddedRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 31);

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("ChallengeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeOption");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOptionConsequence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("ChallengeOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Consequence")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)");

                    b.Property<int?>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Money")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Power")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeOptionId");

                    b.ToTable("ChallengeOptionConsequence");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Game.Models.GameState", b =>
                {
                    b.Property<string>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Money")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Token");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.PlayersScores.Models.PlayerScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GameStateToken")
                        .IsRequired()
                        .HasColumnType("CHAR(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("VARCHAR(6)");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameStateToken");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PlayerScores");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOption", b =>
                {
                    b.HasOne("BrazilSurvival.BackEnd.Challenges.Models.Challenge", "Challenge")
                        .WithMany("Options")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOptionConsequence", b =>
                {
                    b.HasOne("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOption", "ChallengeOption")
                        .WithMany("Consequences")
                        .HasForeignKey("ChallengeOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChallengeOption");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.PlayersScores.Models.PlayerScore", b =>
                {
                    b.HasOne("BrazilSurvival.BackEnd.Game.Models.GameState", "GameState")
                        .WithMany()
                        .HasForeignKey("GameStateToken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameState");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.Challenge", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("BrazilSurvival.BackEnd.Challenges.Models.ChallengeOption", b =>
                {
                    b.Navigation("Consequences");
                });
#pragma warning restore 612, 618
        }
    }
}
