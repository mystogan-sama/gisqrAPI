using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class JnsKibRepository
    {
        [Key]
        public string KDKIB { get; set; }
        public string NMKIB { get; set; }
        public string GOLKIB { get; set; }
    }
}