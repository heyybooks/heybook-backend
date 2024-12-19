using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Entity.Concrete

{
    public class BookImage : IEntity
    {
        public int BookImageId { get; set; } // Fotoğraf ID'si
        public int BookId { get; set; } // İlgili kitap ID
        public string? ImageUrl { get; set; } // Fotoğrafın URL'si
        public DateTime UploadedDate { get; set; } // Yükleme tarihi
        public Book Book { get; set; }
    }
}

