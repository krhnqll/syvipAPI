using Entities.All.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace syvipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }

        [HttpPost("send-otp")]
        public IActionResult SendOtp([FromBody] string phoneNumber)
        {
            var result = _service.SendOtp(phoneNumber);
            return Ok(result);
        }

        [HttpPost("confirm-reservation")]
        public IActionResult ConfirmReservation([FromBody] VerifyOtpDto dto)
        {
            var result = _service.VerifyAndCreateReservation(dto);
            return Ok(result);
        }
    }
}
