using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }


        // foriegn key 

        public int MovieId { get; set; }

        // navigation property 

        [ValidateNever]
        public Movie Movie { get; set; } = null!; 



        public string Description { get; set; } = string.Empty;

        public int Rating { get; set; }

        public bool IsPublished { get; set; }

        public string CreatedBy { get; set; } = string.Empty; // userId

        
        public DateTime CreatedDate { get; set; }

    }
}
