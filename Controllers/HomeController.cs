using Cafe_Otomasyonu.Models.siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafe_Otomasyonu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        Context c = new Context();
        public ActionResult Index()
        {

            if (Session["Kullaniciadi"] != null)
            {
                var kullaniciadi = (string)Session["Kullaniciadi"];
                ViewBag.dgr = c.Calisans.Where(x => x.Kullaniciadi == kullaniciadi).Select(y => y.AdSoyad).First();
                 
            }

            else
            {
                var mail = (string)Session["Email"];
                ViewBag.dgr = c.Isletme_Sahibis.Where(x => x.Email == mail).Select(y => y.Ad).First();

            }
            return View();
        }


    }
}