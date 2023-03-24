using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Kategori
    {

        [Key]
        public int Kategoriid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }
        public bool Durum { get; set; }
        public Kategori()
        {
            Durum = true;
        }
        public int Menuid { get; set; }
        public virtual Menu Menu { get; set; } 

        public ICollection<Urun> Urunlers { get; set; }
        
    }
}