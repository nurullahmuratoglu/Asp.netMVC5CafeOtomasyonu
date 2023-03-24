using Cafe_Otomasyonu.Models.siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cafe_Otomasyonu.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();

        }
        [HttpPost]
        public PartialViewResult Partial1(Isletme_Sahibi p)
        {
            if (!c.Isletme_Sahibis.Any(x => x.Email == p.Email))
            {
                c.Isletme_Sahibis.Add(p);
                c.SaveChanges();
                Menu menu = new Menu();
                menu.IsletmeId = p.IsletmeId;
                c.Menus.Add(menu);
                c.SaveChanges();
                return PartialView();
            }
            return PartialView();

        }
        [HttpGet]
        public ActionResult Login1()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login1(Isletme_Sahibi p)
        {
            var bilgiler = c.Isletme_Sahibis.FirstOrDefault(x => x.Email == p.Email && x.Sifre == p.Sifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                Session["Email"] = bilgiler.Email.ToString();
                return RedirectToAction("Index", "Home");

            }
            else
            {

                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Cikisyap()
        {
            FormsAuthentication.SignOut();
            Session["Kullaniciadi"] = null;
            return RedirectToAction("Index", "Login");

        }

        [HttpGet]
        public ActionResult LoginPersonel()
        {
            return View();

        }

        [HttpPost]
        public ActionResult LoginPersonel(Calisan p)
        {
            var bilgiler = c.Calisans.FirstOrDefault(x => x.Kullaniciadi == p.Kullaniciadi && x.Sifre == p.Sifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullaniciadi, false);
                Session["Kullaniciadi"] = bilgiler.Kullaniciadi.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}