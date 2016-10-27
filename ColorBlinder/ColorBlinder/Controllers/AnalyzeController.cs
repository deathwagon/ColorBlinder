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
        OriginalImagePath = analyzeResult.OriginalImagePath,
        FilterImagePath = analyzeResult.FilterImagePath
      };

      return View(model);
    }
  }
}
