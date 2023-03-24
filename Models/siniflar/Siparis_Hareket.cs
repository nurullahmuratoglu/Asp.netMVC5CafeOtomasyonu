using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Siparis_Hareket
    {
        [Key]
        public int Satisid { get; set; }

        public int Adet { get; set; }
        public decimal Fiyat { get; set; }

        public int Urunid { get; set; }
        public int IsletmeId { get; set; }
        public int? Masaid { get; set; }
        public int? Hesapid { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }
        public virtual Masa Masa { get; set; }
        
        public virtual Hesap Hesaps { get; set; }
    }
}