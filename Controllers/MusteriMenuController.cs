using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe_Otomasyonu.Models.siniflar;



namespace Cafe_Otomasyonu.Controllers
{
   
    public class MusteriMenuController : Controller
    {
        Context c = new Context();
        // GET: MusteriMenu
        
        public ActionResult Index()
        {

            int isletmeid;

            if (Session["Kullaniciadi"] != null)
            {
                var kullaniciadi = (string)Session["Kullaniciadi"];
                isletmeid = c.Calisans.Where(x => x.Kullaniciadi == kullaniciadi).Select(y => y.IsletmeId).FirstOrDefault();
            }

            else
            {
                var mail = (string)Session["Email"];
                isletmeid = c.Isletme_Sahibis.Where(x => x.Email == mail).Select(y => y.IsletmeId).FirstOrDefault();
            }
            var isletme = c.Isletme_Sahibis.Where(x => x.IsletmeId == isletmeid).Select(y => y.Isletme_Adı).FirstOrDefault();
            ViewBag.dgr1 = isletme;
            var menuid = c.Menus.Where(x => x.IsletmeId == isletmeid).Select(y => y.Menuid).FirstOrDefault();

            var kategoriler = c.Kategoris.Where(x => x.Menuid == menuid && x.Durum == true).ToList();
            return View(kategoriler);
        }        
        public ActionResult Urun(int id ) 
        {
            var urunler = c.Uruns.Where(x => x.Kategoriid == id && x.Durum == true).ToList();
            var ktg = c.Kategoris.Where(x => x.Kategoriid == id).Select(y => y.KategoriAd).FirstOrDefault();
            ViewBag.d = ktg;
            return View(urunler);
            
        
        }
    }
}