using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Category
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [Display(Name= "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

