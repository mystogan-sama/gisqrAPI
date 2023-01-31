using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class PenyewaRepository
    {
        public string ? ID { get; set; }
        [Key]
        public string IDBRG { get; set; }
        public string NOSERTIFIKAT { get; set; }
        public string ? TAHUNSERT { get; set; }
        public string NAMA { get; set; }
        public string ALAMAT { get; set; }
        public string ? ALAMATTANAH { get; set; }
        public string LUAS { get; set; }
        public string ? LUASTANAH { get; set; }
        public string PERUNTUKAN { get; set; }
        public string STARTDATE { get; set; }
        public string ENDDATE { get; set; }
        public int BESARANSEWA { get; set; }
        public string ? URLIMGSEWA { get; set; }
        public string METODE { get; set; }
        public string LOKASI { get; set; }
        public string DESA { get; set; }
        public string NOHAKPAKAI { get; set; }
        public string TAHUNHAKPAKAI { get; set; }
        public string NOSKBUP { get; set; }
        public string NOMOU { get; set; }
        public string KET { get; set; }
        public string STATUS { get; set; }
        public string ? TOTALNOMINAL { get; set; }
    }
}