using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class GambarAset
    {
        [Key]
        public long? ID { get; set; }
        public string IDBRG { get; set; }
        public string FILENAME { get; set; }
        public string KET { get; set; }
    }
}