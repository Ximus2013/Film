using film.Data.DBContext;
using film.Interfaces;
using film.Repository;
using film.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Data.Entity;
using film.Infrastructure.Models;
using film.ViewModels;
using film.Infrastructure.Enums;

namespace film.Reposytory
{
    public class FilmsRepository : IAllFilm
    {
        public FilmDBContext context;

        public static int pageSize = 3;

        public FilmsRepository()
        {
            context = new FilmDBContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        }
        private readonly IAllCategories _categoryProducts = new CategoriesRepository();

        public IEnumerable<Film> AllFilms(int page = 1, string status = null,string search = null)
        {
            var result = (IEnumerable<Film>)context.Films;
            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(x => x.Name!=null&&x.Name.Contains(search) || x.Description!=null&&x.Description.Contains(search));
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "No Released")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.NoReleased);
                }
                if (status == "Released")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.Released);
                }
                if (status == "Finished")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.Finished);
                }
            }

            if (!string.IsNullOrEmpty(status) && status == "OrderByDate")
            {
                result = result.OrderBy(x => x.Release);
            }
            else
                result = result.OrderBy(x => x.Id);

            return result.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int CountFilms(string status = null, string search = null)
        {
            var result = (IEnumerable<Film>)context.Films;
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    return context.Films.Where(x => x.Name != null && x.Name.Contains(search) || x.Description != null && x.Description.Contains(search)).Count();
            //}
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "No Released")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.NoReleased);
                }
                if (status == "Released")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.Released);
                }
                if (status == "Finished")
                {
                    result = result.Where(x => x.EnumForRelese == EnumForRelese.Finished);
                }
            }
            if (!string.IsNullOrEmpty(status) && status == "OrderByDate")
            {
                result = result.OrderBy(x => x.Release);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                return result.Where(x => x.Name != null && x.Name.Contains(search) || x.Description != null && x.Description.Contains(search)).Count();
            }
            else
                result = result.OrderBy(x => x.Id);

            return result.Count();

        }

        public IEnumerable<Film> GetFavoriteFilms()
        {
            return context.Films.Where(x => x.Isfavorite).ToList();
        }

        public IEnumerable<Film> GetCategoryFilm(int id)
        {
            return context.Films.Where(x => x.CategoryID == id).ToList();
        }

        public Film GetFilmByID(int id)
        {
            return context.Films.FirstOrDefault(x => x.Id == id);
        }
        public int AddFilmView(FilmViewModel filmViewModel)
        {
            var model = new Film()
            {
                Id = filmViewModel.Id,
                Name = filmViewModel.Name,
                CategoryID = filmViewModel.CategoryID,
                Description = filmViewModel.Description,
                Img = filmViewModel.Img,
                EnumForRelese = filmViewModel.EnumForRelese,
                Isfavorite = filmViewModel.Isfavorite,
                Producer = filmViewModel.Producer,
                Release = filmViewModel.Release
            };

            model.CategoryID = 1;
            context.Films.Add(model);
            context.SaveChanges();
            return model.Id;
        }
        public void EditFilm(Film model)
        {
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteFilmById(int id)
        {
            context.FilmActor.RemoveRange(context.FilmActor.Where(x => x.IdFilm == id).ToList());
            context.SaveChanges();

            //for (var j = 0; j < count; j++)
            //{
            //    var ActorForThisFilm_From_FilmActor = GetActorForFilmToDeleteFilmById(id);
            //    context.FilmActor.Remove(ActorForThisFilm_From_FilmActor);
            //    context.SaveChanges();
            //}
            var model = GetFilmByID(id);
            context.Films.Remove(model);
            context.SaveChanges();
        }
        public void Upload(int id, string path)
        {
            var model = context.Films.FirstOrDefault(x => x.Id == id);
            model.Img = path;
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public string GetActorsForFilm(int id)
        {
            //var model = context.Films.Select(x => x.Id == id);
            var ActorsIDs = context.FilmActor.Where(x => x.IdFilm == id).Select(x => x.IdActor).ToList();
            return string.Join(",", context.Actors.Where(x => ActorsIDs.Contains(x.Id)).Select(x => x.ActorName));
        }
        public IEnumerable<Actor> GetActorsForFilm2(int id)
        {
            ////var model = context.Films.Select(x => x.Id == id);
            //var ActorsIDs = context.FilmActor.Where(x => x.IdFilm == id).Select(x => x.IdActor).ToList();
            //return context.Actors.Where(x => ActorsIDs.Contains(x.Id)).ToList();
            var FilmActors = context.FilmActor.ToList();
            var ActorsList = context.Actors.ToList();
            var ActorsListLast = new List<Actor>();
            for (var i=0; i < FilmActors.Count; i++)
            {
                if (FilmActors[i].IdFilm == id)
                {
                    foreach (var item_IdActor in ActorsList)
                    {
                        if (item_IdActor.Id == FilmActors[i].IdActor)
                        {
                            ActorsListLast.Add(item_IdActor);
                            break;
                        }
                    }
                }
            }
            return ActorsListLast.ToList();
        }

        public void SaveActorForFilm(int ActorId, int FilmId)
        {
            var model = new FilmActor();
            model.IdActor = ActorId;
            model.IdFilm = FilmId;
            context.FilmActor.Add(model);
            context.SaveChanges();
        }
    }
}