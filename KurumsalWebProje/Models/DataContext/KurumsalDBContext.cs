using KurumsalWebProje.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KurumsalWebProje.Models.DataContext
{
    public class KurumsalDBContext :DbContext
    {
        public KurumsalDBContext()
          : base("name=KurumsalWebDB")
        {
        }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<Iletisim> Iletisim { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kimlik> Kimlik { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }
    }
}