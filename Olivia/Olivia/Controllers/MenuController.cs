using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Olivia.DataAccess;
using Olivia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class MenuController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            int idUser = int.Parse(claim.Value);

            MenuDAO dao = new MenuDAO();
            Menu m = dao.getLastestMenu(idUser);
            if (m == null) // falta verificar a data
                return RedirectToAction("Index", "User");

            DateTime date = m.StartingDate.AddDays(6);

            if (date.Date.CompareTo(DateTime.Now.Date) == -1)
                return RedirectToAction("Index", "User");

            return View(m.OrderedRecipes());
        }

        [Authorize]
        public IActionResult Create()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            int idUser = int.Parse(claim.Value);
            MenuDAO dao = new MenuDAO();
            Menu m = dao.getLastestMenu(idUser);


            if (m == null)
                return View();

            DateTime date = m.StartingDate.AddDays(6);
            if (date.Date.CompareTo(DateTime.Now.Date) == -1)
            {
                return View();
            }
            return RedirectToAction("Index", "Menu");
        }

        [Authorize]
        public IActionResult Random(int id,string date)
        {
            RecipeDAO dao = new RecipeDAO();
            List<Recipe> recipes = dao.LoadRecipeByType(id);
            Random rnd = new Random();
            List<Recipe> r_menu = new List<Recipe>();
            for (int i=0; i < 14; i++)
            {
                int rand = rnd.Next(recipes.Count);
                r_menu.Add(recipes[rand]);
            }

            DateTime parsedDate = DateTime.Parse(date);
            return View();
        }
    }
}
