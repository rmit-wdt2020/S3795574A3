﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3795574A2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(maxLength: 4, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    States = table.Column<int>(maxLength: 20, nullable: false),
                    PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    TFN = table.Column<string>(maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Payees",
                columns: table => new
                {
                    PayeeID = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    States = table.Column<int>(maxLength: 20, nullable: false),
                    PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payees", x => x.PayeeID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<int>(maxLength: 4, nullable: false),
                    AccountType = table.Column<int>(maxLength: 1, nullable: false),
                    Balance = table.Column<decimal>(type: "money", nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.CheckConstraint("CH_Account_Balance", "Balance >= 0");
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    UserID = table.Column<string>(maxLength: 8, nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 64, nullable: false),
                    ModifyDate = table.Column<DateTime>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.UserID);
                    table.CheckConstraint("CH_Login_UserID", "len(UserID) = 8");
                    table.CheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                    table.ForeignKey(
                        name: "FK_Logins_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillPays",
                columns: table => new
                {
                    BillPayID = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(maxLength: 4, nullable: false),
                    PayeeID = table.Column<int>(maxLength: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "money", maxLength: 8, nullable: false),
                    ScheduleDate = table.Column<DateTime>(maxLength: 8, nullable: false),
                    Period = table.Column<int>(maxLength: 1, nullable: false),
                    ModifyDate = table.Column<DateTime>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPays", x => x.BillPayID);
                    table.CheckConstraint("CH_BillPay_Amount", "Amount > 0");
                    table.ForeignKey(
                        name: "FK_BillPays_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillPays_Payees_PayeeID",
                        column: x => x.PayeeID,
                        principalTable: "Payees",
                        principalColumn: "PayeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(maxLength: 1, nullable: false),
                    AccountNumber = table.Column<int>(maxLength: 4, nullable: false),
                    DestinationAccountNumber = table.Column<int>(maxLength: 4, nullable: true),
                    Amount = table.Column<decimal>(type: "money", maxLength: 8, nullable: false),
                    Comment = table.Column<string>(maxLength: 255, nullable: true),
                    ModifyDate = table.Column<DateTime>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.CheckConstraint("CH_Transaction_Amount", "Amount > 0");
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_DestinationAccountNumber",
                        column: x => x.DestinationAccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_AccountNumber",
                table: "BillPays",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_PayeeID",
                table: "BillPays",
                column: "PayeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationAccountNumber",
                table: "Transactions",
                column: "DestinationAccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPays");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Payees");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
