﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonAPI.Data;

#nullable disable

namespace PokemonAPI.Migrations
{
    [DbContext(typeof(AppDbDataContext))]
    partial class AppDbDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("PokemonAPI.Models.Pokemon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("spritesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("weight")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.HasIndex("spritesId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonsCapturados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PokemonName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PokemonsCapturados");
                });

            modelBuilder.Entity("PokemonAPI.Models.Sprites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("front_default")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sprites");
                });

            modelBuilder.Entity("PokemonAPI.Models.Type2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Type2");
                });

            modelBuilder.Entity("PokemonAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Idade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Regiao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PokemonAPI.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Pokemonid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("slot")
                        .HasColumnType("INTEGER");

                    b.Property<int>("typeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Pokemonid");

                    b.HasIndex("typeId");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("PokemonAPI.Models.Pokemon", b =>
                {
                    b.HasOne("PokemonAPI.Models.User", "User")
                        .WithMany("Pokemons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.Models.Sprites", "sprites")
                        .WithMany()
                        .HasForeignKey("spritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("sprites");
                });

            modelBuilder.Entity("PokemonAPI.Type", b =>
                {
                    b.HasOne("PokemonAPI.Models.Pokemon", null)
                        .WithMany("types")
                        .HasForeignKey("Pokemonid");

                    b.HasOne("PokemonAPI.Models.Type2", "type")
                        .WithMany()
                        .HasForeignKey("typeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("type");
                });

            modelBuilder.Entity("PokemonAPI.Models.Pokemon", b =>
                {
                    b.Navigation("types");
                });

            modelBuilder.Entity("PokemonAPI.Models.User", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
