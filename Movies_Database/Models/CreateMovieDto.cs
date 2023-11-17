using System.ComponentModel.DataAnnotations;

namespace Movies_Database.Models
{
    public class CreateMovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public string Year { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [Required]
        public string DirectorSurname { get; set; }

        [Required]
        [MaxLength(40)]
        public string Country { get; set; }

        [Required]
        [MaxLength(35)]
        public string Genre { get; set; }



    }
}
