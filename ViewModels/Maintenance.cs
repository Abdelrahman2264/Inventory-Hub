using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required]
        [ForeignKey("Asset")]
        public int AssetId { get; set; }

        public Asset Asset { get; set; } = null!;

        [Required(ErrorMessage = "Maintenance date is required.")]
        public DateOnly DateReceived { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public DateOnly? DateReturned { get; set; }

        [Required(ErrorMessage = "Maintenance type is required.")]
        [StringLength(50, ErrorMessage = "Maintenance type cannot exceed 50 characters.")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required(ErrorMessage = "Maintenance location is required.")]
        [StringLength(100, ErrorMessage = "Maintenance location cannot exceed 100 characters.")]
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;


        [StringLength(500, ErrorMessage = "Solution cannot exceed 500 characters.")]
        public string? Solution { get; set; }
    }
}