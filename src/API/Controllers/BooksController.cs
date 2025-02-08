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
        public async Task<IActionResult> AddWithImages([FromBody] BookWithImagesDto bookWithImagesDto)
        {
            if (bookWithImagesDto == null)
                return BadRequest(Messages.BookInvalid);

            var result = await _bookService.AddWithImages(bookWithImagesDto); // Remove await
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAll();
            return HandleDataResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookService.GetById(id);
            return HandleDataResult(result);
        }
        [HttpGet("GetImageByBookId")]
        public async Task<IActionResult> GetImageByBookId(int bookId)
        {
            var result = await _bookService.GetImageByBookId(bookId);
            return HandleDataResult(result);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(Messages.BookInvalid);

            var result = await _bookService.GetByName(name);
            return HandleDataResult(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId)
        {
            var result = await _bookService.GetAllByCategoryId(categoryId);
            return HandleDataResult(result);
        }

        [HttpGet("city/{cityId}")]
        public async Task<IActionResult> GetByCity(int cityId)
        {
            var result = await _bookService.GetByCity(cityId);
            return HandleDataResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (book == null || id != book.BookId)
                return BadRequest(Messages.BookInvalid);

            var result = await _bookService.Update(book);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookResult = await _bookService.GetById(id);
            if (!bookResult.IsSuccess)
                return NotFound(Messages.BookNotFound);

            var deleteResult = await _bookService.Delete(bookResult.Data);
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
