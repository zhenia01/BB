using Microsoft.EntityFrameworkCore.Migrations;

namespace BB.DAL.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CheckingBranches",
                columns: new[] { "CheckingBranchId", "Balance" },
                values: new object[,]
                {
                    { 1, 1000m },
                    { 2, 100m },
                    { 3, 250m }
                });

            migrationBuilder.InsertData(
                table: "CreditBranches",
                columns: new[] { "CreditBranchId", "Available", "Balance", "Debt" },
                values: new object[,]
                {
                    { 1, 1000m, 1000m, null },
                    { 2, 500m, 500m, null },
                    { 3, 100m, 10m, null }
                });

            migrationBuilder.InsertData(
                table: "DepositBranches",
                column: "DepositBranchId",
                values: new object[]
                {
                    1,
                    2,
                    3
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Alex", "Slobozhenko" },
                    { 2, "John", "Travolta" },
                    { 3, "Bill", "Gates" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "CheckingBranchId", "CreditBranchId", "DepositBranchId", "IsBlocked", "Number", "Pin", "UserId" },
                values: new object[] { 1, 1, 1, 1, false, "1111", "$2a$11$TsrLXCNWHnW.sTaTshjXS.JOmQPVGGxKxCLwwudeazweuEGlPXq/q", 1 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "CheckingBranchId", "CreditBranchId", "DepositBranchId", "IsBlocked", "Number", "Pin", "UserId" },
                values: new object[] { 2, 2, 2, 2, false, "2222", "$2a$11$qicMzn2YNEv1MHpJ5ELFWue5N6R5akWvoBeYowWb6ml2PfYCId8Mq", 2 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "CheckingBranchId", "CreditBranchId", "DepositBranchId", "IsBlocked", "Number", "Pin", "UserId" },
                values: new object[] { 3, 3, 3, 3, false, "3333", "$2a$11$.lsq6c0PZMDCrtrsH0tW0ONr7UKeVk7/z2DeQ0B0fgwWIPwSdXvu6", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CheckingBranches",
                keyColumn: "CheckingBranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CheckingBranches",
                keyColumn: "CheckingBranchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CheckingBranches",
                keyColumn: "CheckingBranchId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CreditBranches",
                keyColumn: "CreditBranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CreditBranches",
                keyColumn: "CreditBranchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CreditBranches",
                keyColumn: "CreditBranchId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DepositBranches",
                keyColumn: "DepositBranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DepositBranches",
                keyColumn: "DepositBranchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DepositBranches",
                keyColumn: "DepositBranchId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
