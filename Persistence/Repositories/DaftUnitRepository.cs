using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class DaftUnitRepository
    {
        public long ID { get; set; }
        [Key]
        public string UNITKEY { get; set; }
        public string KDUNIT { get; set; }
        public string NMUNIT { get; set; }
        public int KDLEVEL { get; set; }
        public string TYPE { get; set; }
        public string AKROUNIT { get; set; }
        public string ALAMAT { get; set; }
        public string TELEPON { get; set; }
        public int STAKTIF { get; set; }
        public DateTime DATECREATE { get; set; }
    }
}