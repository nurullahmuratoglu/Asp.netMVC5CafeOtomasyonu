using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Isletme_Sahibi
    {
        [Key]
        public int IsletmeId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Ad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string SoyAd { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Isletme_Adı { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Email { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Sifre { get; set; }

        public ICollection<Menu> Menus { get; set; }
        public ICollection<Masa> Masas { get; set; }
        public ICollection<Siparis_Hareket> Siparis_Harekets { get; set; }
        public ICollection<Hesap> Hesaps { get; set; }
        public ICollection<Rezerve> Rezerves { get; set; }
        public ICollection<Calisan> Calisans { get; set; }

    }
}