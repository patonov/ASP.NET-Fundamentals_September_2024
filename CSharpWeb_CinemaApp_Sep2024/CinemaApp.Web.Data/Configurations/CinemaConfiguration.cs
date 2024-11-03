using CinemaApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Web.Data.Configurations
{
    using static Common.EntityValidationConstants.Cinema;

    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(CinemaNameMaxLength);

            builder.HasData(this.SeedCinemas());
        }

        private IEnumerable<Cinema> SeedCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>()
            {
                new Cinema()
                { 
                Id = 1,
                Name = "Kranta Movies",
                Location = "Stolipinovo"
                },
                new Cinema()
                {
                Id = 2,
                Name = "Bolliwood Cinema",
                Location = "Indian Neighborhood"
                },
                new Cinema()
                {
                Id = 3,
                Name = "Tralala Cinema",
                Location = "Fakulteta"
                }
            };

            return cinemas;
        }
    }
}
