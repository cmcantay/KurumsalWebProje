
    using System;
    using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
namespace KurumsalWebProje.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikId { get; set; }

        [StringLength(150)]
        [DisplayName("Baþlýk")] 
        public string Title { get; set; }

        [StringLength(250)]
        [DisplayName("Anahtar Kelimeler")]
        public string Keywords { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string LogoURL { get; set; }

        [StringLength(250)]
        public string Unvan { get; set; }
    }
}
