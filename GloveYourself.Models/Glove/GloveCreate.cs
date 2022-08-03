using System;
using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Glove
{
    public class GloveCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(200, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}

