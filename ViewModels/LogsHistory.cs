using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class LogsHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Required(ErrorMessage = "Asset ID is required.")]
        [ForeignKey("Asset")]
        [Display(Name = "Asset ID")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Change description is required.")]
        [StringLength(500, ErrorMessage = "Change description cannot exceed 500 characters.")]
        [Display(Name = "Change Description")]
        public string ChangeDescription { get; set; } = null!;

      
        public string DeviceType { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Change Date")]
        public DateTime? ChangeDate { get; set; }

        // Navigation properties
        public virtual Asset Asset { get; set; } = null!;

        [Required(ErrorMessage = "Changed by user ID is required.")]
        [ForeignKey("AppUser")]
        [Display(Name = "Changed By User ID")]
        public int AppUserId { get; set; }
        public int UserId { get; set; }
        public string LogType { get; set; }

        public virtual AppUsers AppUser { get; set; } = null!;

    }
}