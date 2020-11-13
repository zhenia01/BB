using Microsoft.EntityFrameworkCore.Migrations;

namespace BB.DAL.Migrations
{
    public partial class cardFks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CreditBranches_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_DepositBranches_DepositBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_DepositBranchId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "DepositBranchId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreditBranchId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CreditBranchId",
                table: "Cards",
                column: "CreditBranchId",
                unique: true,
                filter: "[CreditBranchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DepositBranchId",
                table: "Cards",
                column: "DepositBranchId",
                unique: true,
                filter: "[DepositBranchId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CreditBranches_CreditBranchId",
                table: "Cards",
                column: "CreditBranchId",
                principalTable: "CreditBranches",
                principalColumn: "CreditBranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_DepositBranches_DepositBranchId",
                table: "Cards",
                column: "DepositBranchId",
                principalTable: "DepositBranches",
                principalColumn: "DepositBranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CreditBranches_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_DepositBranches_DepositBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_DepositBranchId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "DepositBranchId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditBranchId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CreditBranchId",
                table: "Cards",
                column: "CreditBranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DepositBranchId",
                table: "Cards",
                column: "DepositBranchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CreditBranches_CreditBranchId",
                table: "Cards",
                column: "CreditBranchId",
                principalTable: "CreditBranches",
                principalColumn: "CreditBranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_DepositBranches_DepositBranchId",
                table: "Cards",
                column: "DepositBranchId",
                principalTable: "DepositBranches",
                principalColumn: "DepositBranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
