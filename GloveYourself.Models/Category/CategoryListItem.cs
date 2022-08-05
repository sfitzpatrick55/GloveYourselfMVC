using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Category
{
    public class CategoryListItem
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

