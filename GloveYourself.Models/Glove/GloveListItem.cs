using System;
using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Glove
{
    public class GloveListItem
    {
        public int GloveId { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

