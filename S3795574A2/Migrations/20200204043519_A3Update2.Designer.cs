﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using S3795574A2.Data;

namespace S3795574A2.Migrations
{
    [DbContext(typeof(NwbaContext))]
    [Migration("20200204043519_A3Update2")]
    partial class A3Update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("S3795574A2.Models.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int")
                        .HasMaxLength(4);

                    b.Property<int>("AccountType")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2")
                        .HasMaxLength(8);

                    b.HasKey("AccountNumber");

                    b.HasIndex("CustomerID");

                    b.ToTable("Accounts");

                    b.HasCheckConstraint("CH_Account_Balance", "Balance >= 0");
                });

            modelBuilder.Entity("S3795574A2.Models.BillPay", b =>
                {
                    b.Property<int>("BillPayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PayeeID")
                        .HasColumnType("int");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BillPayID");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("PayeeID");

                    b.ToTable("BillPays");

                    b.HasCheckConstraint("CH_BillPay_Amount", "Amount > 0");
                });

            modelBuilder.Entity("S3795574A2.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("States")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("TFN")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("S3795574A2.Models.Login", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<int>("Attempt")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LockedToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2")
                        .HasMaxLength(8);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("UserID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Logins");

                    b.HasCheckConstraint("CH_Login_UserID", "len(UserID) = 8");

                    b.HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                });

            modelBuilder.Entity("S3795574A2.Models.Payee", b =>
                {
                    b.Property<int>("PayeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(4)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("States")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.HasKey("PayeeID");

                    b.ToTable("Payees");
                });

            modelBuilder.Entity("S3795574A2.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int")
                        .HasMaxLength(4);

                    b.Property<decimal>("Amount")
                        .HasColumnType("money")
                        .HasMaxLength(8);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("DestinationAccountNumber")
                        .HasColumnType("int")
                        .HasMaxLength(4);

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2")
                        .HasMaxLength(8);

                    b.Property<int>("TransactionType")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.HasKey("TransactionID");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("DestinationAccountNumber");

                    b.ToTable("Transactions");

                    b.HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
                });

            modelBuilder.Entity("S3795574A2.Models.Account", b =>
                {
                    b.HasOne("S3795574A2.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("S3795574A2.Models.BillPay", b =>
                {
                    b.HasOne("S3795574A2.Models.Account", "Account")
                        .WithMany("BillPays")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("S3795574A2.Models.Payee", "Payee")
                        .WithMany("BillPays")
                        .HasForeignKey("PayeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("S3795574A2.Models.Login", b =>
                {
                    b.HasOne("S3795574A2.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("S3795574A2.Models.Transaction", b =>
                {
                    b.HasOne("S3795574A2.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("S3795574A2.Models.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
