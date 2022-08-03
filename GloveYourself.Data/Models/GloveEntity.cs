using System;
using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Data.Models
{
    public class GloveEntity
    {
        [Key]
        public int GloveId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

