using Movies_Database.Entities;

namespace Movies_Database.Models
{
    public class MovieRatingDto
    {

        public int Id { get; set; }

        public int Rating { get; set; }

        public bool IsFavorite { get; set; }

        public string MovieName {  get; set; }

        public int UsersId { get; set; }

        public string UserName { get; set; }
        
    }
}
