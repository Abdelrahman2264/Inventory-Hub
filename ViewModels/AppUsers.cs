using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public class AppUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppUserId { get; set; }
        [Required(ErrorMessage = "User Name Is Required")]
        [StringLength(100, ErrorMessage = "User Name cannot be longer than 100 characters.")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }
        public bool IsActive { get; set; }


    }
}
