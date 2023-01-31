using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class KibAsetRepository
    {
        public long ID { get; set; }
        [Key]
 
        public string IDBRG { get; set; }
        public string UNITKEY { get; set; }
        public string ? NMUNIT { get; set; }
        public string ? NMASET { get; set; }
        public string ? KDASET { get; set; }
        public string ASETKEY { get; set; }
        public string TAHUN { get; set; }
        public string NOREG { get; set; }
        public string DOKPEROLEHAN { get; set; }
        public DateTime TGLPEROLEHAN { get; set; }
        public int URUTBRG { get; set; }
        public string KDKIB { get; set; }
        public bool STATUSPENGGUNA { get; set; }
        public DateTime DATECREATE { get; set; }
        public DateTime DATEUPDATE { get; set; }
    }
}