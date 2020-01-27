using film.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace film.Infrastructure.Repository.Interfaces
{
    interface IAllActors
    {
        IEnumerable<Actor> AllActors();
        Actor GetActorID(int id);
        void AddActor(Actor model);
        void Upload(int id, string path);
        void DeleteActorView(int id);
    }
}
