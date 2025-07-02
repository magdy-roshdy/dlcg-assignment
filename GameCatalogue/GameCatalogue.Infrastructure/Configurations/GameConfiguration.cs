using GameCatalogue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameCatalogue.Infrastructure.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(512);

            builder.Property(g => g.ImagePath)
                   .HasMaxLength(512);

            builder.Property(g => g.Platforms)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(g => g.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(g => g.AddedOn)
                   .IsRequired();

            builder.Property(g => g.LastModified)
                   .IsRequired(false);
        }
    }
}
