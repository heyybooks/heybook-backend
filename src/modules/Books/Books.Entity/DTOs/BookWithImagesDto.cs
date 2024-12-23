using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Entity.DTOs
{
    public class BookCreateDto : IDto
    {
        public string BookName { get; set; } // Kitap adı
        public string Author { get; set; } // Yazar adı
        public string Description { get; set; } // Açıklama
        public string Publisher { get; set; } // Yayınevi
        public int PublicationYear { get; set; } // Yayın yılı
        public int CategoryId { get; set; } // Kategori ID
        public int CityId { get; set; } // Şehir ID
        public string Condition { get; set; } // Kitabın durumu
        public int OwnerId { get; set; } // Kitap sahibinin kullanıcı ID'si
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } // İlan aktif mi?
        public List<string> ImageUrls { get; set; } // Görsellerin URL'leri
    }
}
