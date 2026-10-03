using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int id { get; set; }
        [StringLength(100)]
        [Required]

        public string Title { get; set; } = string.Empty; // get movie Title

        [StringLength(860)] // roughly 150 words or so 
        [Required]

        public string Synopsis { get; set; }= string.Empty; // get movie description 

        [Required]

        public string Genre { get; set; } = string.Empty; // get movie genre 

        [Required]
        public string Rating { get; set; } = string.Empty; // get movie rating

        [Display(Name = "Runtime (Min)")]

        [MinLength(1)]
        [Required]
        public int RuntimeMinutes { get; set; } // gets the movies runtime in minutes 

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Required]
        public DateTime ReleaseDate { get; set; } // gets movie relase date 

    



    }
}
