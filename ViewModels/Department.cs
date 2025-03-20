using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = null!;

        public bool? IsActive { get; set; } = true;
        public bool? IsDefault { get; set; } = false;

        // Navigation property
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}