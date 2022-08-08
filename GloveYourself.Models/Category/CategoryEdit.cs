using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Category
{
    public class CategoryEdit
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string CategoryName { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(500, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string? Description { get; set; }
    }
}

