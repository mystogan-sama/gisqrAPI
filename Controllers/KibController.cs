using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<JnsKibRepository>>> GetAllJnsKib()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<JnsKibRepository> jnsKibs = await SelectAllJnsKibs(connection);
            return Ok(jnsKibs);
        }

        private static async Task<IEnumerable<JnsKibRepository>> SelectAllJnsKibs(SqlConnection connection)
        {
            return await connection.QueryAsync<JnsKibRepository>("SELECT * FROM JNSKIB");
        }

        // [Authorize]
        [HttpGet("{IDBRG}/{search}")]
        public async Task<ActionResult<KibRepository>> GetUnitKey(string search, string IDBRG)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (search != null)
            {
                var kibDLok = await connection.QueryAsync<KibRepository>("select TOP 10 b.IDBRG,b.ASETKEY,b.UNITKEY,b.KDKIB, a.NMASET,a.KDASET, a.NMASET as KDASET from ASET_KIB b join DAFTASET a on b.ASETKEY=a.ASETKEY where a.KDASET LIKE '%'+@search+'%'",
                    new { search = search });
                return Ok(kibDLok);
            }
            else
            {
                var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where IDBRG = @IDBRG",
                    new { IDBRG = IDBRG });
                return Ok(kibDLok);
            }

        }

        // [Authorize]
        [HttpGet("{NMKIB}")]
        public async Task<ActionResult<KibRepository>> GetKibbyNmKib(string NMKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<KibRepository>("SELECT * FROM JNSKIB WHERE NMKIB LIKE '" + NMKIB + "%'",
                    new { ID = NMKIB });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<KibRepository>> GetKibbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<KibRepository>("select a.IDBRG, b.NMASET,b.KDASET, c.NMUNIT from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '" + KDKIB + "%'",
                    new { ID = KDKIB });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibRepository>> GetKibbyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<KibRepository>("select a.IDBRG, b.NMASET,b.KDASET, c.NMUNIT from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<KibRepository>> GetKibbyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<KibRepository>("select a.IDBRG,a.TAHUN, a.NOREG, a.DOKPEROLEHAN, a.TGLPEROLEHAN, a.URUTBRG, a.STATUSPENGGUNA ,b.ASETKEY, b.NMASET,b.KDASET,c.UNITKEY, c.NMUNIT, d.KDKIB from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' and b.ASETKEY LIKE '" + ASETKEY + "%'",
                    new { ID = ASETKEY });
            return Ok(kib);
        }
    }
}