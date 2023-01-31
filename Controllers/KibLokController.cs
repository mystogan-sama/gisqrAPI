using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibLokController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibLokController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<KibLokRepository>>> GetAllKibLoks()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<KibLokRepository> kibBLoks = await SelectAllKibLok(connection);
            return Ok(kibBLoks);
        }

        private static async Task<IEnumerable<KibLokRepository>> SelectAllKibLok(SqlConnection connection)
        {
            return await connection.QueryAsync<KibLokRepository>("select * from WEB_KIBLOK");
        }

        // [Authorize]
        [HttpGet("{kibLokId}")]
        public async Task<ActionResult<KibLokRepository>> GetKibLok(string kibLokId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLok = await connection.QueryFirstAsync<KibLokRepository>("select * from WEB_KIBLOK where ID = @ID",
                    new { ID = kibLokId });
            return Ok(kibLok);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<KibLokRepository>>> AddKibLok(KibLokRepository kibLok)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("insert into WEB_KIBLOK (IDBRG, UNITKEY, ASETKEY, KET, METODE, LOKASI, KDKIB) values (@IDBRG, @UNITKEY, @ASETKEY, @KET, @METODE, @LOKASI, @KDKIB)", kibLok);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllKibLok(connection));
        }
    }
}