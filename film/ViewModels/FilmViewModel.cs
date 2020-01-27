using film.Infrastructure.Enums;
using film.Infrastructure.Models;
using film.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace film.ViewModels
{
    public class FilmViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { set; get; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Название фильма")]
        public string Name { set; get; }
        public string Img { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата проката")]
        public DateTime Release { set; get; }
        [Display(Name = "Любимый фильм")]
        public bool Isfavorite { set; get; }
        [Display(Name = "Режиссер")]
        public string Producer { set; get; }
        [Display(Name = "Актеры")]
        public string Actors { set; get; }
        public int CategoryID { set; get; }
        [Display(Name = "Про что фильм")]

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public EnumForRelese EnumForRelese { set; get; }
    }
}