//using film.Interfaces;
//using film.Repository;
//using film.Models;
//using film.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using film.Reposytory;


//namespace film.Controllers
//{
//    public class FilmsController : Controller
//    {
//        private readonly IAllFilm _allFilms;
//        private readonly IAllCategories _allCategories;
//        public FilmsController()
//        {
//            _allFilms = new FilmsRepository();
//            _allCategories = new CategoriesRepository();
//        }



//        [Route("Films/List")]
//        [Route("Films/List{category}")]
//        public ActionResult List(string category)
//        {
//            var _category = category;
//            IEnumerable<Film> film = null;
//            var filmCategory = "";
//            if (string.IsNullOrEmpty(category))
//            {
//                film = _allFilms.AllFilms().OrderBy(i => i.Id);
//            }
//            else
//            {
//                if (string.Equals("cpu", category, StringComparison.OrdinalIgnoreCase))
//                {
//                    film = _allFilms.AllFilms().Where(i => i.Category.CategoryName.Equals("Процессоры")).OrderBy(i => i.Id);
//                }
//                else if (string.Equals("gpu", category, StringComparison.OrdinalIgnoreCase))
//                {
//                    film = _allFilms.AllFilms().Where(i => i.Category.CategoryName.Equals("Видеокарты")).OrderBy(i => i.Id);
//                }

//                filmCategory = _category;

//            }

//            var filmObj = new FilmListViewModel
//            {
//                AllFilms = film,
//                FilmCategory = filmCategory
//            };

//            return View(filmObj);
//        }


//        // GET: api/Films
//        //public IEnumerable<string> Get()
//        //{
//        //    return new string[] { "value1", "value2" };
//        //}

//        //// GET: api/Films/5
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}

//        //// POST: api/Films
//        //public void Post([FromBody]string value)
//        //{
//        //}

//        //// PUT: api/Films/5
//        //public void Put(int id, [FromBody]string value)
//        //{
//        //}

//        //// DELETE: api/Films/5
//        //public void Delete(int id)
//        //{
//        //}
//    }
//}
