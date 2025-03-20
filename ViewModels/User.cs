using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.ViewModels
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Department is required.")]
        [ForeignKey("Department")]
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Select A Department")]

        public int DepartmentId { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Fingerprint is required.")]
        [StringLength(100, ErrorMessage = "Fingerprint cannot exceed 100 characters.")]
        [Display(Name = "Fingerprint Data")]
        public string? Fingerprint { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters.")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = null!;

        [Required(ErrorMessage = "Site is required.")]
        [ForeignKey("Site")]
        [Display(Name = "Site")]
        [Range(1, int.MaxValue, ErrorMessage = "Select A Site")]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        [Display(Name = "User Role")]
        public string Role { get; set; } = "User";

        [ForeignKey("Manager")]
        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        [Display(Name = "Active User")]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<User> InverseManager { get; set; } = new List<User>();

        public virtual ICollection<LogsHistory> LogsHistories { get; set; } = new List<LogsHistory>();

        public virtual User? Manager { get; set; }

        public virtual Site Site { get; set; } = null!;
    }
}