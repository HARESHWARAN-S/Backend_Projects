using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models.DTOs;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IBorrowService _borrowService;
        private readonly IFineService _fineService;

        public ReportsController(IBorrowService borrowService, IFineService fineService)
        {
            _borrowService = borrowService;
            _fineService = fineService;
        }

        [HttpGet("pending-fines")]
        public IActionResult PendingFines()
        {
            var result = _fineService.pendingFineList();

            if (result == null || !result.Any())
                return NotFound("No pending fines found");

            var dto = result.Select(f => new FineReadDto
            {
                Borrow_id = f.Borrow_id,
                member_id = f.member_id,
                amount = f.amount,
                status = f.status.ToString()
            }).ToList();

            return Ok(dto);
        }

        /*
        [HttpGet("summary/{memberId}")]
        public IActionResult Summary(int memberId)
        {
            var result = _borrowService.BorrowingSummary(memberId);

            return Ok(new BorrowSummaryDto
            {
                memberId = memberId,
                totalBorrowed = result.totalborrow,
                returned = result.returnedCount,
                pending = result.dueCount
            });
        }
        */
    }
}