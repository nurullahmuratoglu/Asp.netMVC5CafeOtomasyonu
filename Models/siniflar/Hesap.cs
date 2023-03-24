using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Hesap
    {
        [Key]
        public int Hesapid { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime? Tarih { get; set; } 
        public int? Masaid { get; set; }
        public int IsletmeId { get; set; }
        public virtual Masa Masa { get; set; }
        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }
        public ICollection<Siparis_Hareket> Siparis_Harekets { get; set; }
    }
}