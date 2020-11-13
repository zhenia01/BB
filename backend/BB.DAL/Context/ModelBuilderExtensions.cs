using BB.DAL.Context.EntityConfigurations;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder model)
        {
            model.ApplyConfigurationsFromAssembly(typeof(CardConfiguration).Assembly);
            model.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            model.ApplyConfigurationsFromAssembly(typeof(DepositConfiguration).Assembly);
            model.ApplyConfigurationsFromAssembly(typeof(CreditBranchConfiguration).Assembly);
        }
    }
}