using System.Data;
using System.Net;
using AsetQrApi.DTO;
using AutoWrapper.Wrappers;
using Dapper;
using gisAPI.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class BarangController : BaseController
    {
        private readonly IDbContext _context;

        public BarangController(IDbContext context)
        {
            _context = context;
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetByIdBarang(BarangDto dto)
        {
            var barang = await _context.Connection
              .QueryAsync("QR_GETIDBRG", new { IDBRG = dto.Id }, commandType: CommandType.StoredProcedure);

            var list = barang.ToList();

            if (!list.Any())
                throw new ApiException("Barang tidak ditemukan.", (int)HttpStatusCode.BadRequest);

            var photoDir = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Upload", dto.Id));

            if (!Directory.Exists(photoDir))
                Directory.CreateDirectory(photoDir);

            var files = Directory.GetFiles(photoDir).Select(p => Path.GetFileName(p)).ToList();

            var body = new { barang = barang, foto = files };

            return Ok(body);
        }

        // [Authorize]
        [HttpGet("{noPolisi}")]
        public async Task<IActionResult> GetByNoPolisi(string noPolisi)
        {
            var barang = await _context.Connection
              .QueryAsync("QR_GETNOPOL", new { NOPOL = noPolisi }, commandType: CommandType.StoredProcedure);

            var list = barang.ToList();

            if (!list.Any())
                throw new ApiException("Barang tidak ditemukan.", (int)HttpStatusCode.BadRequest);

            var id = (string)list[0].IDBRG;

            var photoDir = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Upload", id));

            if (!Directory.Exists(photoDir))
                Directory.CreateDirectory(photoDir);

            var files = Directory.GetFiles(photoDir).Select(p => Path.GetFileName(p)).ToList();

            var body = new { barang = barang, foto = files };

            return Ok(body);
        }

        // [Authorize]
        [HttpGet("print/{UNITKEY}/{ASETKEY}")]
        public async Task<IActionResult> Print(string UNITKEY, string ASETKEY)
        {
            var result = await _context.Connection
              .QueryAsync("QR_GETDAFTBRG", new { UNITKEY = UNITKEY, ASETKEY = ASETKEY }, commandType: CommandType.StoredProcedure);

            return Ok(result);
        }
    }
}