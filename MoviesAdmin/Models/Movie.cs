using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int id { get; set; }
        public string Title { get; set; } = string.Empty; // get movie Title
        public string Synopsis { get; set; }= string.Empty; // get movie description 

        public string Genre { get; set; } = string.Empty; // get movie genre 

        public string Rating { get; set; } = string.Empty; // get movie rating 

        public int RuntimeMinutes { get; set; } // gets the movies runtime in minutes 

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; } // gets movie relase date 

    



    }
}
