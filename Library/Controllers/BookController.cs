using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models.DTOs;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICopyService _copyService;

        public BookController(IBookService bookService, ICopyService copyService)
        {
            _bookService = bookService;
            _copyService = copyService;
        }

        [HttpPost("add")]
        public IActionResult AddBook([FromBody] BookCreateDto dto)
        {
            var result = _bookService.addBook(dto);

            // creating first copy by default when we add a book
            //further we can add the copies directly by giving the bookId
            _copyService.addCopy(dto.book_Id, true);

            return Ok(result);
        }

        [HttpPost("add-copy")]
        public IActionResult AddCopy([FromQuery] string bookId)
        {
            var result = _copyService.addCopy(
                bookId,false
                );

            return Ok(result);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableBooks()
        {
            var result = _bookService.ViewAvlBooks();

            if (result == null || result.Count == 0)
                return NotFound("No books found");

            return Ok(result);
        }

        [HttpGet("search/category")]
        public IActionResult SearchByCategory([FromQuery] string category)
        {
            var result = _bookService.searchBookByCategory(category);

            if (result == null || result.Count == 0)
                return NotFound("No books found");

            return Ok(result);
        }
    }
}