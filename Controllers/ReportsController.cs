// using gisAPI.Utilities;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace gisAPI.Controllers
// {
//     public class ReportsController : BaseController
//     {
//         [AllowAnonymous]
//         // [Route("Financial/VarianceAnalysisReport")]
//         [HttpGet]
//         // [ClientCacheWithEtag(60)]  //1 min client side caching
//         public HttpResponseMessage FinancialVarianceAnalysisReport()
//         {
//             string reportPath = "~/Reports";
//             string reportFileName = "Daftpenyewatanah.rpt";
//             string exportFilename = "Daftpenyewatanah.pdf";

//             HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);
//             return result;
//         }

//         [AllowAnonymous]
//         [Route("Demonstration/ComparativeIncomeStatement")]
//         [HttpGet]
//         [ClientCacheWithEtag(60)]  //1 min client side caching
//         public HttpResponseMessage DemonstrationComparativeIncomeStatement()
//         {
//             string reportPath = "~/Reports/Demonstration";
//             string reportFileName = "ComparativeIncomeStatement.rpt";
//             string exportFilename = "ComparativeIncomeStatement.pdf";

//             HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);
//             return result;
//         }

//         [AllowAnonymous]
//         [Route("VersatileandPrecise/Invoice")]
//         [HttpGet]
//         [ClientCacheWithEtag(60)]  //1 min client side caching
//         public HttpResponseMessage VersatileandPreciseInvoice()
//         {
//             string reportPath = "~/Reports/VersatileandPrecise";
//             string reportFileName = "Invoice.rpt";
//             string exportFilename = "Invoice.pdf";

//             HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);
//             return result;
//         }

//         [AllowAnonymous]
//         [Route("VersatileandPrecise/FortifyFinancialAllinOneRetirementSavings")]
//         [HttpGet]
//         [ClientCacheWithEtag(60)]  //1 min client side caching
//         public HttpResponseMessage VersatileandPreciseFortifyFinancialAllinOneRetirementSavings()
//         {
//             string reportPath = "~/Reports/VersatileandPrecise";
//             string reportFileName = "FortifyFinancialAllinOneRetirementSavings.rpt";
//             string exportFilename = "FortifyFinancialAllinOneRetirementSavings.pdf";

//             HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);

//             return result;
//         }
//     }
// }
