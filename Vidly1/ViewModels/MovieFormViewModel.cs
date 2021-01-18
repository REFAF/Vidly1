using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly1.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly1.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int? Id { get; set; } //ex5

        [Required]
        [StringLength(255)]
        public string Name { get; set; } //ex5

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }//ex5

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }//ex5

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }//ex5

        //public Movie Movie { get; set; } // delete it in ex5
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";

                //if (Movie != null && Movie.Id != 0) delete it in ex5
                //return "Edit Movie";

                //return "New Movie";
            }
        }

        //ex5
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}