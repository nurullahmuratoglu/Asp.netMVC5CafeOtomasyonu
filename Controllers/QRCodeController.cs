using Cafe_Otomasyonu.Models;
using Cafe_Otomasyonu.Models.IEnumerable;
using Cafe_Otomasyonu.Models.siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafe_Otomasyonu.Controllers
{
    public class QRCodeController : Controller
    {
        // GET: QRCode
        Context c = new Context();
        public ActionResult Index(int id)
        {

            var menuid = c.Menus.Where(x => x.IsletmeId == id).Select(y => y.Menuid).FirstOrDefault();
            var kategoriler = c.Kategoris.Where(x => x.Menuid == menuid && x.Durum == true).ToList();
            var isletme = c.Isletme_Sahibis.Where(x => x.IsletmeId == id).Select(y => y.Isletme_Adı).First();

            ViewBag.dgr1 = isletme;
            Class1 cs = new Class1();
            cs.Kategori1 = kategoriler;
            cs.isletmeid = id;
            return View(cs);
        }

        public ActionResult Urun(int kid, int isletmeid)
        {
            var urunler = c.Uruns.Where(x => x.Kategoriid == kid && x.Durum == true).ToList();
            var ktg = c.Kategoris.Where(x => x.Kategoriid == kid).Select(y => y.KategoriAd).FirstOrDefault();
            ViewBag.d = ktg;
            Class1 cs = new Class1();
            cs.isletmeid = isletmeid;
            cs.Urun1 = urunler;
            return View(cs);


        }
        [Authorize]
        public ActionResult QROlustur() 
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
            Class1 cs = new Class1();
            cs.isletmeid = isletmeid;
            return View(cs);
        
        }

        
    }
}