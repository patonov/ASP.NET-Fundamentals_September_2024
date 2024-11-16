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
    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => new { cm.MovieId, cm.CinemaId });

            builder.HasOne(cm => cm.Movie).WithMany(m => m.MovieCinemas).HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.Cinema).WithMany(c => c.CinemaMovies).HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateCinemasMovies());
        }

        private IEnumerable<CinemaMovie> GenerateCinemasMovies() 
        {
            IEnumerable<CinemaMovie> cinemaMovies = new List<CinemaMovie>()
            {
                new CinemaMovie()
                {
                CinemaId = 1,
                MovieId = 1,
                },
                new CinemaMovie()
                { 
                CinemaId = 2,
                MovieId = 2,
                }
            };

            return cinemaMovies;
        }
    }
}
