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
                return RedirectToAction("Create", "Menu");

            DateTime date = m.StartingDate.AddDays(6);

            if (date.Date.CompareTo(DateTime.Now.Date) == -1)
                return RedirectToAction("Create", "Menu");

            Dictionary<int, byte[]> dict = new Dictionary<int, byte[]>();
            foreach ( Recipe r in m.Recipes)
            {
                dict[r.Id_Recipe] = r.GetImage();
            }
            ViewBag.images = dict;

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
        public ActionResult Random(int rid,string rdata)
        {
            RecipeDAO dao = new RecipeDAO();
            List<Recipe> recipes = dao.LoadRecipeByType(rid);
            Random rnd = new Random();
            List<Recipe> r_menu = new List<Recipe>();
            for (int i=0; i < 14; i++)
            {
                int rand = rnd.Next(recipes.Count);
                r_menu.Add(recipes[rand]);
            }

            DateTime parsedDate = DateTime.Parse(rdata);

            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            int idUser = int.Parse(claim.Value);

            MenuDAO daoM = new MenuDAO();
            daoM.Insert(parsedDate, r_menu, idUser);

            return RedirectToAction("Index", "Menu");
        }


    }
}
