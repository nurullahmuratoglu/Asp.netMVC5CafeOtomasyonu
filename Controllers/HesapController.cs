using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe_Otomasyonu.Models.siniflar;
using Cafe_Otomasyonu.Models.IEnumerable;

namespace Cafe_Otomasyonu.Controllers
{
    [Authorize(Roles = "C,AC,BC,CD,CE,ABC,ACD,ACE,BCD,BCE,CDE,ABCD,ABCDE")]
    public class HesapController : Controller
    {
        Context c = new Context();
        // GET: Hesap
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
            var masalar = c.Masas.Where(x => x.IsletmeId == isletmeid && x.MasaDurum == true).ToList();
            Class1 cs = new Class1();
            cs.Masa1 = masalar;
            cs.Siparis1 = c.Siparis_Harekets.ToList();
            cs.Rezerve1 = c.Rezerves.Where(x => x.IsletmeId == isletmeid).ToList();
            return View(cs);
        }
        public ActionResult Siparisler(int id)
        {
            Class1 cs = new Class1();
            cs.Siparis1 = c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesapid==null).ToList();
            var urunid = c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesapid==null).Select(x => x.Urunid).ToList();
            List<Urun> urunler = new List<Urun>();
            foreach (var k in urunid)
            {

                urunler.AddRange(c.Uruns.Where(x => x.Urunid == k).ToList());

            }

            cs.Urun1 = urunler;
            cs.masaid = id;

            return View(cs);

        }
        public ActionResult Hesapkes(int id)
        {
            Hesap hesap = new Hesap();
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
            var masa = c.Siparis_Harekets.Where(x => x.Masaid == id && x.Hesapid == null).ToList();
            if (masa.Any())
            {
                decimal toplamfiyat = 0;
                foreach (var x in masa)
                {
                    toplamfiyat += x.Fiyat;
                }
                hesap.ToplamTutar = toplamfiyat;
                hesap.Tarih = DateTime.Now;
                hesap.Masaid = id;
                hesap.IsletmeId = isletmeid;
                c.Hesaps.Add(hesap);
                foreach (var x in masa)
                {
                    x.Hesapid += hesap.Hesapid;
                }
                c.SaveChanges();

                return Hesapid(masa, hesap.Hesapid);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Hesapid(List<Siparis_Hareket> masa , int id )
        {
            foreach (var x in masa)
            {
                x.Hesapid = id;

            }
            c.SaveChanges();
            
            return RedirectToAction("Detay", "KasaHareketi", new { @hesapid = id });


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
            return RedirectToAction("/Siparisler/" + masaid);

        }
        public ActionResult Artir(int satisid, int masaid)
        {
            var satis = c.Siparis_Harekets.Find(satisid);
            var adetfiyat = satis.Fiyat / satis.Adet;
            satis.Adet++;
            satis.Fiyat = adetfiyat * satis.Adet;
            c.SaveChanges();

            return RedirectToAction("/Siparisler/" + masaid);

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
                return RedirectToAction("/Siparisler/" + masaid);
            }
            return RedirectToAction("/Siparisler/" + masaid);

        }



    }
}