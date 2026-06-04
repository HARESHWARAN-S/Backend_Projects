using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models.DTOs;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("add")]
        public IActionResult AddMember([FromBody] MemberCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Member cannot be null");

            var result = _memberService.addMember(dto);
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAllMembers()
        {
            var result = _memberService.viewAllMembers();
            return Ok(result);
        }

        [HttpGet("search/email")]
        public IActionResult SearchByEmail([FromQuery] string email)
        {
            var result = _memberService.searchMemberByMail(email);

            if (result == null)
                return NotFound("Member not found");

            return Ok(result);
        }

        [HttpGet("search/phone")]
        public IActionResult SearchByPhone([FromQuery] string phone)
        {
            var result = _memberService.searchMemberByPhno(phone);

            if (result == null)
                return NotFound("Member not found");

            return Ok(result);
        }

        [HttpPut("deactivate/{memberId}")]
        public IActionResult Deactivate(int memberId)
        {
            var result = _memberService.DeactivateMember(memberId);

            if (result == null)
                return NotFound("Member not found");

            return Ok(result);
        }

        [HttpPut("activate/{memberId}")]
        public IActionResult Activate(int memberId)
        {
            var result = _memberService.ActivateMember(memberId);

            if (result == null)
                return NotFound("Member not found");

            return Ok(result);
        }
    }
}