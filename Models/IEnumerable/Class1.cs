using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafe_Otomasyonu.Models.siniflar;

namespace Cafe_Otomasyonu.Models.IEnumerable
{
    public class Class1
    {
        public IEnumerable<Urun> Urun1 { get; set; }
        public IEnumerable<Isletme_Sahibi> isletme1 { get; set; }
        public IEnumerable<Siparis_Hareket> Siparis1 { get; set; }
        public IEnumerable<Masa> Masa1 { get; set; }
        public IEnumerable<Hesap> Hesap1 { get; set; }
        public IEnumerable<Rezerve> Rezerve1 { get; set; }
        public IEnumerable<Kategori> Kategori1 { get; set; }


        public List<String> Yetkiler { get; set; }
        public int masaid { get; set; }
        public String[] Yetki { get; set; }
        public int isletmeid { get; set; }
    }

}