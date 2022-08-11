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
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(500, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string Brand { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(500, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Task")]
        public int TaskId { get; set; }

        [Required]
        public decimal Smin { get; set; }
        [Required]
        public decimal Smax { get; set; }
        [Required]
        public decimal Mmin { get; set; }
        [Required]
        public decimal Mmax { get; set; }
        [Required]
        public decimal Lmin { get; set; }
        [Required]
        public decimal Lmax { get; set; }
        [Required]
        public decimal XLmin { get; set; }
        [Required]
        public decimal XLmax { get; set; }
    }
}

