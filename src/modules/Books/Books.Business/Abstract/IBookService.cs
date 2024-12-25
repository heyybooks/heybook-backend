using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAll();
        IDataResult<Book> GetById(int id);
        IDataResult<List<Book>> GetByName(string name);
        IDataResult<List<Book>> GetByCity(int CityId);
        IDataResult<List<Book>> GetAllByCategoryId(int CategoryId);

        IResult Add(Book book);
        IResult Delete(Book book);
        IResult Update(Book book);
        IResult AddWithImages(BookCreateDto bookCreateDto);
    }
}
