using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Type name is required.")]
        [StringLength(100, ErrorMessage = "Type name cannot exceed 100 characters.")]
        [Display(Name = "Type Name")]
        public string Name { get; set; } = null!;

        public bool ? IsActive { get; set; } = true;
        public bool? IsDefault { get; set; } = false;


        // Navigation property
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}