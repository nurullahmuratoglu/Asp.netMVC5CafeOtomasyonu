using Cafe_Otomasyonu.Models.siniflar;
using Cafe_Otomasyonu.Models.IEnumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafe_Otomasyonu.Controllers
{
    [Authorize(Roles = "E,AE,BE,CE,DE,ABE,ACE,ADE,BCE,BDE,CDE,ACDE,ABDE,ABCE,BCDE,ABCDE")]
    public class CalisanController : Controller
    {
        Context c = new Context();
        // GET: Calisan


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
            var calisanlar = c.Calisans.Where(x => x.IsletmeId == isletmeid).ToList();
            return View(calisanlar);
        }
        [HttpGet]
        public ActionResult CalisanEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CalisanEkle(Calisan p, String A, String B, String C, String D, String E)
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

            if (!c.Calisans.Any(x =>x.Kullaniciadi == p.Kullaniciadi))
            {

                if (A == "true") p.Yetki += "A";
                if (B == "true") p.Yetki += "B";
                if (C == "true") p.Yetki += "C";
                if (D == "true") p.Yetki += "D";
                if (E == "true") p.Yetki += "E";
                p.IsletmeId = isletmeid;
                c.Calisans.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        public ActionResult CalisanSil(int id)
        {
            int calisanid;
            if (Session["Kullaniciadi"] != null)
            {
                var kullaniciadi = (string)Session["Kullaniciadi"];
                calisanid = c.Calisans.Where(x => x.Kullaniciadi == kullaniciadi).Select(y => y.Calisanid).FirstOrDefault();
                if (calisanid == id) 
                {
                    return RedirectToAction("Index");
                }
            }
            var calisan = c.Calisans.Find(id);
            c.Calisans.Remove(calisan);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}