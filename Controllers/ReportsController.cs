using CrystalDecisions.CrystalReports.Engine;
using gisAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReport()
        {
            try
            {
                ReportDocument report = new ReportDocument();
                report.Load("../../Reports/Daftpenyewatanah.rpt");
                Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "MyReport.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
