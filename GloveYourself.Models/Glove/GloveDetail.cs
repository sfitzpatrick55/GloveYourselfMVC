using System;
using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Glove
{
    public class GloveDetail
    {
        public int GloveId { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        //[Display(Name = "Category")]
        //public int CategoryId { get; set; }

        [Display(Name= "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        //public CategoryEntity CategoryEntity { get; set; }

        public string Category { get; set; }
    }
}

