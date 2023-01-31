using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class SertifikatController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public SertifikatController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SertifikatRepository>>> GetAllSertifikats()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<SertifikatRepository> Sertifikats = await SelectAllSertifikat(connection);
            return Ok(Sertifikats);
        }

        private static async Task<IEnumerable<SertifikatRepository>> SelectAllSertifikat(SqlConnection connection)
        {
            return await connection.QueryAsync<SertifikatRepository>("select b.IDBRG,b.NOFIKAT,b.TAHUN,b.DESA,b.BLOK,b.ALAMAT,b.KORDINAT,b.LUAS,b.URLIMG,b.KET, b.NOFIKAT + ' - ' +d.NMUNIT as NMASET from ASET_KIBSPESIFIKASI a join WEB_SERT b on a.IDBRG=b.IDBRG join DAFTASET c on a.ASETKEY=c.ASETKEY join DAFTUNIT d on a.UNITKEY=d.UNITKEY join JNSKIB e on a.KDKIB=e.KDKIB where  e.KDKIB LIKE '01%' and d.UNITKEY LIKE '5081_%'");
        }

        // [Authorize]
        [HttpGet("unit")]
        public async Task<ActionResult<DaftUnitRepository>> GetUnit()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<DaftUnitRepository>("select DISTINCT a.UNITKEY, b.NMUNIT from ( SELECT  ak.UNITKEY FROM ASET_KIBSPESIFIKASI ak WHERE KDKIB = '01') a LEFT JOIN DAFTUNIT b on a.UNITKEY=b.UNITKEY");
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("blok/{UNITKEY}")]
        public async Task<ActionResult<SertifikatRepository>> GetBlok(string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<SertifikatRepository>("select a.BLOK, count(a.BLOK) from WEB_SERT a join DAFTUNIT b on a.UNITKEY=b.UNITKEY where a.UNITKEY LIKE '" + UNITKEY + "%' group by a.BLOK");
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("nominal/{IDBRG}")]
        public async Task<ActionResult<NominalRepository>> GetNominalPenyewa(string IDBRG)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<NominalRepository>("SELECT SUM(BESARANSEWA) AS TOTALNOMINAL FROM WEB_SEWA WHERE IDBRG LIKE '" + IDBRG + "%' and STATUS LIKE '1%'",
                    new { ID = IDBRG });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("idbrg/{IDBRG}")]
        public async Task<ActionResult<PenyewaRepository>> GetSertifikatbyIdbrg(string IDBRG)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<PenyewaRepository>("select a.IDBRG, a.NOFIKAT,a.TAHUN,a.URLIMG,b.ID, b.IDBRG, b.NOSERTIFIKAT, b.NAMA,b.ALAMAT,b.LUAS,b.PERUNTUKAN,b.STARTDATE,b.ENDDATE,b.BESARANSEWA,b.DESA,b.NOHAKPAKAI,b.TAHUNHAKPAKAI,b.NOSKBUP,b.NOMOU, b.METODE, b.LOKASI, b.KET, b.STATUS, a.NOFIKAT + ' - ' +a.TAHUN as TAHUNSERT, a.ALAMAT as ALAMATTANAH, a.LUAS as LUASTANAH from WEB_SERT a join WEB_SEWA b on a.IDBRG=b.IDBRG where a.IDBRG LIKE '" + IDBRG + "%'",
                    new { ID = IDBRG });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("unitkey/{UNITKEY}")]
        public async Task<ActionResult<SertifikatRepository>> GetSertifikatbyUnitkey(string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<SertifikatRepository>("select b.IDBRG, a.NOFIKAT, d.NMUNIT, b.TAHUN, a.DESA, a.BLOK, a.ALAMAT, a.KORDINAT, a.LUAS, a.URLIMG, a.KET, b.NOFIKAT + ' - ' +d.NMUNIT as NMASET from WEB_SERT a join ASET_KIBSPESIFIKASI b on a.IDBRG=b.IDBRG join DAFTASET c on a.ASETKEY=c.ASETKEY join DAFTUNIT d on a.UNITKEY=d.UNITKEY  where d.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("unit/{UNITKEY}")]
        public async Task<ActionResult<KibSertifikatRepository>> GetAsetbyUnitkey(string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<KibSertifikatRepository>("select a.IDBRG, a.UNITKEY,a.ASETKEY,a.NOFIKAT, a.TAHUN, a.KET, a.LUASTNH, a.ALAMAT, a.NOFIKAT + ' - ' + c.NMASET as NMASET from ASET_KIBSPESIFIKASI a join DAFTASET c on a.ASETKEY=c.ASETKEY join DAFTUNIT d on a.UNITKEY=d.UNITKEY join JNSKIB e on a.KDKIB=e.KDKIB where  a.KDKIB LIKE '01%' and d.UNITKEY LIKE '" + UNITKEY + "%'",
                    new { ID = UNITKEY });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("{NOFIKAT}")]
        public async Task<ActionResult<SertifikatRepository>> GetSertifikatIdbrg(string NOFIKAT)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<SertifikatRepository>("select a.IDBRG, a.NOFIKAT,a.URLIMG, b.METODE, b.LOKASI from WEB_SERT a join WEB_SEWA b on a.NOFIKAT=b.NOSERTIFIKAT where a.NOSERTIFIKAT LIKE '" + NOFIKAT + "%'",
                    new { ID = NOFIKAT });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpGet("{UNITKEY}/{BLOK}")]
        public async Task<ActionResult<SertifikatRepository>> GetSertifikatBlok(string UNITKEY, string BLOK)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var Sertifikat = await connection.QueryAsync<SertifikatRepository>("select a.IDBRG, a.UNITKEY,a.ASETKEY,a.NOFIKAT, a.TAHUN, a.KET,a.BLOK, a.LUAS, a.ALAMAT,a.KORDINAT, a.URLIMG, a.NOFIKAT + ' - ' + c.NMASET as NMASET from WEB_SERT a join DAFTASET c on a.ASETKEY=c.ASETKEY join DAFTUNIT d on a.UNITKEY=d.UNITKEY  where d.UNITKEY LIKE '" + UNITKEY + "%' and a.BLOK LIKE '" + BLOK + "%'",
                    new { ID = BLOK });
            return Ok(Sertifikat);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<SertifikatRepository>>> AddSertifikat(SertifikatRepository Sertifikat)
        {
            // kibBLok.ID = Guid.NewGuid();
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            await connection.ExecuteAsync("insert into WEB_SERT (IDBRG, ASETKEY, UNITKEY, NOFIKAT, TAHUN, DESA, BLOK, ALAMAT, KORDINAT, LUAS, URLIMG, KET) values (@IDBRG, @ASETKEY, @UNITKEY, @NOFIKAT, @TAHUN, @DESA, @BLOK, @ALAMAT, @KORDINAT, @LUAS, @URLIMG, @KET)", Sertifikat);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            return Ok(await SelectAllSertifikat(connection));
        }
    }
}