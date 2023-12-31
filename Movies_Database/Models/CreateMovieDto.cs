﻿using System.ComponentModel.DataAnnotations;

namespace Movies_Database.Models
{
    public class CreateMovieDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public string DirectorName { get; set; }

        public string DirectorSurname { get; set; }

        public string Country { get; set; }

        public string Genre { get; set; }



    }
}
