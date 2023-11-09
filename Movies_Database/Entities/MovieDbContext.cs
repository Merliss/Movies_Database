using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Movies_Database.Entities
{
    public class MovieDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MovieDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Movie> Movies { get; set;}
        public DbSet<Country>  Countries { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Genre> Genres { get; set;}

        public DbSet<Roles> Roles { get; set;}

        public DbSet<MovieRating> MovieRatings { get; set;}

        public DbSet<Users> Users { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Movie>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(150);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .HasMaxLength(30);

            modelBuilder.Entity<Genre>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Genre>()
                .Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Country>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Country>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Director>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Director>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Director>()
                .Property(x => x.Surname)
                .IsRequired();

            modelBuilder.Entity<MovieRating>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<MovieRating>()
                .Property(x => x.MovieId)
                .IsRequired();

            modelBuilder.Entity<MovieRating>()
                .Property(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<MovieRating>()
                .Property(x => x.IsFavorite)
                .HasDefaultValue(false);

            modelBuilder.Entity<Roles>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Roles>()
                .Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Users>()
                .Property(x => x.Username)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        }


    }
    
}
