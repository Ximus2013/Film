using film.Data.DBContext;
using film.Interfaces;
using film.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace film.Repository
{
    public class CategoriesRepository : IAllCategories
    {
        private FilmDBContext context;
        public CategoriesRepository()
        {
            context = new FilmDBContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        }
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return context.Categories;
            }
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(x => x.Id == id);
        }
    }
}