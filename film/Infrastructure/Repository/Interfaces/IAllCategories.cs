using film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace film.Interfaces
{
    interface IAllCategories
    {
        IEnumerable<Category> AllCategories { get; }
        Category GetCategoryById(int id);
    }
}
