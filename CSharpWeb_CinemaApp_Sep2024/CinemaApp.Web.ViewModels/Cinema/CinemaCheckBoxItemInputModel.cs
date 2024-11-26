using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Common.EntityValidationConstants.Cinema;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaCheckBoxItemInputModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(CinemaNameMaxLength)]
        [MinLength(CinemaNameMinLength)]
        public string Name { get; set; } = null!;

        public bool IsSelected { get; set; }
    }
}
