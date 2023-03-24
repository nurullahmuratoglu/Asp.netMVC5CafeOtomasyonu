using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe_Otomasyonu.Models.siniflar;
using System.Web.Security;

namespace Cafe_Otomasyonu.Controllers
{
    
    [Authorize(Roles = "A,AB,AC,AD,AE,ABC,ABD,ABE,ACD,ACE,ADE,ABCD,ABCDE")]//13
    public class MenuController : Controller
    {
        // GET: Menu
        Context c = new Context();

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
            var menuid = c.Menus.Where(x => x.IsletmeId == isletmeid).Select(y => y.Menuid).FirstOrDefault();

            var kategoriler = c.Kategoris.Where(x => x.Menuid == menuid && x.Durum == true).ToList();


            return View(kategoriler);
        }


        
        public ActionResult KategoriSil(int id)
        {
            var kategoriler = c.Kategoris.Find(id);
            kategoriler.Durum = false;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategoriler = c.Kategoris.Find(id);
            return View("KategoriGetir", kategoriler);

        }
        public ActionResult KategoriGuncelle(Kategori p)
        {
            var kategoriler = c.Kategoris.Find(p.Kategoriid);
            kategoriler.KategoriAd = p.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori p)
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
            var menuid = c.Menus.Where(x => x.IsletmeId == isletmeid).Select(y => y.Menuid).FirstOrDefault();
            p.Menuid = menuid;
            c.Kategoris.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Urunler(int id)
        {
            var urunler = c.Uruns.Where(x => x.Kategoriid == id && x.Durum == true).ToList();
            var ktg = c.Kategoris.Where(x => x.Kategoriid == id).Select(y => y.KategoriAd).FirstOrDefault();
            ViewBag.d = ktg;
            return View(urunler);
        }
        public ActionResult UrunEkle()

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
            var menuid = c.Menus.Where(x => x.IsletmeId == isletmeid).Select(y => y.Menuid).FirstOrDefault();
            List<SelectListItem> deger1 = (from x in c.Kategoris.Where(x => x.Menuid == menuid).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.Kategoriid.ToString(),


                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun p)
        {

            c.Uruns.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urunler = c.Uruns.Find(id);
            urunler.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
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
            var menuid = c.Menus.Where(x => x.IsletmeId == isletmeid).Select(y => y.Menuid).FirstOrDefault();
            List<SelectListItem> deger1 = (from x in c.Kategoris.Where(x => x.Menuid == menuid && x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.Kategoriid.ToString(),


                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urunler = c.Uruns.Find(id);
            return View("UrunGetir", urunler);

        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urunler = c.Uruns.Find(p.Urunid);
            urunler.UrunAd = p.UrunAd;
            urunler.Marka_Icerik = p.Marka_Icerik;
            urunler.SatisFiyat = p.SatisFiyat;
            urunler.Kategoriid = p.Kategoriid;
            c.SaveChanges();

            return RedirectToAction("Index");
        }


    }

}