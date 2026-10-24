using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int id { get; set; }


        // foreign key
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        // navigation property
        [ValidateNever]
        public Genre Genre { get; set; } = null!;





        [StringLength(100)]
        [Required]

        public string Title { get; set; } = string.Empty; // get movie Title

       [StringLength(860)] // roughly 150 words or so 
        [Required]

        public string Synopsis { get; set; }= string.Empty; // get movie description 

       

        [Display(Name = "Runtime (Min)")]

        [Range(1, 200)]
        [Required]
        public int RuntimeMinutes { get; set; } // gets the movies runtime in minutes 


        public string ImageUrl { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Required]
        public DateTime ReleaseDate { get; set; } // gets movie relase date 

    



    }
}
