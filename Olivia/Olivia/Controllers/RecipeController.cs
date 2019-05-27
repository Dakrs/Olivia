using Microsoft.AspNetCore.Mvc;
using Olivia.DataAccess;
using Olivia.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class RecipeController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Details(int id)
        {

            RecipeDAO dao = new RecipeDAO();
            Recipe recipe = dao.FindById(id);

            return View(recipe);
        }

        [Authorize]
        public IActionResult Create()
        {

            RecipeDAO dao = new RecipeDAO();

            Recipe recipe = new Recipe();

            return View(recipe);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Create(Recipe recipe)
        {

            RecipeDAO dao = new RecipeDAO();

            dao.Insert(recipe);
            return RedirectToAction("Index", "Recipe");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {

            RecipeDAO dao = new RecipeDAO();

            Recipe recipe = dao.FindById(id);

            return View(recipe);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(Recipe recipe)
        {

            RecipeDAO dao = new RecipeDAO();

            dao.Edit(recipe);
            return RedirectToAction("Index", "Recipe");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {

            RecipeDAO dao = new RecipeDAO();

            dao.Delete(id);
            return RedirectToAction("Index", "Recipe");
        }

        [Authorize]
        public IActionResult Index()
        {
            RecipeDAO dao = new RecipeDAO();

            List<Recipe> Recipes = dao.LoadRecipes();

            return View(Recipes);
        }


    }



    
}
