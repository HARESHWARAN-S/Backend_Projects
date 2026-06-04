using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models.DTOs;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpPost("borrow")]
        public IActionResult BorrowBook([FromQuery] int memberId, [FromQuery] string bookId)
        {
            var result = _borrowService.BorrowBook(memberId, bookId);
            return Ok(result);
        }

        [HttpPost("return/{borrowId}")]
        public IActionResult ReturnBook(int borrowId)
        {
            var result = _borrowService.ReturnBook(borrowId);
            return Ok(result);
        }

        [HttpGet("current/{memberId}")]
        public IActionResult Current(int memberId)
        {
            var result = _borrowService.currentlyBorrowedBook(memberId);

            if (result == null || result.Count == 0)
                return NotFound("No active borrows");

            return Ok(result);
        }

        [HttpGet("overdue/{memberId}")]
        public IActionResult Overdue(int memberId)
        {
            var result = _borrowService.overdueBooks(memberId);

            if (result == null || result.Count == 0)
                return NotFound("No overdue books");

            return Ok(result);
        }

        [HttpGet("history/{memberId}")]
        public IActionResult History(int memberId)
        {
            var result = _borrowService.BorrowingHistory(memberId);

            if (result == null || result.Count == 0)
                return NotFound("No history found");

            return Ok(result);
        }
    }
}