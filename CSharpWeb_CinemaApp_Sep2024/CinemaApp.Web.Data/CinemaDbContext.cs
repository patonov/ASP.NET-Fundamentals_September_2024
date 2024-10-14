using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.Models;
using System.Reflection;

namespace CinemaApp.Web.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext() 
        { 
        
        }

        public CinemaDbContext(DbContextOptions options) 
            : base(options) 
        { 
        
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
