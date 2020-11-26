using BB.DAL.Context.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BB.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder model)
        {
            model.ApplyConfigurationsFromAssembly(typeof(CardConfiguration).Assembly);
        }
    }
}