using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Rezerve
    {
        [Key]
        public int Rezerveid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string RezerveAd { get; set; }
        public bool Durum { get; set; }

        public int? Masaid { get; set; }
        public int IsletmeId { get; set; }
        public DateTime Tarih { get; set; }

        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }
        public virtual Masa Masa { get; set; }
    }
}