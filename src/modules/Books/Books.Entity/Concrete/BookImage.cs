using Core.Entities;

namespace Books.Entity.Concrete

{
    public class BookImage : IEntity
    {
        public int BookImageId { get; set; } // Fotoğraf ID'si
        public int BookId { get; set; } // İlgili kitap ID
        public string? ImageUrl { get; set; } // Fotoğrafın URL'si
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        // Yükleme tarihi
        public virtual Book Book { get; set; }
    }
}

