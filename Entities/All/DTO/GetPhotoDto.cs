namespace Entities.All.DTO
{
    public class GetPhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; } // /uploads/... şeklinde
        public DateTime CreatedAt { get; set; }
    }

}
