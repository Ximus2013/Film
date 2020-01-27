using System;
using film.Infrastructure.Enums;
using System.Web.Mvc;
using System.Collections.Generic;
using film.Infrastructure.Models;

namespace film.Models
{
    public class Film
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Img { set; get; }
        public DateTime Release { set; get; }
        public bool Isfavorite { set; get; }
        public string Producer { set; get; }
        public virtual ICollection<Actor> Actors { get; set; }
        public int CategoryID { set; get; }
        public virtual Category Category { set; get; }
        public string Description { get; set; }
        public EnumForRelese EnumForRelese { set; get; }
    }
}