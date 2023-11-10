using System.Xml.Linq;
using Movies_Database.Entities;

namespace Movies_Database
{
    public class MovieSeeder
    {

        private readonly MovieDbContext _dbContext;
        public MovieSeeder(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                //var movies = GetMovies();
               
                if (!_dbContext.Movies.Any())
                {
                    var movies = GetMovies();
                    _dbContext.Movies.AddRange(movies);
                    _dbContext.SaveChanges();
                }
            }
        }


        IEnumerable<Movie> GetMovies()
        {

            var countries = new List<Country>
        {
            new Country { Name = "United States" },
            new Country { Name = "United Kingdom" },
            new Country { Name = "Brazil" },
            new Country { Name = "France" },
            new Country { Name = "China" },
            new Country { Name = "Spain" },
            new Country { Name = "Japan" },
            new Country { Name = "Germany" },
            // Dodaj więcej krajów, jeśli potrzebujesz
        };

            var genres = new List<Genre>
        {
            new Genre { Name = "Crime" },
            new Genre { Name = "Drama" },
            new Genre { Name = "Thriller" },
            new Genre { Name = "Comedy" },
            new Genre { Name = "Mystery" },
            new Genre { Name = "Sci-Fi/Action" },
            new Genre { Name = "Drama/Romance" },
            new Genre { Name = "Action/Crime/Drama" },
            new Genre { Name = "Adventure" },
            new Genre { Name = "Fantasy" },
            new Genre { Name = "Crime/Drama" },
            new Genre { Name = "Comedy/Romance" },
            new Genre { Name = "Action/Drama/Romance" },
            new Genre { Name = "Drama/Fantasy/War" },
            new Genre { Name = "Animation/Adventure/Family" },
            new Genre { Name = "Crime/Drama/Thriller" },
            // Dodaj więcej gatunków, jeśli potrzebujesz
        };

            var directors = new List<Director>
        {
            new Director { Name = "Quentin", Surname = "Tarantino" },
            new Director { Name = "Francis Ford", Surname = "Coppola" },
            new Director { Name = "Martin", Surname = "Scorsese" },
            new Director { Name = "Brian", Surname = "De Palma" },
            new Director { Name = "Michael", Surname = "Mann" },
            new Director { Name = "Guy", Surname = "Ritchie" },
            new Director { Name = "Bryan", Surname = "Singer" },
            new Director { Name = "Ridley", Surname = "Scott" },
            new Director { Name = "Robert", Surname = "De Niro" },
            new Director { Name = "Antoine", Surname = "Fuqua" },
            new Director { Name = "David", Surname = "Cronenberg" },
            new Director { Name = "Christopher", Surname = "Nolan" },
            new Director { Name = "Frank", Surname = "Darabont" },
            new Director { Name = "Robert", Surname = "Zemeckis" },
            new Director { Name = "Brothers", Surname = "The Wachowskis" },
            new Director { Name = "Peter", Surname = "Jackson" },
            new Director { Name = "Steven", Surname = "Spielberg" },
            new Director { Name = "Michel", Surname = "Gondry" },
            new Director { Name = "Fernando", Surname = "Meirelles" },
            new Director { Name = "Jean-Pierre", Surname = "Jeunet" },
            new Director { Name = "Ang", Surname = "Lee" },
            new Director { Name = "Guillermo", Surname = "del Toro" },
            new Director { Name = "Hayao", Surname = "Miyazaki" },
            new Director { Name = "Tom", Surname = "Tykwer" },
            // Dodaj więcej reżyserów, jeśli potrzebujesz
        };

            var movies = new List<Movie>
        {
            new Movie
            {
                Name = "Pulp Fiction",
                Year = "1994",
                Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                Director = directors.Single(d => d.Name == "Quentin" && d.Surname == "Tarantino"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "The Godfather",
                Year = "1972",
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                Director = directors.Single(d => d.Name == "Francis Ford" && d.Surname == "Coppola"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },
            new Movie
            {
                Name = "Goodfellas",
                Year = "1990",
                Description = "A young man grows up in the mob and works hard to advance himself through the ranks, but his life of crime comes at a high cost.",
                Director = directors.Single(d => d.Name == "Martin" && d.Surname == "Scorsese"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "Reservoir Dogs",
                Year = "1992",
                Description = "After a simple jewelry heist goes terribly wrong, the surviving criminals begin to suspect that one of them is a police informant.",
                Director = directors.Single(d => d.Name == "Quentin" && d.Surname == "Tarantino"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Thriller")
            },
            new Movie
            {
                Name = "The Departed",
                Year = "2006",
                Description = "An undercover cop and a mole in the police force attempt to identify each other while infiltrating an Irish gang in Boston.",
                Director = directors.Single(d => d.Name == "Martin" && d.Surname == "Scorsese"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Thriller")
            },
            new Movie
            {
                Name = "Scarface",
                Year = "1983",
                Description = "In Miami, a determined Cuban immigrant takes over a drug cartel and succumbs to greed.",
                Director = directors.Single(d => d.Name == "Brian" && d.Surname == "De Palma"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },
            new Movie
            {
                Name = "Heat",
                Year = "1995",
                Description = "A group of professional bank robbers start to feel the heat from the police when they unknowingly leave a clue at their latest heist.",
                Director = directors.Single(d => d.Name == "Michael" && d.Surname == "Mann"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },
            new Movie
            {
                Name = "Casino",
                Year = "1995",
                Description = "A tale of greed, deception, money, power, and murder occur between two best friends: a mafia enforcer and a casino executive.",
                Director = directors.Single(d => d.Name == "Martin" && d.Surname == "Scorsese"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "The Untouchables",
                Year = "1987",
                Description = "Federal Agent Eliot Ness sets out to stop Al Capone; because of rampant corruption, he assembles a small, hand-picked team.",
                Director = directors.Single(d => d.Name == "Brian" && d.Surname == "De Palma"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "Lock, Stock, and Two Smoking Barrels",
                Year = "1998",
                Description = "A botched card game in London triggers four friends, thugs, weed-growers, hard gangsters, loan sharks, and debt collectors.",
                Director = directors.Single(d => d.Name == "Guy" && d.Surname == "Ritchie"),
                Country = countries.Single(c => c.Name == "United Kingdom"),
                Genre = genres.Single(g => g.Name == "Comedy")
            },
            new Movie
            {
                Name = "The Usual Suspects",
                Year = "1995",
                Description = "A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.",
                Director = directors.Single(d => d.Name == "Bryan" && d.Surname == "Singer"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Mystery")
            },
            new Movie
            {
                Name = "American Gangster",
                Year = "2007",
                Description = "In 1970s America, a detective works to bring down the drug empire of Frank Lucas, a heroin kingpin from Manhattan, who is smuggling the drug into the country from the Far East.",
                Director = directors.Single(d => d.Name == "Ridley" && d.Surname == "Scott"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },
            new Movie
            {
                Name = "A Bronx Tale",
                Year = "1993",
                Description = "A father becomes worried when a local gangster befriends his son in the Bronx in the 1960s.",
                Director = directors.Single(d => d.Name == "Robert" && d.Surname == "De Niro"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "Training Day",
                Year = "2001",
                Description = "On his first day on the job as a narcotics officer, a rookie cop works with a rogue detective who isn't what he appears.",
                Director = directors.Single(d => d.Name == "Antoine" && d.Surname == "Fuqua"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },
            new Movie
            {
                Name = "Eastern Promises",
                Year = "2007",
                Description = "A Russian teenager living in London who dies during childbirth leaves clues to a midwife in her journal that could tie her child to a rape involving a violent Russian mob family.",
                Director = directors.Single(d => d.Name == "David" && d.Surname == "Cronenberg"),
                Country = countries.Single(c => c.Name == "United Kingdom"),
                Genre = genres.Single(g => g.Name == "Crime")
            },
            new Movie
            {
                Name = "Snatch",
                Year = "2000",
                Description = "Unscrupulous boxing promoters, violent bookmakers, a Russian gangster, incompetent amateur robbers, and supposedly Jewish jewelers fight to track down a priceless stolen diamond.",
                Director = directors.Single(d => d.Name == "Guy" && d.Surname == "Ritchie"),
                Country = countries.Single(c => c.Name == "United Kingdom"),
                Genre = genres.Single(g => g.Name == "Comedy")
            },
            new Movie
            {
                Name = "Inception",
                Year = "2010",
                Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                Director = directors.Single(d => d.Name == "Christopher" && d.Surname == "Nolan"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Sci-Fi/Action")
            },
            new Movie
            {
                Name = "The Shawshank Redemption",
                Year = "1994",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                Director = directors.Single(d => d.Name == "Frank" && d.Surname == "Darabont"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama")
            },

            new Movie
            {
                Name = "Forrest Gump",
                Year = "1994",
                Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events.",
                Director = directors.Single(d => d.Name == "Robert" && d.Surname == "Zemeckis"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama/Romance")
            },
            new Movie
            {
                Name = "The Matrix",
                Year = "1999",
                Description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                Director = directors.Single(d => d.Surname == "The Wachowskis"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Sci-Fi/Action")
            },
            new Movie
            {
                Name = "Eternal Sunshine of the Spotless Mind",
                Year = "2004",
                Description = "When their relationship turns sour, a couple undergoes a medical procedure to have each other erased from their memories.",
                Director = directors.Single(d => d.Name == "Michel" && d.Surname == "Gondry"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Drama/Romance")
            },
            new Movie
            {
                Name = "The Dark Knight",
                Year = "2008",
                Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                Director = directors.Single(d => d.Name == "Christopher" && d.Surname == "Nolan"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Action/Crime/Drama")
            },
            new Movie
            {
                Name = "Indiana Jones: Raiders of the Lost Ark",
                Year = "1981",
                Description = "Archaeologist and adventurer Indiana Jones is hired by the U.S. government to find the Ark of the Covenant before the Nazis.",
                Director = directors.Single(d => d.Name == "Steven" && d.Surname == "Spielberg"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Adventure")
            },
            new Movie
            {
                Name = "The Lord of the Rings: The Fellowship of the Ring",
                Year = "2001",
                Description = "A young hobbit, Frodo, who has found the One Ring, begins his journey with eight companions to Mount Doom, the only place where it can be destroyed.",
                Director = directors.Single(d => d.Name == "Peter" && d.Surname == "Jackson"),
                Country = countries.Single(c => c.Name == "United States"),
                Genre = genres.Single(g => g.Name == "Fantasy")
            },
            new Movie
            {
                Name = "City of God",
                Year = "2002",
                Description = "Two boys growing up in a violent neighborhood of Rio de Janeiro take different paths: one becomes a photographer, the other a drug dealer.",
                Director = directors.Single(d => d.Name == "Fernando" && d.Surname == "Meirelles"),
                Country = countries.Single(c => c.Name == "Brazil"),
                Genre = genres.Single(g => g.Name == "Crime/Drama")
            },
            new Movie
            {
                Name = "Amélie",
                Year = "2001",
                Description = "Amélie, an innocent and naive girl in Paris, decides to help those around her and, along the way, discovers love.",
                Director = directors.Single(d => d.Name == "Jean-Pierre" && d.Surname == "Jeunet"),
                Country = countries.Single(c => c.Name == "France"),
                Genre = genres.Single(g => g.Name == "Comedy/Romance")
            },
            new Movie
            {
                Name = "Crouching Tiger, Hidden Dragon",
                Year = "2000",
                Description = "Two warriors in pursuit of a stolen sword and a notorious fugitive cross paths with a beautiful, mysterious swordswoman.",
                Director = directors.Single(d => d.Name == "Ang" && d.Surname == "Lee"),
                Country = countries.Single(c => c.Name == "China"),
                Genre = genres.Single(g => g.Name == "Action/Drama/Romance")
            },
            new Movie
            {
                Name = "Pan's Labyrinth",
                Year = "2006",
                Description = "In post-Civil War Spain, a young girl encounters a mysterious labyrinth, revealing her dark and magical destiny.",
                Director = directors.Single(d => d.Name == "Guillermo" && d.Surname == "del Toro"),
                Country = countries.Single(c => c.Name == "Spain"),
                Genre = genres.Single(g => g.Name == "Drama/Fantasy/War")
            },
            new Movie
            {
                Name = "Spirited Away",
                Year = "2001",
                Description = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.",
                Director = directors.Single(d => d.Name == "Hayao" && d.Surname == "Miyazaki"),
                Country = countries.Single(c => c.Name == "Japan"),
                Genre = genres.Single(g => g.Name == "Animation/Adventure/Family")
            },
            new Movie
            {
                Name = "Run Lola Run",
                Year = "1998",
                Description = "After a botched money delivery, Lola has 20 minutes to come up with 100,000 Deutschmarks.",
                Director = directors.Single(d => d.Name == "Tom" && d.Surname == "Tykwer"),
                Country = countries.Single(c => c.Name == "Germany"),
                Genre = genres.Single(g => g.Name == "Crime/Drama/Thriller")
            },


            };

            return movies;

        }
    }
}
