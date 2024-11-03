using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.Models
{
    public class CinemaMovie
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; } = null!; 
    }
}
