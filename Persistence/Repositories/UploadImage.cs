using System.ComponentModel.DataAnnotations;

namespace gisAPI.Repositories
{
    public class UploadImage
    {
        [Key]
        public long? ID { get; set; }
        public IFormFile? files { get; set; }
        public string IDBRG { get; set; }
        public string? FILENAME { get; set; }
        public string KET { get; set; }

    }
}