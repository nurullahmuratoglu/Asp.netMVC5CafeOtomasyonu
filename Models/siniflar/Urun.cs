using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Urun
    {
        [Key]
        public int Urunid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public String Marka_Icerik { get; set; }
        public Decimal SatisFiyat { get; set; }
        public bool Durum { get; set; }
        public Urun() 
        { 
            Durum = true;
        }
        public int Kategoriid { get; set; }
        public virtual Kategori Kategori { get; set; }
        public ICollection<Siparis_Hareket> Siparis_Harekets { get; set; }

    }
}