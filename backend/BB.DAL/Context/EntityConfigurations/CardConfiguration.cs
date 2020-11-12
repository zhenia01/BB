using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BB.DAL.Context.EntityConfigurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> model)
        {
            model.Property(c => c.Pin).IsRequired().IsFixedLength().HasMaxLength(4);
        }
    }
}