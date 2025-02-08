using AutoMapper;
using Books.Business.Abstract;
using Books.Business.Constants;
using Books.Business.Mapping;
using Books.Business.ValidationRules.FluentValidation;
using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Collections;

namespace Books.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly IBookImageDal _bookImageDal;
        private readonly BookFactory _bookFactory;
        private readonly IMapper _mapper;

        public BookManager(IBookDal bookDal, IBookImageDal bookImageDal, BookFactory bookFactory, IMapper mapper)
        {
            _bookDal = bookDal;
            _bookImageDal = bookImageDal;
            _bookFactory = bookFactory;
            _mapper = mapper;
        }

        
        [ValidationAspect(typeof(BookValidator))]
        public async Task<IResult> Add(Book book)
        {
            await _bookDal.AddAsync(book);
            return new SuccessResult(Messages.BookAdded);
        }

        
        [ValidationAspect(typeof(BookValidator))]
        public async Task<IResult> Update(Book book)
        {
            var result = CheckIfBookExists(book);
            if (!result.IsSuccess)
            {
                return new ErrorResult(result.Message);
            }

            await _bookDal.UpdateAsync(book);
            return new SuccessResult(Messages.BookUpdated);
        }

        public async Task<IResult> Delete(Book book)
        {
            var result = CheckIfBookExists(book);
            if (!result.IsSuccess)
            {
                return new ErrorResult(result.Message);
            }

            await _bookDal.DeleteAsync(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        public async Task<IDataResult<List<Book>>> GetAll()
        {
            var books = await _bookDal.GetAllAsync();
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }

        public async Task<IDataResult<List<Book>>> GetAllByCategoryId(int categoryId)
        {
            var books = await _bookDal.GetAllAsync(b => b.CategoryId == categoryId);
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books);
        }

        public async Task<IDataResult<List<Book>>> GetByCity(int cityId)
        {
            var books = await _bookDal.GetAllAsync(b => b.CityId == cityId);
            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books);
        }

        public async Task<IDataResult<Book>> GetById(int id)
        {
            var book = await _bookDal.GetAsync(b => b.BookId == id);
            return CheckForNull(book, Messages.BookNotFound) ?? new SuccessDataResult<Book>(book, Messages.BookListed);
        }

        public async Task<IDataResult<List<BookImage>>> GetImageByBookId(int id)
        {
            var images = await _bookImageDal.GetAllAsync(b => b.BookId == id);
            return CheckForNull(images, Messages.BookNotFound) ?? new SuccessDataResult<List<BookImage>>(images);
        }

        public async Task<IDataResult<List<Book>>> GetByName(string name)
        {
            var books = (await _bookDal.GetAllAsync())
                .Where(b => b.BookName.ToLower() == name.ToLower())
                .ToList();

            return CheckForNull(books, Messages.BookNotFound) ?? new SuccessDataResult<List<Book>>(books, Messages.BookListed);
        }

        
        [ValidationAspect(typeof(BookWithImagesDtoValidator))]
        public async Task<IResult> AddWithImages(BookWithImagesDto bookWithImagesDto)
        {
            if (bookWithImagesDto == null)
            {
                return new ErrorResult(Messages.BookInvalid);
            }

            var bookResult = _bookFactory.CreateBookFromDto(bookWithImagesDto);
            if (!bookResult.IsSuccess)
            {
                return bookResult;
            }

            Book book = bookResult.Data;
            await Add(book);

            var bookImagesResult = _bookFactory.CreateBookImagesFromDto(bookWithImagesDto, book);
            if (!bookImagesResult.IsSuccess)
            {
                return bookImagesResult;
            }

            foreach (var bookImage in bookImagesResult.Data)
            {
                await _bookImageDal.AddAsync(bookImage);
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
            if (book == null || _bookDal.GetAsync(b => b.BookId == book.BookId) == null)
            {
                return new ErrorResult(Messages.BookNotFound);
            }

            return new SuccessResult();
        }
    }
}
