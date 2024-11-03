using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EntityValidationMessages
    {
        public static class Movie 
        {
            public const string TitleRequiredMsg = "Movie title is required.";
            public const string TitleMaxLengthMsg = "Movie title is too long.";

            public const string GenreRequiredMsg = "Genre is required.";
        }
    }
}
