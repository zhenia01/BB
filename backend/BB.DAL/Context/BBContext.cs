using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.DAL.Context
{
    public class BBContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<CreditBranch> CreditBranches { get; set; }
        public DbSet<CheckingBranch> CheckingBranches { get; set; }
        public DbSet<DepositBranch> DepositBranches { get; set; }

        
        public BBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}