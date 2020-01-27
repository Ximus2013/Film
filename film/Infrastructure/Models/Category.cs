using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace film.Models
{
    public class Category
    {

            public int Id { set; get; }
            public string CategoryName { set; get; }
            public string DescCategory { set; get; }
            public List<Film> Film { set; get; }

        
    }
}