using film.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace film.Infrastructure.Models
{
    public class Actor
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { set; get; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Имя")]
        public string ActorName { set; get; }
        [Display(Name = "Биография")]
        public string Description { get; set; }
        [Display(Name = "ActorImg")]
        public string ActorImg { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateofBirth { set; get; }
        [Display(Name = "Фильмография")]
        public virtual ICollection<Film> Films { get; set; }
        public Actor()
        {
            Films = new List<Film>();
        }
    }
}