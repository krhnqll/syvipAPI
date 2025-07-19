using Entities.All.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace syvipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ReservationService _service;

        public ReservationController(ReservationService service, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _service = service;
        }

        [HttpPost("send-otp")]
        [EnableRateLimiting("otp_policy")]
        public IActionResult SendOtp([FromBody] string phoneNumber)
        {
            string phoneKey = $"otp_limit_{phoneNumber}";

            if (_memoryCache.TryGetValue(phoneKey, out _))
            {
                return StatusCode(429, "Bu telefon numarasına kısa sürede çok fazla istek yapıldı.");
            }

            _memoryCache.Set(phoneKey, true, TimeSpan.FromMinutes(1));

            var result = _service.SendOtpAsync(phoneNumber);
            return Ok(result);
        }

        [HttpPost("confirm-reservation")]
        public IActionResult ConfirmReservation([FromBody] VerifyOtpDto dto)
        {
            var result = _service.VerifyAndCreateReservationAsync(dto);
            return Ok(result);
        }
    }
}
