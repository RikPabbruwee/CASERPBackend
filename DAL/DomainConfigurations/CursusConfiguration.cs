using Domain_Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DomainConfigurations
{
    public class CursusConfiguration : IEntityTypeConfiguration<Cursus>
    {
        public void Configure(EntityTypeBuilder<Cursus> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
