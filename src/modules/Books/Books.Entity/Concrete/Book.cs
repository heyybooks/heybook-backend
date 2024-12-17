using Core.Entities;


namespace Books.Entity.Concrete
{
    public class Book : IEntity
    {
        public int BookId { get; set; } // Kitap ID'si
        public string BookName { get; set; } // Kitap adı
        public string Author { get; set; } // Yazar adı
        public string Description { get; set; } // Açıklama
        public string Publisher { get; set; } // Yayınevi
        public int PublicationYear { get; set; } // Yayın yılı
        //public string ISBN { get; set; } // ISBN numarası (opsiyonel)
        public int CategoryId { get; set; } // Kategori ID
        public int CityId { get; set; } // Şehir ID
        // public int DistrictId { get; set; } // İlçe ID
        public string Condition { get; set; } // Kitabın durumu
        public int OwnerId { get; set; } // Kitap sahibinin kullanıcı ID'si
        public DateTime CreatedDate { get; set; } // İlan oluşturma tarihi
        public bool IsActive { get; set; } // İlan aktif mi?

        public ICollection<BookImage> BookImages { get; set; }

    }
}
