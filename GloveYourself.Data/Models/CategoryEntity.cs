using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GloveYourself.Data.Models
{
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public virtual IEnumerable<GloveEntity> GloveEntities { get; set; } // one to many
    }
}

