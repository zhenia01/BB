using System.Collections.Generic;
using System.Linq;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.DAL.Context
{
    public class BBContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public BBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();

            List<int> l = new List<int>();
        }
    }
}