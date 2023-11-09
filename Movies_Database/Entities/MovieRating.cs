namespace Movies_Database.Entities
{
    public class MovieRating
    {

        public int Id { get; set; }
        
        public int Rating { get; set; }

        public bool IsFavorite { get; set; }
        
        public int MovieId { get; set; }

        public int UserID { get; set; }


        public virtual Movie Movie { get; set; }

        public virtual Users Users { get; set; }

    }
}
