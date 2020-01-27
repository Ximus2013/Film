using film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace film.ViewModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public string Search { get; set;}
        public string Status { get; set; }
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class FilmListViewModel
    {
        public IEnumerable<FilmViewModel> AllFilms { get; set; }
        public string FilmCategory { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}