using System.ComponentModel.DataAnnotations;

namespace UserManagement.Business.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad zorunludur")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur")]
        public string Address { get; set; } = "Varsayılan Adres";

        [Required(ErrorMessage = "Şehir zorunludur")]
        public string City { get; set; } = "Belirtilmemiş"; 
        
    }

}
