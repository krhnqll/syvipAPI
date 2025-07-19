using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.All.Models.Admin
{
    public class Rezervations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string TelNo { get; set; }
        public string? Email { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public TimeSpan RezTime { get; set; }
        public DateOnly RezDate { get; set; }
        public int Status { get; set; } // 0 => Reddedilenler, 1 => Aktif Rezervasyonlar, 2 => Gerçekleşen Rezervasyonlar
        public DateTime CreatedTime { get; set; }
    }
}
