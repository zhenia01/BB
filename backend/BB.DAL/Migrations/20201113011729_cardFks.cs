using Microsoft.EntityFrameworkCore.Migrations;

namespace BB.DAL.Migrations
{
    public partial class cardFks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckingBranches_Cards_CardId",
                table: "CheckingBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditBranches_Cards_CardId",
                table: "CreditBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositBranches_Cards_CardId",
                table: "DepositBranches");

            migrationBuilder.DropIndex(
                name: "IX_DepositBranches_CardId",
                table: "DepositBranches");

            migrationBuilder.DropIndex(
                name: "IX_CreditBranches_CardId",
                table: "CreditBranches");

            migrationBuilder.DropIndex(
                name: "IX_CheckingBranches_CardId",
                table: "CheckingBranches");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "DepositBranches");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CreditBranches");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CheckingBranches");

            migrationBuilder.AddColumn<int>(
                name: "CheckingBranchId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditBranchId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepositBranchId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CheckingBranchId",
                table: "Cards",
                column: "CheckingBranchId",
                unique: true);

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
                name: "FK_Cards_CheckingBranches_CheckingBranchId",
                table: "Cards",
                column: "CheckingBranchId",
                principalTable: "CheckingBranches",
                principalColumn: "CheckingBranchId",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CheckingBranches_CheckingBranchId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CreditBranches_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_DepositBranches_DepositBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CheckingBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CreditBranchId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_DepositBranchId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CheckingBranchId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreditBranchId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "DepositBranchId",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "DepositBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CreditBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CheckingBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DepositBranches_CardId",
                table: "DepositBranches",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CreditBranches_CardId",
                table: "CreditBranches",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckingBranches_CardId",
                table: "CheckingBranches",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckingBranches_Cards_CardId",
                table: "CheckingBranches",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditBranches_Cards_CardId",
                table: "CreditBranches",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositBranches_Cards_CardId",
                table: "DepositBranches",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
