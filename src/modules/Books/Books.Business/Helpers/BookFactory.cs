
using Books.Entity.Concrete;
using Books.Entity.DTOs;

namespace Books.Business.Helpers
{
    public static class BookFactory
    {
        public static Book CreateBookFromDto(BookCreateDto bookCreateDto)
        {
            if (bookCreateDto == null)
                return null;

            return new Book
            {
                BookName = bookCreateDto.BookName,
                Author = bookCreateDto.Author,
                Description = bookCreateDto.Description,
                Publisher = bookCreateDto.Publisher,
                PublicationYear = bookCreateDto.PublicationYear,
                CategoryId = bookCreateDto.CategoryId,
                CityId = bookCreateDto.CityId,
                Condition = bookCreateDto.Condition,
                OwnerId = bookCreateDto.OwnerId,
                CreatedDate = DateTime.UtcNow,
                IsActive = bookCreateDto.IsActive

            };
        }

        public static List<BookImage> CreateBookImageFromDto(BookCreateDto bookCreateDto, Book book)
        {
           var bookImages = new List<BookImage>();
            if (bookCreateDto.ImageUrls != null && bookCreateDto.ImageUrls.Any())
            {
                foreach (var imageUrl in bookCreateDto.ImageUrls)
                {
                    bookImages.Add(new BookImage
                    {
                        BookId = book.BookId,
                        ImageUrl = imageUrl,
                        UploadedDate = DateTime.UtcNow,
                        Book = book
                    });
                }
            }
            return bookImages;
        }
    }

}