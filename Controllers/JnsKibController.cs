using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class JnsKibController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public JnsKibController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
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
            return await connection.QueryAsync<JnsKibRepository>("SELECT * FROM JNSKIB WHERE KDKIB in ('01','03', '04')");
        }

        // [Authorize]
        [HttpGet("{kdKib}")]
        public async Task<ActionResult<KibRepository>> GetKib(string kdKib)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var jnsKib = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where KDKIB = @KDKIB",
                    new { KDKIB = kdKib });
            return Ok(jnsKib);
        }
    }
}