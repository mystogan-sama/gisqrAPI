using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class KibAsetRepository
    {
        public long ID { get; set; }
        [Key]
 
        public long ID_LOK { get; set; }
        public string IDBRG { get; set; }
        public string NOFIKAT { get; set; }
        public string UNITKEY { get; set; }
        public string ? NMUNIT { get; set; }
        public string ? NMASET { get; set; }
        public string ? KDASET { get; set; }
        public string ASETKEY { get; set; }
        public string TAHUN { get; set; }
        public string NOREG { get; set; }
        public string DOKPEROLEHAN { get; set; }
        public string IDBRG_LOK { get; set; }
        public string UNITKEY_LOK { get; set; }
        public string ASETKEY_LOK { get; set; }
        public string TAHUN_LOK { get; set; }
        public string KET { get; set; }
        public string KET_LOK { get; set; }
        public string METODE { get; set; }
        public string LOKASI { get; set; }
        public string KDKIB { get; set; }
        public DateTime TGLPEROLEHAN { get; set; }
        public int URUTBRG { get; set; }
        public string KDKIB_LOK { get; set; }
        public string URLIMG { get; set; }
        public string URLIMG1 { get; set; }
        public string URLIMG2 { get; set; }
        public string URLIMG3 { get; set; }
        public bool STATUSPENGGUNA { get; set; }
        public DateTime DATECREATE { get; set; }
        public DateTime DATEUPDATE { get; set; }
    }
}