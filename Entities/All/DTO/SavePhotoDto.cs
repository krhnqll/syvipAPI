using Microsoft.AspNetCore.Http;

namespace Entities.All.DTO
{
    public class SavePhotoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
