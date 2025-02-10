using System;

namespace UserManagement.Business.DTOs
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }

         // Eğer kullanmak istemiyorsanız bu satırı kaldırabilirsiniz.
         public DateTime UpdatedAt { get; set; } 
    }
}
