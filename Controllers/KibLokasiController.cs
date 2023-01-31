using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibLokasiController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibLokasiController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<KibLokasiRepository>>> GetAllKibLokasis()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<KibLokasiRepository> kibBLoks = await SelectAllKibLokasi(connection);
            return Ok(kibBLoks);
        }

        private static async Task<IEnumerable<KibLokasiRepository>> SelectAllKibLokasi(SqlConnection connection)
        {
            return await connection.QueryAsync<KibLokasiRepository>("select a.ID, a.IDBRG,a.METODE,a.LOKASI,a.ASETKEY,a.UNITKEY,a.KDKIB,a.TAHUN, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_KIBLOKASI a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB");
        }

        // [Authorize]
        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<KibLokasiRepository>> GetKibLokasibyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLokasi = await connection.QueryAsync<KibLokasiRepository>("select a.ID,a.IDBRG,a.ASETKEY,a.UNITKEY,a.KDKIB,a.TAHUN,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_KIBLOKASI a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '" + KDKIB + "%'",
                    new { ID = KDKIB });
            return Ok(kibLokasi);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibLokasiRepository>> GetKibLokasibyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLokasi = await connection.QueryAsync<KibLokasiRepository>("select a.ID,a.IDBRG,a.ASETKEY,a.UNITKEY,a.KDKIB,a.TAHUN,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_KIBLOKASI a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(kibLokasi);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<KibLokasiRepository>> GetKibLokasibyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLokasi = await connection.QueryAsync<KibLokasiRepository>("select a.ID,a.IDBRG,a.ASETKEY,a.UNITKEY,a.KDKIB,a.TAHUN,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_KIBLOKASI a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' and b.ASETKEY LIKE '" + ASETKEY + "%'",
                    new { ID = ASETKEY });
            return Ok(kibLokasi);
        }

        // [Authorize]
        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}/{TAHUN}")]
        public async Task<ActionResult<KibLokasiRepository>> GetKibLokasibyTahun(string KDKIB, string UNITKEY, string ASETKEY, string TAHUN)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLokasi = await connection.QueryAsync<KibLokasiRepository>("select a.ID,a.IDBRG,a.ASETKEY,a.UNITKEY,a.KDKIB,a.TAHUN,a.METODE,a.LOKASI, b.NMASET,b.KDASET, c.NMUNIT, a.KET, a.URLIMG from WEB_KIBLOKASI a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '" + KDKIB + "%' and c.UNITKEY LIKE '" + UNITKEY + "%' and b.ASETKEY LIKE '" + ASETKEY + "%' and a.TAHUN LIKE '" + TAHUN + "%'",
                    new { ID = ASETKEY });
            return Ok(kibLokasi);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<KibLokasiRepository>>> AddKibLokasi(KibLokasiRepository kibLokasi)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("insert into WEB_KIBLOKASI (IDBRG, UNITKEY, ASETKEY,TAHUN, KET, METODE, LOKASI, KDKIB, URLIMG) values (@IDBRG, @UNITKEY, @ASETKEY, @TAHUN, @KET, @METODE, @LOKASI, @KDKIB, @URLIMG)", kibLokasi);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllKibLokasi(connection));
        }

        // [Authorize]
        [HttpPut("IDBRG")]
        public async Task<ActionResult<List<KibLokasiRepository>>> UpdateKibLokasi(string IDBRG, KibLokasiRepository kibLokasi)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("UPDATE WEB_KIBLOKASI SET KET = @KET, METODE = @METODE, LOKASI = @LOKASI, URLIMG = @URLIMG WHERE IDBRG = @IDBRG", kibLokasi);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllKibLokasi(connection));
        }

        // [Authorize]
        [HttpDelete("{IDBRG}")]
        public async Task<ActionResult<KibLokasiRepository>> DeleteKibLokasi(string IDBRG)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibLokasi = await connection.ExecuteAsync("DELETE FROM WEB_KIBLOKASI WHERE IDBRG = @IDBRG", new { IDBRG = IDBRG });
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(kibLokasi);
            // return Ok(await SelectAllKibLokasi(connection));
        }
    }
}