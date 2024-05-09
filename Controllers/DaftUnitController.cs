using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class DaftUnitController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public DaftUnitController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DaftUnitRepository>>> GetAllUnits()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<DaftUnitRepository> units = await SelectAllDaftUnits(connection);
            return Ok(units);
        }

        private static async Task<IEnumerable<DaftUnitRepository>> SelectAllDaftUnits(SqlConnection connection)
        {
            return await connection.QueryAsync<DaftUnitRepository>("select a.UNITKEY, a.NMUNIT,a.KDLEVEL from DAFTUNIT a WHERE a.KDLEVEL in ('3', '4')");
        }

        // [Authorize]
        [HttpGet("{UNITKEY}")]
        public async Task<ActionResult<KibRepository>> GetUnitKey(string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where UNITKEY = @UNITKEY",
                    new { UNITKEY = UNITKEY });
            return Ok(kibDLok);
        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<DaftUnitRepository>> GetKibbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<DaftUnitRepository>("select DISTINCT a.UNITKEY, b.NMUNIT from (SELECT ak.UNITKEY FROM ASET_KIB ak WHERE KDKIB = '"+KDKIB+"' ) a LEFT JOIN DAFTUNIT b on a.UNITKEY=b.UNITKEY ORDER BY UNITKEY",
                    new { ID = KDKIB });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<DaftAsetRepository>> GetKibbyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<DaftAsetRepository>("select DISTINCT a.ASETKEY, b.NMASET, b.KDASET from (SELECT ak.UNITKEY, ak.ASETKEY FROM ASET_KIB ak WHERE KDKIB = '"+KDKIB+"' ) a LEFT JOIN DAFTASET b on a.ASETKEY=b.ASETKEY LEFT JOIN DAFTUNIT c on a.UNITKEY=c.UNITKEY WHERE c.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(kib);
        }
    }
}