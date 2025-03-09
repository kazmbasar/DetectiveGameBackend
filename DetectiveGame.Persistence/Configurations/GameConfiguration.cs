using DetectiveGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetectiveGame.Persistence.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Evidences)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Notes)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 