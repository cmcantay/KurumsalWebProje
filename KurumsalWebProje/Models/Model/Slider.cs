using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWebProje.Models.Model
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider Başlık"),StringLength(30,ErrorMessage ="30 karakter olmalı")]
        public string Baslik { get; set; }
        [DisplayName("Slider Aciklama"), StringLength(150, ErrorMessage = "150 karakter olmalı")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), StringLength(250)]
        public string ResimURL { get; set; }


    }
}