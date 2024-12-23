using Books.Business.Abstract;
using Books.Business.Constants;
using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Books.Business.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;
        IBookImageDal _bookImageDal;

        public BookManager(IBookDal bookDal, IBookImageDal bookImageDal)
        {
            _bookDal = bookDal;
            _bookImageDal = bookImageDal;
        }

        public IResult Add(Book book)
        {
            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }

        public IResult Delete(Book book)
        {
            var result = CheckIfBookExists(book);
            if (!result.IsSuccess)
            {
                return result;
            }

            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        public IResult Update(Book book)
        {
            var result = CheckIfBookExists(book);
            if (!result.IsSuccess)
            {
                return result;
            }
            _bookDal.Update(book);
            return new SuccessResult(Messages.BookUpdated);
        }


        public IDataResult<List<Book>> GetAll()
        {
            var books = _bookDal.GetAll();
            return new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }

        public IDataResult<List<Book>> GetAllByCategoryId(int CategoryId)
        {
            var books = _bookDal.GetAll(b => b.CategoryId == CategoryId);
            return new SuccessDataResult<List<Book>>(books);
        }

        public IDataResult<List<Book>> GetByCity(int CityId)
        {
            var books = _bookDal.GetAll(b => b.CityId == CityId);
            return new SuccessDataResult<List<Book>>(books);
        }

        public IDataResult<Book> GetById(int id)
        {
            var book = _bookDal.Get(b => b.BookId == id);
            return new SuccessDataResult<Book>(book, Messages.BookListed);
        }

        public IDataResult<List<Book>> GetByName(string name)
        {
            var books = _bookDal.GetAll()
                .Where(b => b.BookName.ToLower() == name.ToLower())
                .ToList();
            return new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }



        private IResult CheckIfBookExists(Book book)
        {
            if (book == null)
            {
                return new ErrorResult(Messages.BookNotFound);
            }
            return new SuccessResult();
        }

        public IResult AddWithImages(BookCreateDto bookCreateDto)
        {
            if (bookCreateDto == null)
            {
                return new ErrorResult(Messages.BookInvalid);
            }

            // 1. Book Entity'si oluşturuluyor
            var book = new Book
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
                CreatedDate = DateTime.UtcNow, // Güvenlik için server zamanı kullanılabilir
                IsActive = bookCreateDto.IsActive
            };

            // 2. Book Kaydı
            _bookDal.Add(book);
            Console.WriteLine($"BookId: {book.BookId}");
            // 3. ImageUrls ekleniyor
            if (bookCreateDto.ImageUrls != null && bookCreateDto.ImageUrls.Any())
            {
                foreach (var imageUrl in bookCreateDto.ImageUrls)
                {
                    var bookImage = new BookImage
                    {
                        
                        BookId = book.BookId, // Artık atanmış durumda
                        ImageUrl = imageUrl,
                        UploadedDate = DateTime.UtcNow,
                        Book = book,
                        
                    };
                    Console.WriteLine($"BookId: {bookImage.BookImageId}");
                    if (bookImage == null)
                    {
                        throw new InvalidOperationException("Book image repository is not initialized.");
                    }
                    _bookImageDal.Add(bookImage);
                }
            }

            return new SuccessResult(Messages.BookAdded);
        }
    }
}
