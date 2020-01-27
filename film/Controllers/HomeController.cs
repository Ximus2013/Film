using film.Repository;
using film.Interfaces;
using film.Models;
using film.Reposytory;
using film.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Helpers;
using film.Infrastructure.Repository.Interfaces;
using film.Infrastructure.Repository;
using film.Infrastructure.Models;
using System.Web.Security;

namespace film.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAllFilm _allfilms;
        private readonly IAllCategories _allcategory;
        private readonly IAllActors _allactors;


        public HomeController()
        {
            _allfilms = new FilmsRepository();
            _allcategory = new CategoriesRepository();
            _allactors = new ActorsRepository();
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.Title = "Лучшие Фильмы";
            var asd = _allfilms.GetFavoriteFilms();

            var homeProduct = new HomeViewModels
            {
                favFilm = _allfilms.GetFavoriteFilms().Select(x=>new FilmViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Img = x.Img,
                    Actors = _allfilms.GetActorsForFilm(x.Id),
                    Description = x.Description,
                    CategoryID = x.CategoryID,
                    Isfavorite = x.Isfavorite,
                    CategoryName = x.Category.CategoryName,
                    EnumForRelese = x.EnumForRelese,
                    Producer = x.Producer,
                    Release = x.Release
                })
            };
            return View(homeProduct);
        }
        public ActionResult OneFilm(int id)
        {
            var film = _allfilms.GetFilmByID(id);
            var oneFilm = new OneFilmViewModel
            {
                OneFilm = new FilmViewModel()
                {
                    Id = film.Id,
                    Name = film.Name,
                    CategoryID = film.CategoryID,
                    Description = film.Description,
                    Img = film.Img,
                    EnumForRelese = film.EnumForRelese,
                    Isfavorite = film.Isfavorite,
                    Producer = film.Producer,
                    Release = film.Release,
                    Actors = _allfilms.GetActorsForFilm(id),
                    CategoryName = film.Category.CategoryName
                }
        };

            if (oneFilm.OneFilm == null)
                return HttpNotFound();

            ViewBag.Title = oneFilm.OneFilm.Name;
            return View(oneFilm);
        }

        public ActionResult CategoryFilm(int id)
        {
            var category = _allcategory.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            var categoryViewModel = new CategoryFilmsViewModel
            {
                CategoryFilm = _allfilms.GetCategoryFilm(id).Select(x=>new FilmViewModel() {
                    Id = x.Id,
                    Name = x.Name,
                    Img = x.Img,
                    Actors = _allfilms.GetActorsForFilm(x.Id),
                    Description = x.Description,
                    CategoryID = x.CategoryID,
                    Isfavorite = x.Isfavorite,
                    CategoryName = x.Category.CategoryName,
                    EnumForRelese = x.EnumForRelese,
                    Producer = x.Producer,
                    Release = x.Release
                }),
                CategoryName = category.CategoryName,
                CategoryDescription = category.DescCategory,
                CategoryId = category.Id
            };
            ViewBag.Title = categoryViewModel.CategoryName;
            return View(categoryViewModel);
        }

        public ActionResult ListAllFilms(int page = 1,string status=null,string search = null)
        {
            ViewBag.Search = search;
            ViewBag.Status = status;
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = FilmsRepository.pageSize,
                TotalItems = _allfilms.CountFilms(status,search),
                Search = search,
                Status = status
            };
            var filmList = new FilmListViewModel
            {
                AllFilms = _allfilms.AllFilms(page,pageInfo.Status, pageInfo.Search).Select(x => new FilmViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Img = x.Img,
                    Actors = _allfilms.GetActorsForFilm(x.Id),
                    Description = x.Description,
                    CategoryID = x.CategoryID,
                    Isfavorite = x.Isfavorite,
                    CategoryName = x.Category.CategoryName,
                    EnumForRelese = x.EnumForRelese,
                    Producer = x.Producer,
                    Release = x.Release,
                }),
            };


            filmList.PageInfo = pageInfo;

            return View(filmList);

        }
        //[Authorize(Roles = "Admin")]
        public ActionResult AddFilmView()
        {
            var category = new SelectList(_allcategory.AllCategories, "Id", "CategoryName");
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        public ActionResult AddFilmView(FilmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var filmid = _allfilms.AddFilmView(model);


                var ActorsString = model.Actors;
                if (String.IsNullOrEmpty(ActorsString)==false) { 
                String[] Numbers = ActorsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var element in Numbers)
                {
                    int result;
                    bool success = Int32.TryParse(element, out result);
                    _allfilms.SaveActorForFilm(result, filmid);
                }
                }
                return RedirectToAction("ListAllFilms");
            }
            var category = new SelectList(_allcategory.AllCategories, "Id", "CategoryName");
            ViewBag.Category = category;
            return View(model);
        }
        public ActionResult EditFilmView()
        {
            var filmNames = new SelectList(_allfilms.AllFilms(), "Id", "Name");
            ViewBag.Film = filmNames;
            return View();
        }
        [HttpGet]
        public ActionResult EditFilmForm(int id)
        {
            var category = new SelectList(_allcategory.AllCategories, "Id", "CategoryName");
            ViewBag.Category = category;
            var film = _allfilms.GetFilmByID(id);
            var filmViewModel = new FilmViewModel()
            {
                Id = film.Id,
                Name = film.Name,
                CategoryID = film.CategoryID,
                Description = film.Description,
                Img = film.Img,
                EnumForRelese = film.EnumForRelese,
                Isfavorite = film.Isfavorite,
                Producer = film.Producer,
                Release = film.Release,
                Actors = _allfilms.GetActorsForFilm(id),
                CategoryName = film.Category.CategoryName
            };
            return PartialView("EditFilmForm", filmViewModel);
        }

        [HttpPost]
        public ActionResult EditFilmForm(Film model)
        {
            var category = new SelectList(_allcategory.AllCategories, "Id", "CategoryName");
            ViewBag.Category = category;
            if (ModelState.IsValid)
            {
                _allfilms.EditFilm(model);
                ViewBag.Message = "Saved";
                return PartialView(model);
            }
            return PartialView(model);
        }

 
        public ActionResult DeleteFilmView(int id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("Test exception");
                
                _allfilms.DeleteFilmById(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Upload(int id)
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Images/" + fileName));
                    _allfilms.Upload(id, fileName);
                }
            }
            return Json("файл загружен");
        }
        public ActionResult AddActorsForFilm(int? id)
        {
            var Actors = new List<Actor>();
            if (id.HasValue)
            {
                var IDs = _allfilms.GetActorsForFilm2(id.Value).Select(x => x.Id).ToList();
                var list12 = _allactors.AllActors();
                Actors = list12.Where(item => !IDs.Contains(item.Id)).ToList();
            }
            else
                Actors = _allactors.AllActors().ToList();

            var actors = new SelectList(Actors, "Id", "ActorName");
            ViewBag.Actors = actors;
            //ViewBag.Actor = ActorName;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddActorsForFilm(int ActorId, int FilmId)
        {
            var actors = new SelectList(_allactors.AllActors(), "Id", "ActorName");
            ViewBag.Actors = actors;

            //if (ModelState.IsValid)
            //{
            //    _allfilms.EditFilm(model);
            //    ViewBag.Message = "Saved";
            //    return PartialView(model);
            //}
            _allfilms.SaveActorForFilm(ActorId, FilmId);
            return EditFilmForm(FilmId);
        }
    }
}