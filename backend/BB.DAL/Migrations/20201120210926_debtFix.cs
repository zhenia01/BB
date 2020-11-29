using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BB.DAL.Migrations
{
    public partial class debtFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOver",
                table: "CreditBranches");

            migrationBuilder.AlterColumn<decimal>(
                name: "Debt",
                table: "CreditBranches",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "WithdrawTime",
                table: "CreditBranches",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WithdrawTime",
                table: "CreditBranches");

            migrationBuilder.AlterColumn<decimal>(
                name: "Debt",
                table: "CreditBranches",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeOver",
                table: "CreditBranches",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
