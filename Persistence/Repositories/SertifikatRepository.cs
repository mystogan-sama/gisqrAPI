using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class SertifikatRepository
    {
        public long ID { get; set; }
        [Key]
 
        public string IDBRG { get; set; }
        public string NOFIKAT { get; set; }
        public string ? NMASET { get; set; }
        public string ASETKEY { get; set; }
        public string ? NMUNIT { get; set; }
        public string UNITKEY { get; set; }
        public string TAHUN { get; set; }
        public string DESA { get; set; }
        public string BLOK { get; set; }
        public string ALAMAT { get; set; }
        public string KORDINAT { get; set; }
        public string LUAS { get; set; }
        public string ? LUASTNH { get; set; }
        public string URLIMG { get; set; }
        public string KET { get; set; }
        public string ? METODE { get; set; }
        public string ? LOKASI { get; set; }
    }
}