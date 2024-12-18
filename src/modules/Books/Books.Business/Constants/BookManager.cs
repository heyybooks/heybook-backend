using Books.Business.Abstract;
using Books.Business.Constants;
using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Books.Business.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
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

        public IDataResult<Book> GetByName(string name)
        {
            var book = _bookDal.Get(b => string.Equals(b.BookName, name, StringComparison.OrdinalIgnoreCase));
            return new SuccessDataResult<Book>(book, Messages.BookListed);
        }



        private IResult CheckIfBookExists(Book book)
        {
            if (book == null)
            {
                return new ErrorResult(Messages.BookNotFound);
            }
            return new SuccessResult();
        }
    }
}
