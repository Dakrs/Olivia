using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Olivia.Models;
using Olivia.DataAccess;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class CookController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Index", "User");
        } 

        public IActionResult Shopping(int id)
        {
            RecipeDAO dao = new RecipeDAO();
            Recipe recipe = dao.FindById(id);
            return View(recipe);
        }
    }
}
