using film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace film.ViewModels
{
    public class CategoryFilmsViewModel
    {
        public IEnumerable<FilmViewModel> CategoryFilm { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryId { get; set; }
    }
}