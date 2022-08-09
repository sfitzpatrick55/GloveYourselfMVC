using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GloveYourself.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Hand Width")]
        public decimal HandWidthInch { get; set; }

        public decimal HandWidthMm
        {
            get
            {
                return HandWidthInch * 25.4m;
            }
        }

        public virtual ICollection<GloveEntity> Gloves { get; set; } // One-to-many    }
    }
}

