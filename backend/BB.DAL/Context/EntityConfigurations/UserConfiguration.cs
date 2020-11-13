using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BB.DAL.Context.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> model)
        {
            model.Property(u => u.FirstName).IsRequired().HasMaxLength(25);
            model.Property(u => u.LastName).IsRequired().HasMaxLength(25);
        }
    }
}