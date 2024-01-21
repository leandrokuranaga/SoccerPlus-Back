using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerPlus.Domain.PlayerAggregate;

namespace SoccerPlus.Infra.Data.MapEntities
{
    public class PlayerMap : IEntityTypeConfiguration<PlayerDomain>
    {
        public void Configure(EntityTypeBuilder<PlayerDomain> builder)
        {
            builder.ToTable("Player");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.Latitude).HasColumnType("float").IsRequired();
            builder.Property(x => x.Longitude).HasColumnType("float").IsRequired();
            builder.Property(x => x.Position).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.FullName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.CPF).HasColumnType("varchar(30)").IsRequired();
            builder.Property(x => x.BirthdayDate).HasColumnType("date").IsRequired();
            builder.Property(x => x.IdMatch).HasColumnType("int").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("varchar(100)").IsRequired();

            builder.HasOne(x => x.SoccerMatch)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.IdMatch);
        }
    }
}
