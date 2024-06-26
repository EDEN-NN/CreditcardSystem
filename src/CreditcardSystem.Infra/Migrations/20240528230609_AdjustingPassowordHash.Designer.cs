﻿// <auto-generated />
using System;
using CreditcardSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CreditcardSystem.Infra.Migrations
{
    [DbContext(typeof(CredicardDataContext))]
    [Migration("20240528230609_AdjustingPassowordHash")]
    partial class AdjustingPassowordHash
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CreditcardSystem.Domain.Models.Creditcard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("CardBill")
                        .HasColumnType("DECIMAL");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Creditcards");
                });

            modelBuilder.Entity("CreditcardSystem.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CreditcardSystem.Domain.Models.Creditcard", b =>
                {
                    b.HasOne("CreditcardSystem.Domain.Models.User", "Owner")
                        .WithMany("Creditcards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Creditcard_Owner");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CreditcardSystem.Domain.Models.User", b =>
                {
                    b.Navigation("Creditcards");
                });
#pragma warning restore 612, 618
        }
    }
}
