using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerPlus.Domain.SoccerMatchAggregate;

namespace SoccerPlus.Infra.Data.MapEntities
{
    public class SoccerMatchMap : IEntityTypeConfiguration<SoccerMatchDomain>
    {
        public void Configure(EntityTypeBuilder<SoccerMatchDomain> builder)
        {
            builder.ToTable("SoccerMatch");            

            builder.HasKey(e => e.Id);

            builder.Property(x => x.Address).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.City).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.UF).HasColumnType("varchar(2)").IsRequired();
            builder.Property(x => x.Latitude).HasColumnType("float").IsRequired();
            builder.Property(x => x.Longitude).HasColumnType("float").IsRequired();
            builder.Property(x => x.MatchDay).HasColumnType("date").IsRequired();
        }
    }
}
