using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Calisan
    {
        [Key]
        public int Calisanid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string AdSoyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Kullaniciadi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Sifre { get; set; }
        public string Yetki { get; set; }
        public int IsletmeId { get; set; }
        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }
    }
}