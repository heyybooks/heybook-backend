using Books.Business.Abstract;
using Books.Business.Constants;
using Books.Entity.Concrete;
using Books.Entity.DTOs;
using Core.Utilities.Results.Abstract;
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
            return HandleResult(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            return HandleDataResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _bookService.GetById(id);
            return HandleDataResult(result);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(Messages.BookInvalid);

            var result = _bookService.GetByName(name);
            return HandleDataResult(result);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetAllByCategoryId(int categoryId)
        {
            var result = _bookService.GetAllByCategoryId(categoryId);
            return HandleDataResult(result);
        }

        [HttpGet("city/{cityId}")]
        public IActionResult GetByCity(int cityId)
        {
            var result = _bookService.GetByCity(cityId);
            return HandleDataResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            if (book == null || id != book.BookId)
                return BadRequest(Messages.BookInvalid);

            var result = _bookService.Update(book);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookResult = _bookService.GetById(id);
            if (!bookResult.IsSuccess)
                return NotFound(Messages.BookNotFound);

            var deleteResult = _bookService.Delete(bookResult.Data);
            return HandleResult(deleteResult);
        }

        private IActionResult HandleResult(Core.Utilities.Results.Abstract.IResult result)
        {
            if (result.IsSuccess)
                return Ok(new { Message = result.Message });
            return BadRequest(new { Message = result.Message });
        }

        private IActionResult HandleDataResult<T>(IDataResult<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Data);
            return NotFound(new { Message = result.Message });
        }
    }
}
