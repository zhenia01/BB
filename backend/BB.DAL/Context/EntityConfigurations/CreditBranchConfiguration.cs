using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BB.DAL.Context.EntityConfigurations
{
    public class CreditBranchConfiguration : IEntityTypeConfiguration<CreditBranch>
    {
        public void Configure(EntityTypeBuilder<CreditBranch> model)
        {
            model.Property(cb => cb.Balance).HasPrecision(9, 2);
            model.Property(cb => cb.Available).HasPrecision(9, 2);
            model.Property(cb => cb.Debt).HasPrecision(9, 2);
        }
    }
}