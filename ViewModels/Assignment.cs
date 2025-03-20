using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }

        [Required(ErrorMessage = "Asset ID is required.")]
        [ForeignKey("Asset")]
        [Display(Name = "Asset ID")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Assigned date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Assigned Date")]
        public DateTime AssignedDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Returned Date")]
        public DateTime? ReturnedDate { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        public bool? IsReturned { get; set; } = false;

        // Navigation properties
        public virtual Asset Asset { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}