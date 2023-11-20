﻿namespace Movies_Database.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; }

        public virtual Roles Role { get; set; }

        public List<MovieRating> MovieRatings { get; set; }

    }
}
