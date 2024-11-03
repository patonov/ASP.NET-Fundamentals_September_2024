using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EntityValidationConstants
    {
        public static class Movie
        {
            public const int MovieTitleMaxLength = 50;

            public const int MovieGenreMaxLength = 15;

        }

        public static class Cinema
        { 
            public const int CinemaNameMaxLength = 50;

            public const int CinemaNameMinLength = 3;
        }
    }
}
