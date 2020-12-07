using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BB.DAL.Migrations
{
    public partial class DeleteRedundant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyOfDeposit",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "CreditBranches");

            migrationBuilder.DropColumn(
                name: "WithdrawTime",
                table: "CreditBranches");

            migrationBuilder.AlterColumn<decimal>(
                name: "DepSum",
                table: "Deposits",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DepSum",
                table: "Deposits",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyOfDeposit",
                table: "Deposits",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Interest",
                table: "CreditBranches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WithdrawTime",
                table: "CreditBranches",
                type: "datetime2",
                nullable: true);
        }
    }
}
