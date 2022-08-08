using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(CategoryEntity))]
        public int CategoryId { get; set; }
        public virtual CategoryEntity CategoryEntity { get; set; } // one to one

        public virtual IEnumerable<TaskEntity> TaskEntities { get; set; } // one to many
    }
}

