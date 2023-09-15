
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
namespace KurumsalWebProje.Models.Model
{
    [Table("Hizmet")]
    public class Hizmet
    {
        [Key]
        public int HizmetId { get; set; }

        [Required]
        [StringLength(50)]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        [StringLength(500)]
        public string ResimURL { get; set; }
    }
}
