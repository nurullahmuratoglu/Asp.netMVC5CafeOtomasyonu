using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe_Otomasyonu.Models.siniflar;
using Cafe_Otomasyonu.Models.IEnumerable;

namespace Cafe_Otomasyonu.Controllers
{
    [Authorize(Roles = "D,AD,BD,CD,DE,ABD,ACD,ADE,BCD,BDE,CDE,ABDE,ACDE,ABCD,BCDE,ABCDE")]
    public class KasaHareketiController : Controller
    {
        // GET: KasaHareketi
        Context c = new Context();
        public ActionResult Index()
        {

            Class1 cs = new Class1();
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
            cs.isletme1 = c.Isletme_Sahibis.ToList();
            cs.Hesap1 = c.Hesaps.Where(x => x.IsletmeId == isletmeid).ToList();
            cs.Masa1 = c.Masas.ToList();
            cs.Siparis1 = c.Siparis_Harekets.ToList();


            return View(cs);
        }
        public ActionResult SiparisGecmis()
        {
            Class1 cs = new Class1();
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
            cs.Urun1 = c.Uruns.ToList();
            cs.Siparis1 = c.Siparis_Harekets.Where(x => x.IsletmeId == isletmeid).ToList();
            cs.Masa1 = c.Masas.ToList();
            return View(cs);
        }
        public ActionResult Detay(int hesapid)
        {
            Class1 cs = new Class1();
            cs.Hesap1 = c.Hesaps.Where(x => x.Hesapid == hesapid).ToList();
            cs.Urun1 = c.Uruns.ToList();
            cs.Masa1 = c.Masas.ToList();
            cs.Siparis1 = c.Siparis_Harekets.Where(x => x.Hesapid == hesapid).ToList();
            return View(cs);

        }

        public ActionResult GecmisRezerve()
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
            cs.Rezerve1 = c.Rezerves.Where(x => x.IsletmeId == isletmeid).ToList();
            cs.Masa1 = c.Masas.Where(x => x.IsletmeId == isletmeid).ToList();

            return View(cs);

        }
    }
}