using System.Web.Mvc;
using ColorBlinder.Models;
using System.Threading.Tasks;

namespace ColorBlinder.Controllers
{
  public class AnalyzeController : Controller
  {
    public async Task<ActionResult> Index(string url)
    {
      var httpClient = new CbHttpClient();
      var analyzeResult =  await httpClient.RunAsync(url);

      var model = new AnalyzeViewModel
      {
        FilterType = "Achromatomaly",
        OriginalImagePath = analyzeResult.original,
        FilterImagePath = analyzeResult.Achromatomaly,
        AllFilterImagePaths = string.Join("|", "Achromatomaly="+analyzeResult.Achromatomaly,
                                               "Achromatopsia=" + analyzeResult.Achromatopsia,
                                               "Deuteranomaly=" + analyzeResult.Deuteranomaly,
                                               "Deuteranopia=" + analyzeResult.Deuteranopia,
                                               "Protanomaly=" + analyzeResult.Protanomaly,
                                               "Protanopia=" + analyzeResult.Protanopia,
                                               "Tritanomaly=" + analyzeResult.Tritanomaly,
                                               "Tritanopia=" + analyzeResult.Tritanopia) 
      };

      return View(model);
    }
  }
}
