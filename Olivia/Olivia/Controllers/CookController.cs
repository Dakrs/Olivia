using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Olivia.Models;
using Olivia.DataAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class CookController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var id = TempData["Id"];
            if (id == null)
                return RedirectToAction("Index", "User");

            int id_recipe = int.Parse(id.ToString());
            RecipeDAO dao = new RecipeDAO();
            Recipe r = dao.FindById(id_recipe);

            if (r == null)
                return RedirectToAction("Index", "User");

            ViewBag.Image = r.GetImage();

            return View(r);
        } 

        [Authorize]
        public IActionResult Shopping(int id)
        {
            RecipeDAO dao = new RecipeDAO();
            Recipe recipe = dao.FindById(id);

            ViewBag.Image = recipe.GetImage();

            return View(recipe);
        }

        [Authorize]
        public IActionResult History()
        {
            var id_temp_rec = TempData["Id"];
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);

            if (id_temp_rec == null || claim == null)
            {
                return RedirectToAction("Index", "User");
            }
            int id_recipe = int.Parse(id_temp_rec.ToString());
            string idUser = claim.Value;

            int aux = int.Parse(idUser);

            RecipeDAO dao = new RecipeDAO();
            dao.AddHistory(aux, id_recipe);

            return RedirectToAction("Index", "User");
        }

        [Authorize]
        public IActionResult Rating(int id)
        {
            var id_temp_rec = TempData["Id"];
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);

            if (id_temp_rec == null || claim == null)
            {
                return RedirectToAction("Index", "User");
            }
            int id_recipe = int.Parse(id_temp_rec.ToString());
            int rate = id;
            string idUser = claim.Value;

            int aux = int.Parse(idUser);

            RecipeDAO dao = new RecipeDAO();
            dao.AddRating(aux, id_recipe, rate);
            dao.AddHistory(aux, id_recipe);

            return RedirectToAction("Index", "User");
        }
    }
}
