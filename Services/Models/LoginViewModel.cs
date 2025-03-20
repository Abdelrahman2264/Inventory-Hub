using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Services.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide User Name")]
        public string? Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Password")]

        public string? Password { get; set; }
    }
}
