namespace Movies_Database.Models
{
    public class MovieQuery
    {
        public int PageNumber { get; set; }

        public string SearchPhrase { get; set; }

        public int PageSize { get; set; }

        public string? SortBy { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}
