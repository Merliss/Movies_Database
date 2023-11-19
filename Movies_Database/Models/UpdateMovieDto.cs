using System.ComponentModel.DataAnnotations;

namespace Movies_Database.Models
{
    public class UpdateMovieDto
    {

        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public string Year { get; set; }

       

    }
}