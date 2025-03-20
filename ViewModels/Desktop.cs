using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class Desktop
    {
        [Key]
        [ForeignKey("Asset")]
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

        [StringLength(50, ErrorMessage = "Wi-Fi MAC address cannot exceed 50 characters.")]
        [Display(Name = "Wi-Fi MAC Address")]
        public string? MacWifi { get; set; }

        [Required(ErrorMessage = "Ethernet MAC address is required.")]
        [StringLength(50, ErrorMessage = "Ethernet MAC address cannot exceed 50 characters.")]
        [Display(Name = "Ethernet MAC Address")]
        public string MacEthernet { get; set; } = null!;
    
        [Required(ErrorMessage = "Hard disk is required.")]
        [StringLength(50, ErrorMessage = "Hard disk cannot exceed 50 characters.")]
        [Display(Name = "Hard Disk")]
        public string HardDisk { get; set; } = null!;
        // Navigation property
        public virtual Asset Asset { get; set; } = null!;
        [Required(ErrorMessage = "Operating System Is required")]
        [StringLength(100, ErrorMessage = "Operating System cannot exceed 100 characters.")]
        public string OS { get; set; } = "Windows 11 Pro";
        public string Label { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;


    }
}