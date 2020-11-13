using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BB.DAL.Context.EntityConfigurations
{
    public class CheckingBranchConfiguration : IEntityTypeConfiguration<CheckingBranch>
    {
        public void Configure(EntityTypeBuilder<CheckingBranch> model)
        {
            model.Property(cb => cb.Balance).HasPrecision(9, 2);
        }
    }
}