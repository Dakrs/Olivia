using System;
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
            RecipeDAO dao = new RecipeDAO();
            List<Recipe> recipes = dao.LoadRecipes();
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
            int favourites = rdao.NumberFavourites(user.Id_utilizador);
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

        public ActionResult ProfileEdited(int uid, string uname,string uemail)
        {
            UserDAO dao = new UserDAO();
            dao.UpdateProfile(uid, uname, uemail);
            Utilizador user = dao.FindByUsername("yolo");
            ViewBag.User = user;
           

            return View();
        }


    }
}
