using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models.DTOs;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet("unpaid/{memberId}")]
        public IActionResult GetUnpaid(int memberId)
        {
            var result = _fineService.viewUnpaidFine(memberId);

            if (result == null || result.Count == 0)
                return NotFound("No unpaid fines");

            return Ok(result);
        }

        [HttpPost("pay/{borrowId}")]
        public IActionResult PayFine(int borrowId)
        {
            var result = _fineService.payFine(borrowId);
            return Ok(result);
        }

        [HttpGet("history/{memberId}")]
        public IActionResult History(int memberId)
        {
            var result = _fineService.finePaymentHistory(memberId);

            if (result == null || result.Count == 0)
                return NotFound("No fine history");

            return Ok(result);
        }
    }
}