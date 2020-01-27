using film.Data.DBContext;
using film.Infrastructure.Models;
using film.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace film.Infrastructure.Repository
{
    public class ActorsRepository : IAllActors
    {
        public FilmDBContext context;
        public ActorsRepository()
        {
            context = new FilmDBContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        }
        public IEnumerable<Actor> AllActors()
        {
                return context.Actors;
         
        }
        public Actor GetActorID(int id)
        {
            return context.Actors.FirstOrDefault(x => x.Id == id);
        }
        public void AddActor(Actor model)
        {
            if (model.Id != 0)
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                context.Actors.Add(model);
                context.SaveChanges();
            }
        }
        public void Upload(int id, string path)
        {
            var model = context.Actors.FirstOrDefault(x => x.Id == id);
            model.ActorImg = path;
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteActorView(int id)
        {
            context.FilmActor.RemoveRange(context.FilmActor.Where(x => x.IdActor == id).ToList());
            context.SaveChanges();
            var model = GetActorID(id);
            context.Actors.Remove(model);
            context.SaveChanges();
        }
    }
}