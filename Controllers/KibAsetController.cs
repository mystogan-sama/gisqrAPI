using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibAsetController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibAsetController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<KibAsetRepository>>> GetAllKibAsets()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<KibAsetRepository> kibBLoks = await SelectAllKibAset(connection);
            return Ok(kibBLoks);
        }

        private static async Task<IEnumerable<KibAsetRepository>> SelectAllKibAset(SqlConnection connection)
        {
            return await connection.QueryAsync<KibAsetRepository>("select top 10 a.IDBRG, b.NMASET,b.KDASET, c.NMUNIT from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB");
        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.IDBRG,a.TAHUN, a.NOREG, a.DOKPEROLEHAN, a.TGLPEROLEHAN, a.URUTBRG, a.STATUSPENGGUNA ,b.ASETKEY, b.NMASET,b.KDASET,c.UNITKEY, c.NMUNIT, d.KDKIB from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '" + KDKIB + "%'",
                    new { ID = KDKIB });
            return Ok(kibAset);
        }

        // [Authorize]
        [HttpGet("search/{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibRepository>> GetUnitKey(string term, string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (term != null)
            {
                var kibDLok = await connection.QueryAsync<KibRepository>("select DISTINCT a.ASETKEY, b.NMASET,b.KDASET from (SELECT ak.ASETKEY, ak.UNITKEY, ak.KDKIB FROM ASET_KIBSPESIFIKASI ak WHERE UNITKEY = '"+ UNITKEY +"') a	LEFT JOIN DAFTASET b on a.ASETKEY=b.ASETKEY LEFT JOIN DAFTUNIT c on a.UNITKEY=c.UNITKEY LEFT JOIN JNSKIB d on a.KDKIB=d.KDKIB WHERE d.KDKIB LIKE '" + KDKIB + "%' and b.NMASET LIKE '%" + @term + "%'",
                    new { term = term });
                return Ok(kibDLok);
            }
            else
            {
                var kibDLok = await connection.QueryAsync<KibRepository>("select a.ASETKEY, b.NMASET,b.KDASET, count(b.ASETKEY) as RECORD from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' group by a.ASETKEY, b.NMASET,b.KDASET ORDER BY ASETKEY",
                    new { ID = UNITKEY });
                return Ok(kibDLok);
            }

        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("SELECT e.ID AS ID_LOK, a.IDBRG, a.TAHUN, a.ASETKEY, b.NMASET, f.NOFIKAT,f.KET, b.KDASET, c.UNITKEY, c.NMUNIT, d.KDKIB, e.IDBRG as IDBRG_LOK,	e.UNITKEY AS UNITKEY_LOK, e.ASETKEY AS ASETKEY_LOK,	e.TAHUN AS TAHUN_LOK, e.KET AS KET_LOK, e.METODE, e.LOKASI, e.KDKIB as KDKIB_LOK, e.URLIMG, e.URLIMG1,	e.URLIMG2, e.URLIMG3 FROM (SELECT ak.IDBRG, ak.TAHUN, ak.ASETKEY, ak.UNITKEY, ak.KDKIB FROM ASET_KIB ak WHERE KDKIB = '" + KDKIB + "') a LEFT JOIN DAFTASET b ON a.ASETKEY = b.ASETKEY LEFT JOIN DAFTUNIT c ON a.UNITKEY = c.UNITKEY LEFT JOIN WEB_KIBLOKASI e ON e.IDBRG = a.IDBRG JOIN JNSKIB d ON a.KDKIB = d.KDKIB LEFT JOIN ASET_KIBSPESIFIKASI f ON a.IDBRG = f.IDBRG WHERE c.UNITKEY LIKE '" + UNITKEY + "%' ORDER BY CASE WHEN e.LOKASI IS NOT NULL THEN 0 ELSE 1 END ASC, a.TAHUN ASC;",
                    new { ID = UNITKEY });
            return Ok(kibAset);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("SELECT e.ID AS ID_LOK, a.IDBRG, a.TAHUN, a.ASETKEY, b.NMASET, f.NOFIKAT,f.KET, b.KDASET, c.UNITKEY, c.NMUNIT, d.KDKIB, e.IDBRG as IDBRG_LOK,	e.UNITKEY AS UNITKEY_LOK, e.ASETKEY AS ASETKEY_LOK,	e.TAHUN AS TAHUN_LOK, e.KET AS KET_LOK, e.METODE, e.LOKASI, e.KDKIB as KDKIB_LOK, e.URLIMG, e.URLIMG1,	e.URLIMG2, e.URLIMG3 FROM (SELECT ak.IDBRG, ak.TAHUN, ak.ASETKEY, ak.UNITKEY, ak.KDKIB FROM ASET_KIB ak WHERE KDKIB = '" + KDKIB + "') a LEFT JOIN DAFTASET b ON a.ASETKEY = b.ASETKEY LEFT JOIN DAFTUNIT c ON a.UNITKEY = c.UNITKEY LEFT JOIN WEB_KIBLOKASI e ON e.IDBRG = a.IDBRG JOIN JNSKIB d ON a.KDKIB = d.KDKIB LEFT JOIN ASET_KIBSPESIFIKASI f ON a.IDBRG = f.IDBRG WHERE c.UNITKEY LIKE '" + UNITKEY + "%' AND b.ASETKEY LIKE '" + ASETKEY + "%' ORDER BY CASE WHEN e.LOKASI IS NOT NULL THEN 0 ELSE 1 END ASC, a.TAHUN ASC;",
                    new { ID = ASETKEY });
            return Ok(kibAset);
        }


        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}/{TAHUN}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyTahun(string KDKIB, string UNITKEY, string ASETKEY, string TAHUN)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("SELECT e.ID AS ID_LOK, a.IDBRG, a.TAHUN, a.ASETKEY, b.NMASET, f.NOFIKAT,f.KET, b.KDASET, c.UNITKEY, c.NMUNIT, d.KDKIB, e.IDBRG as IDBRG_LOK,	e.UNITKEY AS UNITKEY_LOK, e.ASETKEY AS ASETKEY_LOK,	e.TAHUN AS TAHUN_LOK, e.KET AS KET_LOK, e.METODE, e.LOKASI, e.KDKIB as KDKIB_LOK, e.URLIMG, e.URLIMG1,	e.URLIMG2, e.URLIMG3 FROM (SELECT ak.IDBRG, ak.TAHUN, ak.ASETKEY, ak.UNITKEY, ak.KDKIB FROM ASET_KIB ak WHERE KDKIB = '" + KDKIB + "') a LEFT JOIN DAFTASET b ON a.ASETKEY = b.ASETKEY LEFT JOIN DAFTUNIT c ON a.UNITKEY = c.UNITKEY LEFT JOIN WEB_KIBLOKASI e ON e.IDBRG = a.IDBRG JOIN JNSKIB d ON a.KDKIB = d.KDKIB LEFT JOIN ASET_KIBSPESIFIKASI f ON a.IDBRG = f.IDBRG WHERE c.UNITKEY LIKE '" + UNITKEY + "%' AND b.ASETKEY LIKE '" + ASETKEY + "%' and a.TAHUN LIKE '" + TAHUN + "%' ORDER BY CASE WHEN e.LOKASI IS NOT NULL THEN 0 ELSE 1 END ASC, a.TAHUN ASC;",
                    new { ID = ASETKEY });
            return Ok(kibAset);
        }

    }
}