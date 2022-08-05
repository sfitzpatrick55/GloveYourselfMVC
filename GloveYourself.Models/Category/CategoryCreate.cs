using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Category
{
    public class CategoryCreate
    {
        [Required]
        public string CategoryName { get; set; }

        public string? Description { get; set; }
    }
}

