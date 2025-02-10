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
        Task<IDataResult<List<Book>>> GetAll();
        Task<IDataResult<Book>> GetById(int id);
        Task<IDataResult<List<Book>>> GetByName(string name);
        Task<IDataResult<List<Book>>> GetByCity(int CityId);
        Task<IDataResult<List<Book>>> GetAllByCategoryId(int CategoryId);
        Task<IDataResult<List<BookImage>>> GetImageByBookId(int id);

        Task<IResult> Add(Book book);
        Task<IResult> Delete(Book book);
        Task<IResult> Update(Book book);
        Task<IResult>AddWithImages(BookWithImagesDto bookWithImagesDto);
    }
}