using CinemaApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.Data.Configurations
{
    using static Common.EntityValidationConstants.Movie;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(MovieTitleMaxLength);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(MovieGenreMaxLength);

            builder.HasData(this.SeedMovies());
        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                Id = 1,
                Title = "Harry Poter and Co.",
                Genre = "Fantasy",
                ReleaseDate = new DateTime(1999, 12, 01),
                Director = "Pesho direktorcheto",
                Duration = 2,
                Description = "It's a really great production for a little boy readint too much."
                },
                new Movie()
                {
                Id = 2,
                Title = "Rambo",
                Genre = "Action",
                ReleaseDate = new DateTime(1991, 01, 10),
                Director = "Ted Kochev",
                Duration = 3,
                Description = "This is a favorite movie of generations of people that like strong introvert men."
                }
            };
        return movies;
        }
    }
}
