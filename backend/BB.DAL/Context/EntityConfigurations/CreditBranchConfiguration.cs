using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BB.DAL.Context.EntityConfigurations
{
    public class CreditBranchConfiguration : IEntityTypeConfiguration<CreditBranch>
    {
        public void Configure(EntityTypeBuilder<CreditBranch> model)
        {
            model.Property(cb => cb.TimeOver).IsRequired(false);
            model.Property(cb => cb.Debt).IsRequired(false);
        }
    }
}