using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.All.DTO
{
    public class VerifyOtpDto
    {
        public SaveReservationDto Reservation { get; set; }
        public string OtpCode { get; set; }
    }
}
