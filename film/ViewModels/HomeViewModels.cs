using film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace film.ViewModels
{
    public class HomeViewModels
    {
        public IEnumerable<FilmViewModel> favFilm { get; set;}
    }
}