using Domain_Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DomainConfigurations
{
    public  class CursusInstantieConfiguration : IEntityTypeConfiguration<CursusInstantie>
    {
        public void Configure(EntityTypeBuilder<CursusInstantie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Cursisten);
            builder.HasOne(x => x.Cursus);  
        }
    }
}
