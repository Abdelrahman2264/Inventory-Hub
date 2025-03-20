using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "Site name is required.")]
        [StringLength(100, ErrorMessage = "Site name cannot exceed 100 characters.")]
        [Display(Name = "Site Name")]
        public string Name { get; set; } = null!;

        public bool ? isActive { get; set; } = true;
        public bool? IsDefault { get; set; } = false;
        //[Required(ErrorMessage = "Site name is required.")]
        [StringLength(100, ErrorMessage = "Site name cannot exceed 100 characters.")]
        [Display(Name = "Site Code")]
        public string ? SiteCode { get; set; } = null!;

        // Navigation properties

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}