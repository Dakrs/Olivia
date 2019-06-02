using Microsoft.AspNetCore.Mvc;
using Olivia.DataAccess;
using Olivia.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class RecipeController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Details(int id)
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            int idUser = int.Parse(claim.Value);

            RecipeDAO dao = new RecipeDAO();
            Recipe recipe = dao.FindById(id);



            if (recipe == null)
                return RedirectToAction("Index","User");

            List<Recipe> receitas = dao.getFavorites(idUser);
            ViewBag.Boolean = false;
            foreach (Recipe recp in receitas) {
                if (recp.Id_Recipe == id)
                {
                    ViewBag.Boolean = true;
                    break;
                }

            }
            return View(recipe);
        }

        [Authorize]
        public IActionResult Random()
        {

            RecipeDAO dao = new RecipeDAO();

            List<Recipe> recipe = dao.LoadRecipes();

            int RandomNumber(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max);
            }

            int numero = RandomNumber(0, recipe.Count);


            return View(dao.FindById(recipe.ElementAt(numero).Id_Recipe) );
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
        public IActionResult Create(Recipe recipe, IFormFile file)
        {
            RecipeDAO dao = new RecipeDAO();

            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);
            recipe.Creator = int.Parse(claim.Value);

            string s = "kapa";
            List<string> warnings = new List<string>
            {
                s
            };
            recipe.Warnings = warnings;
            recipe.Duration = 5;


            int id = dao.Insert(recipe);

            if (file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyToAsync(ms);
                    byte[] b = ms.ToArray();
                    dao.InsertImage(id, b);
                }
            }


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
        public IActionResult Edit(Recipe recipe, IFormFile file)
        {

            RecipeDAO dao = new RecipeDAO();

            if (file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyToAsync(ms);
                    byte[] b = ms.ToArray();
                    dao.InsertImage(recipe.Id_Recipe, b);
                }
            }

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

        [Authorize]
        public IActionResult Search(string words)
        {
            if (words == null || words.Equals(""))
            {
                return View(new List<Recipe>());
            }
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);

            int idUser = int.Parse(claim.Value);
            List<string> l_words = words.Split(' ').ToList();
            RecipeDAO dao = new RecipeDAO();
            Dictionary<int, float> rating = dao.allRatings();
            ViewBag.Ratings = rating;


            List<Recipe> receitas = dao.getFavorites(idUser);
            List<int> favoritos = new List<int>();
            foreach (Recipe recp in receitas)
            {
                favoritos.Add(recp.Id_Recipe);
            }
            ViewBag.Favorites = favoritos;

            return View(dao.searchByWords(l_words));
        }


    }




}
