namespace MoviesAdmin.Models
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty; // get movie Title
        public string Syopsis { get; set; }= string.Empty; // get movie description 

        public string Genre { get; set; } = string.Empty; // get movie genre 

        public string Rating { get; set; } = string.Empty; // get movie rating 

        public int RuntimeMinutes { get; set; } // gets the movies runtime in minutes 

        public DateTime ReleaseDate { get; set; } // gets movie relase date 

    



    }
}
