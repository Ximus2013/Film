using film.Infrastructure.Models;
using film.Infrastructure.Repository;
using film.Infrastructure.Repository.Interfaces;
using film.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace film.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IAllActors _allactors;
        public ActorsController()
        {
            _allactors = new ActorsRepository();
        }
        public ActionResult AllActorsView()
        {
            var actorsListViewModel = new ActorsListViewModel
            {
                AllActors = _allactors.AllActors().ToList()
            };
            return View(actorsListViewModel);
        }
        [HttpGet]
        public ActionResult AddActorView(int id = -1)
        {
            if (id < 0) {
                var model = new Actor();
                return View(model);
            }
            var actor = _allactors.GetActorID(id);
            return View("AddActorView", actor);
        }

        [HttpPost]
        public ActionResult AddActorView(Actor model)
        {
            var file = Request.Files[0];
            model.ActorImg = file.FileName;
            if (ModelState.IsValid)
            {
                _allactors.AddActor(model);
                if (file.ContentLength != 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(Server.MapPath("~/Images/" + fileName));
                    _allactors.Upload(model.Id, fileName);
                }
                ViewBag.Message = "Saved";
                return RedirectToAction("AllActorsView");

            }
            return View(model);
        }
        public ActionResult DeleteActorView(int id)
        {
            _allactors.DeleteActorView(id);
            return RedirectToAction("AllActorsView");
        }
    }
}