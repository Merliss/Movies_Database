namespace Movies_Database.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }

        public int DirectorId { get; set; }
        public int CountryId { get; set; }
        public int GenreId { get; set; }

        public int MovieRatingId { get; set; }

        public virtual Director Director { get; set; }

        public virtual Country Country { get; set; }
        public virtual Genre Genre { get; set; }

        
        public virtual List<MovieRating> MovieRatings { get; set; }


    }
}
