using BB.DAL.Context.EntityConfigurations;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BB.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder model)
        {
            model.ApplyConfigurationsFromAssembly(typeof(CardConfiguration).Assembly);
        }

        public static void Seed(this ModelBuilder model)
        {
            var user1 = new User()
            {
                UserId = 1,
                FirstName = "Alex",
                LastName = "Slobozhenko"
            };

            var user2 = new User()
            {
                UserId = 2,
                FirstName = "John",
                LastName = "Travolta"
            };

            var user3 = new User()
            {
                UserId = 3,
                FirstName = "Bill",
                LastName = "Gates"
            };

            var checkBranch1 = new CheckingBranch()
            {
                CheckingBranchId = 1,
                Balance = 1000
            };

            var checkBranch2 = new CheckingBranch()
            {
                CheckingBranchId = 2,
                Balance = 100
            };

            var checkBranch3 = new CheckingBranch()
            {
                CheckingBranchId = 3,
                Balance = 250
            };

            var creditBranch1 = new CreditBranch()
            {
                CreditBranchId = 1,
                Available = 1000,
                Balance = 1000,
            };

            var creditBranch2 = new CreditBranch()
            {
                CreditBranchId = 2,
                Available = 500,
                Balance = 500,
            };

            var creditBranch3 = new CreditBranch()
            {
                CreditBranchId = 3,
                Available = 100,
                Balance = 10,
            };

            var depositBranch1 = new DepositBranch()
            {
                DepositBranchId = 1,
            };

            var depositBranch2 = new DepositBranch()
            {
                DepositBranchId = 2,
            };

            var depositBranch3 = new DepositBranch()
            {
                DepositBranchId = 3,
            };


            var card1 = new Card()
            {
                CardId = 1,
                Pin = BC.HashPassword("1111"),
                IsBlocked = false,
                UserId = 1,
                CheckingBranchId = 1,
                CreditBranchId = 1,
                DepositBranchId = 1,
                Number = "1111"
            };

            var card2 = new Card()
            {
                CardId = 2,
                Pin = BC.HashPassword("2222"),
                IsBlocked = false,
                UserId = 2,
                CheckingBranchId = 2,
                CreditBranchId = 2,
                DepositBranchId = 2,
                Number = "2222"
            };

            var card3 = new Card()
            {
                CardId = 3,
                Pin = BC.HashPassword("3333"),
                IsBlocked = false,
                UserId = 3,
                CheckingBranchId = 3,
                CreditBranchId = 3,
                DepositBranchId = 3,
                Number = "3333"
            };

            model.Entity<CheckingBranch>().HasData(checkBranch1, checkBranch2, checkBranch3);
            model.Entity<DepositBranch>().HasData(depositBranch1, depositBranch2, depositBranch3);
            model.Entity<CreditBranch>().HasData(creditBranch1, creditBranch2, creditBranch3);
            model.Entity<User>().HasData(user1, user2, user3);
            model.Entity<Card>().HasData(card1, card2, card3);

            // context.Cards.Load();
            // context.Users.Load();
            //
            // context.Cards.Add(card1);
            // context.Cards.Add(card2);
            // context.Cards.Add(card3);
            //
            // context.Users.Add(user1);
            // context.Users.Add(user2);
            // context.Users.Add(user3);
            //
            // context.CheckingBranches.Add(checkBranch1);
            // context.CheckingBranches.Add(checkBranch2);
            // context.CheckingBranches.Add(checkBranch3);
            //
            // context.CreditBranches.Add(creditBranch1);
            // context.CreditBranches.Add(creditBranch2);
            // context.CreditBranches.Add(creditBranch3);
            //
            // context.DepositBranches.Add(depositBranch1);
            // context.DepositBranches.Add(depositBranch2);
            // context.DepositBranches.Add(depositBranch3);
            //
            // context.SaveChanges();
        }
    }
}