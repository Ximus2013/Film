using System.Data.Entity;
using film.Models;
using film.Infrastructure.Models;

namespace film.Data.DBContext
{
    public class FilmDBContext : DbContext
    {
        public FilmDBContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmActor> FilmActor { get; set; }
    }
}