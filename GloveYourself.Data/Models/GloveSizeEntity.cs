using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloveYourself.Data.Models
{
    public class GloveSizeEntity
    {
        [Key, ForeignKey("GloveEntity")]
        public int GloveId { get; set; }

        [Key]
        public SizeEnum Size { get; set; }

        [Required]
        public decimal MinWidth { get; set; }

        [Required]
        public decimal MaxWidth { get; set; }
    }
}

