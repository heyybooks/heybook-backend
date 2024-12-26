using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace UserManagement.Entity.Concrete
{
    public class User : IEntity
    {
        public int UserId { get; set; } // Kullanıcı ID'si

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } // Kullanıcı adı

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } // Kullanıcı e-posta adresi

        [Required(ErrorMessage = "Password hash is required.")]
        public byte[] PasswordHash { get; set; } // Şifre hash'i

        [Required(ErrorMessage = "Password salt is required.")]
        public byte[] PasswordSalt { get; set; } // Şifre tuzu

        public string FirstName { get; set; } // Kullanıcının adı
        public string LastName { get; set; } // Kullanıcının soyadı
        public string PhoneNumber { get; set; } // Telefon numarası
        public string Address { get; set; } // Adres
        public string City { get; set; } // Şehir
        public string ProfilePicture { get; set; } // Profil resmi URL'si
        public string Bio { get; set; } // Kısa biyografi

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal Rating { get; set; } // Kullanıcı puanı

        public int TotalSwaps { get; set; } // Toplam takas sayısı
        public bool IsVerified { get; set; } // Kullanıcı doğrulandı mı?
        
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } // Kullanıcının durumu
        
        public DateTime? LastLoginDate { get; set; } // Son giriş tarihi
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma tarihi (varsayılan olarak UTC)
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Güncellenme tarihi (varsayılan olarak UTC)
        
        public int SuccessfulSwaps { get; set; } // Başarılı takas sayısı
    }
}
