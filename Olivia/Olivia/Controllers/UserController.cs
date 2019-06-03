﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Olivia.Models;
using Olivia.DataAccess;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Olivia.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);

            int idUser = int.Parse(claim.Value);

            RecipeDAO dao = new RecipeDAO();
            List<Recipe> recipes = dao.LoadRecipes();

            Dictionary<int, float> rating = dao.allRatings();
            List<KeyValuePair<int, float>> sortingDic = rating.ToList();

            sortingDic.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            List<Recipe> cardRecipes = new List<Recipe>();
            foreach(KeyValuePair<int, float> pairs in sortingDic.Take(2))
            {
                cardRecipes.Add(dao.FindById(pairs.Key));
            }

            List<Recipe> aux = dao.LoadRecipes();
            foreach(Recipe t in cardRecipes)
            {
                bool flag = aux.Remove(t);
                Console.WriteLine(flag);
            }

            Random rnd = new Random();
            for(int i = 0; i < 2; i++)
            {
                if (aux.Count > 0)
                {
                    int rand = rnd.Next(aux.Count);
                    Recipe t = aux[rand];
                    cardRecipes.Add(t);
                    aux.Remove(t);
                }
            }
            List<Recipe> receitas = dao.getFavorites(idUser);
            List<int> favoritos = new List<int>();
            foreach(Recipe recp in receitas)
            {
                favoritos.Add(recp.Id_Recipe);
            }
            ViewBag.Favorites = favoritos;
            ViewBag.Ratings = rating;
            ViewBag.CardRecipe = cardRecipes;

            return View(recipes);
        }

        public IActionResult Test()
        {
            return View();
        }


        public async Task<IActionResult> Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAO dAO = new UserDAO();
                int id = dAO.LogIn(model.Username, model.Password);

                if (id != -1)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Sid,id.ToString())
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "User", new { area = "" });
                }
                ModelState.AddModelError("", "Wrong username and password");
            }

            return View(model);
        }

        public IActionResult Register(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                Utilizador u = new Utilizador(-1, model.Username, Utilizador.HashPassword(model.Password), model.Email, 0, 0, model.Name);
                UserDAO dAO = new UserDAO();

                bool flag = dAO.Insert(u);

                if (flag)
                    return RedirectToAction("Index", "Home", new { area = "" });
                else model.Username = "";
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public IActionResult Profile()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name);
            string username = claim.Value;
            UserDAO dao = new UserDAO();
            Utilizador user = dao.FindByUsername(username);
            RecipeDAO rdao = new RecipeDAO();
            int nReceipts = rdao.NumberReceipts(user.Id_utilizador);
            int rated = rdao.NumberRated(user.Id_utilizador);
            int favourites = dao.CalculatePontos(user.Id_utilizador);
            ViewBag.User = user;
            ViewBag.NReceipts = nReceipts;
            ViewBag.Rated = rated;
            ViewBag.Favourites = favourites;

            return View();
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name);
            string username = claim.Value;
            UserDAO dao = new UserDAO();
            Utilizador user = dao.FindByUsername(username);
            ViewBag.User = user;

            return View();

        }


        [Authorize]
        public ActionResult ProfileEdited(int uid, string uname,string uemail)
        {
            UserDAO dao = new UserDAO();
            dao.UpdateProfile(uid, uname, uemail);
            Utilizador user = dao.FindByUsername("yolo");
            ViewBag.User = user;
           

            return View();
        }

        [Authorize]
        public IActionResult History()
        {
            UserDAO dao = new UserDAO();
            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid);

            int idUser = int.Parse(claim.Value);

            List<KeyValuePair<DateTime, Recipe>> list = dao.userHistory(idUser).ToList();
            list.Sort((pair1, pair2) => pair2.Key.CompareTo(pair1.Key));

            return View(list);
        }


        public IActionResult Favorites(){

            var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name);
            string username = claim.Value;
            UserDAO dao = new UserDAO();
            Utilizador user = dao.FindByUsername(username);
            RecipeDAO rdao = new RecipeDAO();
            List<Recipe> favoritos = rdao.getFavorites(user.Id_utilizador);
            ViewBag.User = user;
            ViewBag.Favoritos = favoritos;

            return View();
        }


        public IActionResult Colab() 
        {
            RecipeDAO rdao = new RecipeDAO();
            List<Recipe> aproval = rdao.NeedAproval();
            ViewBag.Need = aproval;
            return View();
        }

    }
}
