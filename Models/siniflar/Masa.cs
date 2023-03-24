using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Masa
    {
        [Key]
        public int Masaid { get; set; }
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public String MasaAd { get; set; }

        public bool MasaDurum { get; set; }
        public int IsletmeId { get; set; }
        public virtual Isletme_Sahibi Isletme_Sahibi { get; set; }
        public ICollection<Siparis_Hareket> Siparis_Harekets { get; set; }
        public ICollection<Hesap> Hesaps { get; set; }
        public ICollection<Rezerve> Rezerves { get; set; }
    }
}