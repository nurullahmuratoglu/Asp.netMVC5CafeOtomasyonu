using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Menu
    {
        [Key]
        public int Menuid { get; set; }
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string MenuAd { get; set; }


        public int? IsletmeId { get; set; }
        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }

        public ICollection<Kategori> Kategoris { get; set; }



    }
}