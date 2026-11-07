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


        [Required]
        [StringLength(1000)]

        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating (1-5 stars)")]
        public int Rating { get; set; }


        [Required]
        public bool IsPublished { get; set; } = false;

        public string CreatedBy { get; set; } = string.Empty; // userId


        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
