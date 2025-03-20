using InventorySystem.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Services.Models
{
    public class DeskLapModel
    {
        public int AssetId { get; set; }

        [Required(ErrorMessage = "RAM is required.")]
        [StringLength(50, ErrorMessage = "RAM cannot exceed 50 characters.")]
        [Display(Name = "RAM")]
        public string Ram { get; set; } = null!;

        [Required(ErrorMessage = "CPU is required.")]
        [StringLength(50, ErrorMessage = "CPU cannot exceed 50 characters.")]
        [Display(Name = "CPU")]
        public string Cpu { get; set; } = null!;

        [Required(ErrorMessage = "GPU is required.")]
        [StringLength(50, ErrorMessage = "GPU cannot exceed 50 characters.")]
        [Display(Name = "GPU")]
        public string Gpu { get; set; } = null!;

        [Required(ErrorMessage = "Wi-Fi MAC address is required.")]
        [StringLength(50, ErrorMessage = "Wi-Fi MAC address cannot exceed 50 characters.")]
        [Display(Name = "Wi-Fi MAC Address")]
        public string MacWifi { get; set; } = null!;

        [Required(ErrorMessage = "Ethernet MAC address is required.")]
        [StringLength(50, ErrorMessage = "Ethernet MAC address cannot exceed 50 characters.")]
        [Display(Name = "Ethernet MAC Address")]
        public string MacEthernet { get; set; } = null!;

        [Required(ErrorMessage = "Hard disk is required.")]
        [StringLength(50, ErrorMessage = "Hard disk cannot exceed 50 characters.")]
        [Display(Name = "Hard Disk")]
        public string HardDisk { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Screen size cannot exceed 50 characters.")]
        [Display(Name = "Screen Size")]
        public string ScreenSize { get; set; } = null!;


        [Required(ErrorMessage = "Serial Number Is Required")]
        [StringLength(100, ErrorMessage = "Serial Number cannot be longer than 100 characters.")]
        public string SerialNumber { get; set; } = null!;

        [Required(ErrorMessage = "Type ID Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Type")]

        public int TypeId { get; set; }

        [Required(ErrorMessage = "Purchase Date Is Required")]
        public DateOnly PurchaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? ExpiryDate { get; set; }

  
              
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
        public string Status { get; set; } = string.Empty;
        [Required(ErrorMessage = "Operating System Is required")]
        [StringLength(100, ErrorMessage = "Operating System cannot exceed 100 characters.")]
        public string OS { get; set; } = null!;

        public string Label { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
    }
}
