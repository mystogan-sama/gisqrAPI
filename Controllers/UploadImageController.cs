using System.Data.SqlClient;
using Dapper;
using gisAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadImageController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadImageController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadImage>>> AddImage([FromForm] UploadImage uploadImage)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(uploadImage.files.FileName);
                string extension = Path.GetExtension(uploadImage.files.FileName);
                uploadImage.FILENAME = fileName + extension;
                string path = Path.Combine(wwwRootPath+ "//Upload//", fileName + extension);
                Console.WriteLine(path);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadImage.files.CopyToAsync(fileStream);
                }
                using var connection = new SqlConnection(_config.GetConnectionString("Default"));
                await connection.ExecuteAsync("insert into WEB_GAMBARKIB (IDBRG, FILENAME, KET) values (@IDBRG, @FILENAME, @KET)", uploadImage);
                return RedirectToAction(nameof(Index));
            }
            return View(uploadImage);
            // kibDLok.ID = Guid.NewGuid();
            // using var connection = new SqlConnection(_config.GetConnectionString("SqlConnection"));
            // await connection.ExecuteAsync("insert into WEB_KIBDLOK (IDBRG, URUTTRANS, METODE, LOKASI) values (@IDBRG, @URUTTRANS, @METODE, @LOKASI)", kibDLok);
            // return CreatedAtAction(nameof(SelectAllKibBLok), new { id = kibBLok.ID }, kibBLok);
            // return Ok(await SelectAllKibDLoks(connection));
        }
    }
}