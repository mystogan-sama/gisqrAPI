using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class PenyewaController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public PenyewaController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<PenyewaRepository>>> GetAllPenyewas()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<PenyewaRepository> Penyewas = await SelectAllPenyewa(connection);
            return Ok(Penyewas);
        }

        private static async Task<IEnumerable<PenyewaRepository>> SelectAllPenyewa(SqlConnection connection)
        {
            return await connection.QueryAsync<PenyewaRepository>("select * from WEB_SEWA");
        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<PenyewaRepository>> GetPenyewabyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Penyewa = await connection.QueryAsync<PenyewaRepository>("select a.IDBRG,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_Penyewa a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '" + KDKIB + "%'",
                    new { ID = KDKIB });
            return Ok(Penyewa);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<PenyewaRepository>> GetPenyewabyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Penyewa = await connection.QueryAsync<PenyewaRepository>("select a.IDBRG,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_Penyewa a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(Penyewa);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<PenyewaRepository>> GetPenyewabyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Penyewa = await connection.QueryAsync<PenyewaRepository>("select a.IDBRG,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_Penyewa a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' and b.ASETKEY LIKE '" + ASETKEY + "%'",
                    new { ID = ASETKEY });
            return Ok(Penyewa);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<PenyewaRepository>>> AddPenyewa(PenyewaRepository Penyewa)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("insert into WEB_SEWA (IDBRG, NOSERTIFIKAT, NAMA, ALAMAT, LUAS, PERUNTUKAN, STARTDATE, ENDDATE, BESARANSEWA, URLIMGSEWA, METODE, LOKASI, DESA, NOHAKPAKAI, TAHUNHAKPAKAI, NOSKBUP, NOMOU, KET, STATUS) values (@IDBRG, @NOSERTIFIKAT, @NAMA, @ALAMAT, @LUAS, @PERUNTUKAN, @STARTDATE, @ENDDATE, @BESARANSEWA, @URLIMGSEWA, @METODE, @LOKASI, @DESA, @NOHAKPAKAI, @TAHUNHAKPAKAI, @NOSKBUP, @NOMOU, @KET, @STATUS)", Penyewa);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllPenyewa(connection));
        }

        // [Authorize]
        [HttpPut("ID")]
        public async Task<ActionResult<List<PenyewaRepository>>> UpdatePenyewa(string ID, PenyewaRepository penyewa)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("UPDATE WEB_SEWA SET NOSERTIFIKAT = @NOSERTIFIKAT, NAMA  = @NAMA, ALAMAT = @ALAMAT, LUAS = @LUAS, PERUNTUKAN = @PERUNTUKAN, STARTDATE = @STARTDATE, ENDDATE = @ENDDATE, BESARANSEWA = @BESARANSEWA, URLIMGSEWA = @URLIMGSEWA, METODE = @METODE, LOKASI = @LOKASI, DESA = @DESA, NOHAKPAKAI = @NOHAKPAKAI, TAHUNHAKPAKAI = @TAHUNHAKPAKAI, NOSKBUP = @NOSKBUP, NOMOU = @NOMOU, KET = @KET, STATUS = @STATUS WHERE ID = @ID", penyewa);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllPenyewa(connection));
        }

        // [Authorize]
        [HttpDelete("{ID}")]
        public async Task<ActionResult<PenyewaRepository>> DeletePenyewa(string ID)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var penyewa = await connection.ExecuteAsync("DELETE FROM WEB_SEWA WHERE ID = @ID", new { ID = ID });
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(penyewa);
            // return Ok(await SelectAllKibLokasi(connection));
        }
    }
}