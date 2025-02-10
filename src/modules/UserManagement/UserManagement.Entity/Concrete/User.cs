using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Entity.Concrete
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public decimal? Rating { get; set; }
        public int? TotalSwaps { get; set; }
        public bool? IsVerified { get; set; }
        public string? Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? SuccessfulSwaps { get; set; }
        
    }
}