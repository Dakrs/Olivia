using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Olivia.DataAccess;
using Olivia.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class RecipeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Details(int id)
        {

            RecipeDAO dao = new RecipeDAO();
            Recipe recipe = dao.FindById(id);

            return View(recipe);
        }
    }
}
