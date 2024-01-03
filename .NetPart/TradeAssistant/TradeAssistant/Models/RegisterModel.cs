using System.ComponentModel.DataAnnotations;

namespace TradeAssistant.Models
{
    public class RegisterModel
    {
        
            [Required(ErrorMessage = "User Name is required")]
            public string Name { get; set; }

           [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }

            [Required(ErrorMessage = "First name is required")]
            public string FirstName { get; set; }

           [Required(ErrorMessage = "Last name is required")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "EMail is required")]
            public string Email { get; set; }

        
    }
}
