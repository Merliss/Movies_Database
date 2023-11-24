using System.ComponentModel.DataAnnotations;

namespace Movies_Database.Models
{
    public class CreateMovieRatingDto
    {
        public int Id { get; set; }

        [Required]
        public int Rating { get; set; }


        public bool IsFavorite { get; set; }

        [Required]
        public string MovieName { get; set; }

        

    }
}
