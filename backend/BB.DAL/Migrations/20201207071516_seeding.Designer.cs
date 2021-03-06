﻿// <auto-generated />
using System;
using BB.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BB.DAL.Migrations
{
    [DbContext(typeof(BBContext))]
    [Migration("20201207071516_seeding")]
    partial class seeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BB.DAL.Entities.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CheckingBranchId")
                        .HasColumnType("int");

                    b.Property<int?>("CreditBranchId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositBranchId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nchar(4)")
                        .IsFixedLength(true);

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CardId");

                    b.HasIndex("CheckingBranchId")
                        .IsUnique();

                    b.HasIndex("CreditBranchId")
                        .IsUnique()
                        .HasFilter("[CreditBranchId] IS NOT NULL");

                    b.HasIndex("DepositBranchId")
                        .IsUnique()
                        .HasFilter("[DepositBranchId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            CardId = 1,
                            CheckingBranchId = 1,
                            CreditBranchId = 1,
                            DepositBranchId = 1,
                            IsBlocked = false,
                            Number = "1111",
                            Pin = "$2a$11$TsrLXCNWHnW.sTaTshjXS.JOmQPVGGxKxCLwwudeazweuEGlPXq/q",
                            UserId = 1
                        },
                        new
                        {
                            CardId = 2,
                            CheckingBranchId = 2,
                            CreditBranchId = 2,
                            DepositBranchId = 2,
                            IsBlocked = false,
                            Number = "2222",
                            Pin = "$2a$11$qicMzn2YNEv1MHpJ5ELFWue5N6R5akWvoBeYowWb6ml2PfYCId8Mq",
                            UserId = 2
                        },
                        new
                        {
                            CardId = 3,
                            CheckingBranchId = 3,
                            CreditBranchId = 3,
                            DepositBranchId = 3,
                            IsBlocked = false,
                            Number = "3333",
                            Pin = "$2a$11$.lsq6c0PZMDCrtrsH0tW0ONr7UKeVk7/z2DeQ0B0fgwWIPwSdXvu6",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("BB.DAL.Entities.CheckingBranch", b =>
                {
                    b.Property<int>("CheckingBranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Balance")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("CheckingBranchId");

                    b.ToTable("CheckingBranches");

                    b.HasData(
                        new
                        {
                            CheckingBranchId = 1,
                            Balance = 1000m
                        },
                        new
                        {
                            CheckingBranchId = 2,
                            Balance = 100m
                        },
                        new
                        {
                            CheckingBranchId = 3,
                            Balance = 250m
                        });
                });

            modelBuilder.Entity("BB.DAL.Entities.CreditBranch", b =>
                {
                    b.Property<int>("CreditBranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Available")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Balance")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal?>("Debt")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("CreditBranchId");

                    b.ToTable("CreditBranches");

                    b.HasData(
                        new
                        {
                            CreditBranchId = 1,
                            Available = 1000m,
                            Balance = 1000m
                        },
                        new
                        {
                            CreditBranchId = 2,
                            Available = 500m,
                            Balance = 500m
                        },
                        new
                        {
                            CreditBranchId = 3,
                            Available = 100m,
                            Balance = 10m
                        });
                });

            modelBuilder.Entity("BB.DAL.Entities.Deposit", b =>
                {
                    b.Property<int>("DepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("CanBeTerminated")
                        .HasColumnType("bit");

                    b.Property<decimal>("DepSum")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("DepositBranchId")
                        .HasColumnType("int");

                    b.Property<bool>("PaymentsToDeposit")
                        .HasColumnType("bit");

                    b.Property<double>("Percent")
                        .HasColumnType("float");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("DepositId");

                    b.HasIndex("DepositBranchId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("BB.DAL.Entities.DepositBranch", b =>
                {
                    b.Property<int>("DepositBranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("DepositBranchId");

                    b.ToTable("DepositBranches");

                    b.HasData(
                        new
                        {
                            DepositBranchId = 1
                        },
                        new
                        {
                            DepositBranchId = 2
                        },
                        new
                        {
                            DepositBranchId = 3
                        });
                });

            modelBuilder.Entity("BB.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "Alex",
                            LastName = "Slobozhenko"
                        },
                        new
                        {
                            UserId = 2,
                            FirstName = "John",
                            LastName = "Travolta"
                        },
                        new
                        {
                            UserId = 3,
                            FirstName = "Bill",
                            LastName = "Gates"
                        });
                });

            modelBuilder.Entity("BB.DAL.Entities.Card", b =>
                {
                    b.HasOne("BB.DAL.Entities.CheckingBranch", "CheckingBranch")
                        .WithOne("Card")
                        .HasForeignKey("BB.DAL.Entities.Card", "CheckingBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BB.DAL.Entities.CreditBranch", "CreditBranch")
                        .WithOne("Card")
                        .HasForeignKey("BB.DAL.Entities.Card", "CreditBranchId");

                    b.HasOne("BB.DAL.Entities.DepositBranch", "DepositBranch")
                        .WithOne("Card")
                        .HasForeignKey("BB.DAL.Entities.Card", "DepositBranchId");

                    b.HasOne("BB.DAL.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckingBranch");

                    b.Navigation("CreditBranch");

                    b.Navigation("DepositBranch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BB.DAL.Entities.Deposit", b =>
                {
                    b.HasOne("BB.DAL.Entities.DepositBranch", "DepositBranch")
                        .WithMany("Deposits")
                        .HasForeignKey("DepositBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepositBranch");
                });

            modelBuilder.Entity("BB.DAL.Entities.CheckingBranch", b =>
                {
                    b.Navigation("Card");
                });

            modelBuilder.Entity("BB.DAL.Entities.CreditBranch", b =>
                {
                    b.Navigation("Card");
                });

            modelBuilder.Entity("BB.DAL.Entities.DepositBranch", b =>
                {
                    b.Navigation("Card");

                    b.Navigation("Deposits");
                });

            modelBuilder.Entity("BB.DAL.Entities.User", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
