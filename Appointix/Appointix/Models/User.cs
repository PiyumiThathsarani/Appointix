using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Appointix.Models
{
    public class User : IdentityUser
    {
        
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        
        [Display(Name = "Registered On")]
        public DateTime RegisteredDateTime { get; set; }
    }

}
