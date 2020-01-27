using film.Infrastructure.Models;
using System.Collections.Generic;

namespace film.ViewModels
{
    public class ActorsListViewModel
    {
        public IEnumerable<Actor> AllActors { get; set; }
    }
}