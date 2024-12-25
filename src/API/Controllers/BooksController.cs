using Books.Business.Abstract;
using Books.Business.Constants;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpPost]
        public IActionResult AddWithImages([FromBody] BookCreateDto bookCreateDto)
        {
            if (bookCreateDto == null)
                return BadRequest(Messages.BookInvalid);

            var result = _bookService.AddWithImages(bookCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            return result.IsSuccess ? Ok(result.Data) : NotFound(Messages.BookNotFound);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _bookService.GetById(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(Messages.BookNotFound);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(Messages.BookInvalid);

            var result = _bookService.GetByName(name);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetAllByCategoryId(int categoryId)
        {
            var result = _bookService.GetAllByCategoryId(categoryId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(Messages.BookNotFound);
        }

        [HttpGet("city/{cityId}")]
        public IActionResult GetByCity(int cityId)
        {
            var result = _bookService.GetByCity(cityId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(Messages.BookNotFound);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            if (book == null || id != book.BookId)
                return BadRequest(Messages.BookInvalid);

            var result = _bookService.Update(book);
            return result.IsSuccess ? Ok(Messages.BookUpdated) : NotFound(Messages.BookNotFound);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookResult = _bookService.GetById(id);
            if (!bookResult.IsSuccess)
                return NotFound(Messages.BookNotFound);

            var deleteResult = _bookService.Delete(bookResult.Data);
            return deleteResult.IsSuccess ? Ok(Messages.BookDeleted) : BadRequest(deleteResult.Message);
        }
    }
}
