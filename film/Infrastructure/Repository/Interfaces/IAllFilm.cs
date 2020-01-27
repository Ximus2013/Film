using film.Infrastructure.Models;
using film.Models;
using film.ViewModels;
using System.Collections.Generic;

namespace film.Interfaces
{
    interface IAllFilm
    {
        IEnumerable<Film> AllFilms(int page = 1, string status = null,string search = null);
        IEnumerable<Film> GetFavoriteFilms();
        IEnumerable<Film> GetCategoryFilm(int id);
        Film GetFilmByID(int id);
        int AddFilmView(FilmViewModel model);
        void EditFilm(Film model);
        void DeleteFilmById(int id);
        void Upload(int id, string path);
        string GetActorsForFilm(int id);
        IEnumerable<Actor> GetActorsForFilm2(int id);
        void SaveActorForFilm(int ActorId, int FilmId);
        int CountFilms(string status = null, string search = null);
    }
}
