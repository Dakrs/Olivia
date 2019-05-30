using Microsoft.AspNetCore.Mvc;
using Olivia.DataAccess;
using Olivia.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

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

            if (recipe == null)
                return RedirectToAction("Index","User");

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

            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            recipe.Creator = int.Parse(claim.Value);

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

        [Authorize]
        public IActionResult Favorite(int id)
        {
            string idUser = "";

            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            idUser = claim.Value;

            int aux = int.Parse(idUser);
            if (aux > 0)
            {
                RecipeDAO dAO = new RecipeDAO();
                dAO.AddToFavourite(aux, id);
            }

            return RedirectToAction("Details","Recipe", new {ID = id });
        }


    }



    
}
