using AutoMapper;
using Books.Business.Constants;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Books.Business.Mapping
{
    public class BookFactory
    {
        private readonly IMapper _mapper;

        public BookFactory(IMapper mapper)
        {
            Console.WriteLine("BookFactory olusturuldu");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), Messages.MapperNotNull);
        }

        public IDataResult<Book> CreateBookFromDto(BookWithImagesDto bookWithImagesDto)
        {
            if (_mapper == null)
            {
                throw new Exception("AutoMapper instance is NULL! AutoMapper dependency injection'ı eksik olabilir.");
            }
            if (bookWithImagesDto == null)
            {
                return new ErrorDataResult<Book>(Messages.BookCreateDtoNull);
            }
            Console.WriteLine("BookFactory icerisindeki CreateBookFromDto calistirildi");
            Book book = _mapper.Map<Book>(bookWithImagesDto);
            return new SuccessDataResult<Book>(book, Messages.BookCreateDtoCreated);
        }

        public IDataResult<List<BookImage>> CreateBookImagesFromDto(BookWithImagesDto bookWithImagesDto, Book book)
        {

            if (bookWithImagesDto.ImageUrls == null || !bookWithImagesDto.ImageUrls.Any())
            {
                return new ErrorDataResult<List<BookImage>>(Messages.BookCreateDtoNoImages);
            }

            var bookImages = bookWithImagesDto.ImageUrls.Select(imageUrl => new BookImage
            {
                BookId = book.BookId,
                ImageUrl = imageUrl,
                UploadedDate = DateTime.UtcNow,
                Book = book
            }).ToList();

            return new SuccessDataResult<List<BookImage>>(bookImages, Messages.BookImagesCreated);
        }
    }
}
