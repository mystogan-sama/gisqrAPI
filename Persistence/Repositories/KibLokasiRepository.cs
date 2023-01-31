using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class KibLokasiRepository
    {
        [Key]
        public long ? ID { get; set; }
        public string IDBRG { get; set; }
        public string UNITKEY { get; set; }
        public string ? NMUNIT { get; set; }
        public string ASETKEY { get; set; }
        public string TAHUN { get; set; }
        public string ? NMASET { get; set; }
        public string ? KDASET { get; set; }
        public string KET { get; set; }
        public string METODE { get; set; }
        public string LOKASI { get; set; }
        public DateTime DATECREATE { get; set; } = DateTime.Now;
        public string KDKIB { get; set; }
        public string URLIMG { get; set; }
    }
}