using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Olivia.Models;
using Olivia.DataAccess;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            RecipeDAO dao = new RecipeDAO();
            List<Recipe> recipes = dao.LoadRecipes();
            return View(recipes);
        }
    }
}
