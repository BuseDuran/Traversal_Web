using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Traversal.Models
{
    public class TurkishPhoneNumberAttribute : RegularExpressionAttribute
    {
        public TurkishPhoneNumberAttribute() : base(@"^(0|\+90)(\d{3})\)?[\s.-]?(\d{3})[\s.-]?(\d{2})[\s.-]?(\d{2})$")
        {
            ErrorMessage = "The phone number is not in a valid format";
        }
    }
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="Enter your name")]
        [StringLength(42,MinimumLength =3,ErrorMessage = "Minimum 3 and maximum 42 characters allowed")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        [StringLength(42, MinimumLength = 3, ErrorMessage = "Minimum 3 and maximum 42 characters allowed")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Enter your email")]//a@gmail.com
        [StringLength(42, MinimumLength = 3, ErrorMessage = "Minimum 11 and maximum 42 characters allowed")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your username")]
        [StringLength(42, MinimumLength = 3, ErrorMessage = "Minimum 3 and maximum 42 characters allowed")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [StringLength(42, MinimumLength = 3, ErrorMessage = "Minimum 3 and maximum 42 characters allowed")]
        [TurkishPhoneNumber]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Select your gender")]
        public string? Gender { get; set; }

        public List<SelectListItem>? GenderOptions { get; set; }
        
        [Required(ErrorMessage = "Enter your age")]
        [Range(18,150,ErrorMessage ="Enter your age correctly")]
        public byte Age { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}