using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace film.Infrastructure.Models
{
    public class FilmActor
    {
        public int Id { set; get; }
        public int IdFilm { set; get; }
        public int IdActor { set; get; }
    }
}