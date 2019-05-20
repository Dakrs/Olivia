using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Olivia.DataAccess;
using Olivia.Models;
using Olivia.ViewModels;

namespace Olivia.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe/Random
        public ActionResult Random()
        {
            var recipe = new Recipe() { Name = "Frango do Speedy!!"};

            var viewModel = new RandomRecipeViewModel
            {
                Recipe = recipe
            };


            return View(viewModel);
        }

        public ActionResult ViewAll()
        {
            var recipesDAO = RecipeDAO.LoadRecipes();


            var viewModel = new ViewAllRecipesViewModel
            {
                Recipes = recipesDAO
            };


            return View(viewModel);
        }

        public ActionResult Create()
        {

            Recipe recipe = new Recipe();

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipe recipe)
        {

            if (ModelState.IsValid)
            {
                RecipeDAO.CreateRecipe(recipe);
                return RedirectToAction("ViewAll", "Recipe");
            }


            return View();
        }


        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }



        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace((sortBy)))
                sortBy = "Name";

            return Content((String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy)));
        }

        [Route("recipe/released/{year:regex(\\d{4}):range(2019,2025)}/{month:regex(\\d{2}):range(1,13)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

    }
}