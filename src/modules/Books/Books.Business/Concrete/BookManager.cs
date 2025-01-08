using Books.Business.Abstract;
using Books.Business.Constants;
using Books.Business.Helpers;
using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Collections;

namespace Books.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly IBookImageDal _bookImageDal;

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
                return new ErrorResult(result.Message);
            }

            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        public IResult Update(Book book)
        {
            var result = CheckIfBookExists(book);
            if (!result.IsSuccess)
            {
                return new ErrorResult(result.Message);
            }

            _bookDal.Update(book);
            return new SuccessResult(Messages.BookUpdated);
        }

        public IDataResult<List<Book>> GetAll()
        {
            var books = _bookDal.GetAll();
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }

        public IDataResult<List<Book>> GetAllByCategoryId(int categoryId)
        {
            var books = _bookDal.GetAll(b => b.CategoryId == categoryId);
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books);
        }

        public IDataResult<List<Book>> GetByCity(int cityId)
        {
            var books = _bookDal.GetAll(b => b.CityId == cityId);
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books);
        }

        public IDataResult<Book> GetById(int id)
        {
            var book = _bookDal.Get(b => b.BookId == id);
            return CheckForNull(book, Messages.BookNotFound) ?? new SuccessDataResult<Book>(book, Messages.BookListed);
        }

        public IDataResult<List<Book>> GetByName(string name)
        {
            var books = _bookDal.GetAll()
                .Where(b => b.BookName.ToLower() == name.ToLower())
                .ToList();

            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }

        public IResult AddWithImages(BookCreateDto bookCreateDto)
        {
            if (bookCreateDto == null)
            {
                return new ErrorResult(Messages.BookInvalid);
            }

            // DTO'dan Book Entity oluşturuluyor
            var book = BookFactory.CreateBookFromDto(bookCreateDto);

            _bookDal.Add(book);

            // DTO'daki ImageUrls listesini işleyip veritabanına ekle
            var bookImages = BookFactory.CreateBookImageFromDto(bookCreateDto, book);
            foreach (var bookImage in bookImages)
            {
                _bookImageDal.Add(bookImage);
            }

            return new SuccessResult(Messages.BookAdded);
        }

        private IDataResult<T> CheckForNull<T>(T data, string errorMessage)
        {
            if (data == null || (data is ICollection collection && collection.Count == 0))
            {
                return new ErrorDataResult<T>(errorMessage);
            }

            return null;
        }

        private IResult CheckIfBookExists(Book book)
        {
            if (book == null || _bookDal.Get(b => b.BookId == book.BookId) == null)
            {
                return new ErrorResult(Messages.BookNotFound);
            }

            return new SuccessResult();
        }
    }
}
