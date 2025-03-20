using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public partial class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Serial Number Is Required")]
        [StringLength(100, ErrorMessage = "Serial Number cannot be longer than 100 characters.")]
        public string SerialNumber { get; set; } = null!;

        [Required(ErrorMessage = "Type ID Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Type")]

        public int TypeId { get; set; }

        [Required(ErrorMessage = "Purchase Date Is Required")]
        public DateOnly PurchaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? ExpiryDate { get; set; } 
   

        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        public string Status { get; set; } = "";

        [StringLength(50, ErrorMessage = "Model Version cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "ModelVersion Is Required")]

        public string? ModelVersion { get; set; }

        [StringLength(50, ErrorMessage = "Brand cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "Brand Is Required")]

        public string? Brand { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        public virtual Desktop? Desktop { get; set; }

        public virtual Laptop? Laptop { get; set; }

        public virtual ICollection<LogsHistory> LogsHistories { get; set; } = new List<LogsHistory>();


        public virtual Type Type { get; set; } = null!;
    }
}