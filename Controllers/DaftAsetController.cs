using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class DaftAsetController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public DaftAsetController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<DaftAsetRepository>>> GetAllAset()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<DaftAsetRepository> asets = await SelectAllDaftAsets(connection);
            return Ok(asets);
        }

        [HttpGet("{KDASET}/{search}")]
        public async Task<ActionResult<DaftAsetRepository>> GetUnitKey(string search, string KDASET)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (search != null)
            {
                var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select TOP 10 b.KDASET, b.NMASET, b.KDASET +' - '+ b.NMASET as KDASET from  DAFTASET b where b.KDASET LIKE '%'+@search+'%'",
                    new { search = search });
                return Ok(kibDLok);
            }
            else
            {
                var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select * from ASET_KIB where KDASET = @KDASET",
                    new { KDASET = KDASET });
                return Ok(kibDLok);
            }

        }

        //       [HttpGet("{ASETKEY}")]
        // public async Task<ActionResult<DaftAsetRepository>> GetAsetKib(string ASETKEY)
        // {
        //     using var connection = new SqlConnection(_config.GetConnectionString("Default"));
        //     var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select * from ASET_KIB where ASETKEY = @ASETKEY",
        //             new { ASETKEY = ASETKEY  });
        //     return Ok(kibDLok);
        // }

        private static async Task<IEnumerable<DaftAsetRepository>> SelectAllDaftAsets(SqlConnection connection)
        {
            return await connection.QueryAsync<DaftAsetRepository>("select * from DAFTASET");
        }
        // [Authorize]
        [HttpGet("kib/{KDKIB}")]
        public async Task<ActionResult<DaftUnitRepository>> GetUnitbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<DaftUnitRepository>("select c.UNITKEY, c.NMUNIT,c.KDLEVEL, d.KDKIB, count(c.UNITKEY) from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.KDLEVEL in ('3', '4') group by c.UNITKEY, c.NMUNIT,c.KDLEVEL, d.KDKIB ORDER BY KDLEVEL, UNITKEY",
                    new { ID = KDKIB });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<DaftUnitRepository>> GetSearchUnit(string term)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (term != null)
            {
                var kibDLok = await connection.QueryAsync<DaftUnitRepository>("select a.UNITKEY, a.NMUNIT,a.KDLEVEL from DAFTUNIT a WHERE a.KDLEVEL in ('3', '4') and a.NMUNIT LIKE '%" + @term + "%'",
                    new { term = term });
                return Ok(kibDLok);
            }
            else
            {
                var kibDLok = await connection.QueryAsync<DaftUnitRepository>("select a.UNITKEY, a.NMUNIT,a.KDLEVEL from DAFTUNIT a WHERE a.KDLEVEL in ('3', '4') and a.NMUNIT LIKE '%" + @term + "%'",
                    new { term = term });
                return Ok(kibDLok);
            }

        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<DaftAsetRepository>> GetKibbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<DaftAsetRepository>("select b.ASETKEY, b.NMASET, count(b.ASETKEY) from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '" + KDKIB + "%' group by b.ASETKEY, b.NMASET",
                    new { ID = KDKIB });
            return Ok(kib);
        }

        // [Authorize]
        [HttpGet("{ASETKEY}")]
        public async Task<ActionResult<KibRepository>> GetASETKEY(string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where ASETKEY = @ASETKEY",
                    new { ASETKEY = ASETKEY });
            return Ok(kibDLok);
        }

        // [Authorize]
        [HttpGet("tahun/{KDKIB}")]
        public async Task<ActionResult<KibAsetRepository>> GetTahunbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.TAHUN, count(a.TAHUN) from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' group by a.TAHUN",
                    new { ID = KDKIB });
            return Ok(kibAset);
        }

        // [Authorize]
        [HttpGet("tahun/{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetTahunbyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.TAHUN, count(a.TAHUN) from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' group by a.TAHUN",
                    new { ID = UNITKEY });
            return Ok(kibAset);
        }

        // [Authorize]
        [HttpGet("tahun/{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.TAHUN, count(a.TAHUN) from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' and b.ASETKEY LIKE '" + ASETKEY + "%'group by a.TAHUN",
                    new { ID = ASETKEY });
            return Ok(kibAset);
        }

        // [Authorize]
        [HttpGet("tahun/{TAHUN}")]
        public async Task<ActionResult<KibRepository>> GetTAHUN(string TAHUN)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where TAHUN = @TAHUN",
                    new { TAHUN = TAHUN });
            return Ok(kibDLok);
        }
    }
}