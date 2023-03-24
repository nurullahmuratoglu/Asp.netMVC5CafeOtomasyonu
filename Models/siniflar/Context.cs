using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cafe_Otomasyonu.Models.siniflar
{
    public class Context:DbContext
    {
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Masa> Masas { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Isletme_Sahibi> Isletme_Sahibis { get; set; }
        public DbSet<Siparis_Hareket> Siparis_Harekets { get; set; }
        public DbSet<Hesap> Hesaps { get; set; }
        public DbSet<Rezerve> Rezerves { get; set; }
        public DbSet<Calisan> Calisans { get; set; }

    }
}