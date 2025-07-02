using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.All.DTO
{
    public class SaveReservationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TelNo { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public TimeSpan RezTime { get; set; }
        public DateOnly RezDate { get; set; }
        public string? Email { get; set; }
    }
}
