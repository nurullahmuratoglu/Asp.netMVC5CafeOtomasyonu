using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe_Otomasyonu.Models.siniflar;
using Cafe_Otomasyonu.Models.IEnumerable;
using System.Web.Security;

namespace Cafe_Otomasyonu.Controllers
{

    [Authorize(Roles ="B,AB,BC,BD,BE,ABC,ABD,ABE,BCD,BCE,BDE,ABCD,ABCDE")]
    public class MasaController : Controller
    {
        Context c = new Context();
        



        // GET: Masa
        
        public ActionResult Index()
        {
            int isletmeid;
            
            if (Session["Kullaniciadi"] !=null)
            {
                var kullaniciadi = (string)Session["Kullaniciadi"];
                isletmeid = c.Calisans.Where(x => x.Kullaniciadi == kullaniciadi).Select(y => y.IsletmeId).FirstOrDefault();
            }

            else 
            {
                var mail = (string)Session["Email"];
                isletmeid = c.Isletme_Sahibis.Where(x => x.Email == mail).Select(y => y.IsletmeId).FirstOrDefault();
            }

            var masalar = c.Masas.Where(x => x.IsletmeId == isletmeid && x.MasaDurum == true).ToList();
            Class1 cs = new Class1();
            cs.Siparis1 = c.Siparis_Harekets.ToList();
            cs.Masa1 = masalar;
            cs.Rezerve1 = c.Rezerves.ToList();
            return View(cs);

        }
        [HttpGet]
        public ActionResult MasaEkle()
        {
            Masa p = new Masa();


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
            p.IsletmeId = isletmeid;
            c.Masas.Add(p);
            p.MasaAd = "Masa " + (c.Masas.Where(x => x.IsletmeId == isletmeid).Count() + 1).ToString();
            p.MasaDurum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
            ;
        }

        public ActionResult MasaGetir(int id)
        {
            var masa = c.Masas.Find(id);
            return View("MasaGetir", masa);

        }
        public ActionResult MasaGuncelle(Masa p)
        {
            var masa = c.Masas.Find(p.Masaid);
            masa.MasaAd = p.MasaAd;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult MasaSil(int id)
        {
            if (!c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesaps == null).Any() && !c.Rezerves.Where(x => x.Masaid == id && x.Durum == true).Any())
            {
                var masa = c.Masas.Find(id);
                masa.MasaDurum = false;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        public ActionResult MasaSiparis(int id)
        {


            Class1 cs = new Class1();
            cs.Siparis1 = c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesapid == null).ToList();
            var urunid = c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesapid == null).Select(x => x.Urunid).ToList();
            List<Urun> urunler = new List<Urun>();
            foreach (var k in urunid)
            {

                urunler.AddRange(c.Uruns.Where(x => x.Urunid == k).ToList());

            }

            cs.Urun1 = urunler;
            cs.masaid = id;

            return View(cs);

        }

        public ActionResult SiparisSil(int satisid, int masaid)
        {
            var satis = c.Siparis_Harekets.Find(satisid);
            if (satis.Adet > 1)
            {
                var urunfiyat = satis.Fiyat / satis.Adet;
                satis.Adet--;
                satis.Fiyat = urunfiyat * satis.Adet;


            }
            else if (satis.Adet == 1)
            {
                c.Siparis_Harekets.Remove(satis);
            }
            c.SaveChanges();
            return RedirectToAction("/MasaSiparis/" + masaid);

        }

        public ActionResult UrunListele(int id)
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
            var kategori = c.Kategoris.Where(x => x.Menuid == menuid && x.Durum == true).Select(y => y.Kategoriid).ToList();
            List<Urun> urunler = new List<Urun>();
            foreach (var k in kategori)
            {
                urunler.AddRange(c.Uruns.Where(x => x.Kategoriid == k && x.Durum == true).ToList());

            }
            Class1 cs = new Class1();

            cs.Urun1 = urunler;
            cs.masaid = id;


            return View(cs);
        }
        public ActionResult Siparis(int urunid, int masaid)
        {

            var siparis = c.Siparis_Harekets.Where(x => x.Urunid == urunid && x.Masaid == masaid && x.Hesapid == null).FirstOrDefault();
            if (siparis == null)
            {
                Siparis_Hareket yenisiparis = new Siparis_Hareket();
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
                yenisiparis.IsletmeId = isletmeid;
                yenisiparis.Masaid = masaid;
                yenisiparis.Adet = 1;
                yenisiparis.Urunid = urunid;

                yenisiparis.Fiyat = c.Uruns.Where(x => x.Urunid == urunid).Select(y => y.SatisFiyat).FirstOrDefault();
                c.Siparis_Harekets.Add(yenisiparis);
                if (c.Rezerves.Where(x => x.Masaid == masaid && x.Durum == true).Any())
                {
                    var rezerve = c.Rezerves.Where(x => x.Masaid == masaid && x.Durum == true).First();
                    rezerve.Durum = false;
                }
            }
            else
            {
                siparis.Adet++;
                siparis.Fiyat = siparis.Adet * c.Uruns.Where(x => x.Urunid == urunid).Select(y => y.SatisFiyat).FirstOrDefault();

            }
            c.SaveChanges();

            return RedirectToAction("UrunListele/" + masaid);

        }
        public ActionResult Artir(int satisid, int masaid)
        {
            var satis = c.Siparis_Harekets.Find(satisid);
            var adetfiyat = satis.Fiyat / satis.Adet;
            satis.Adet++;
            satis.Fiyat = adetfiyat * satis.Adet;
            c.SaveChanges();

            return RedirectToAction("/MasaSiparis/" + masaid);

        }

        public ActionResult Azalt(int satisid, int masaid)
        {
            var satis = c.Siparis_Harekets.Find(satisid);
            if (satis.Adet > 1)
            {
                var adetfiyat = satis.Fiyat / satis.Adet;
                satis.Adet--;
                satis.Fiyat = adetfiyat * satis.Adet;
                c.SaveChanges();
                return RedirectToAction("/MasaSiparis/" + masaid);
            }
            return RedirectToAction("/MasaSiparis/" + masaid);

        }

        [HttpGet]
        public ActionResult Rezerve(int masaid)
        {
            if (!c.Siparis_Harekets.Where(x => x.Masaid == masaid && x.Hesapid == null).Any())
            {
                if (!c.Rezerves.Where(x => x.Masaid == masaid && x.Durum == true).Any())
                {
                    Class1 cs = new Class1();
                    cs.masaid = masaid;
                    return View(cs);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Rezerve(Rezerve p, int masaid)
        {
            p.Masaid = masaid;
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
            p.IsletmeId = isletmeid;
            p.Durum = true;
            p.Tarih = DateTime.Now;
            c.Rezerves.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult RezerveSil(int masaid)
        {
            if (c.Rezerves.Where(x => x.Masaid == masaid && x.Durum == true).Any())
            {
                var rezerve = c.Rezerves.Where(x => x.Masaid == masaid && x.Durum == true).First();
                rezerve.Durum = false;
                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}