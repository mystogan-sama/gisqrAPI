using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class DaftAsetRepository
    {
        public long ID { get; set; }
        [Key]
        public string ASETKEY { get; set; }
        public string MTGLEVEL { get; set; }
        public string KDASET { get; set; }
        public string NMASET { get; set; }
        public string TYPE { get; set; }
        public double UMEKO { get; set; }
        public decimal NKLAS { get; set; }
        public DateTime DATECREATE { get; set; }
        public DateTime DATEUPDATE { get; set; }
    }
}