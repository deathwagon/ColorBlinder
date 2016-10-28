using System.Web.Mvc;
using ColorBlinder.Models;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web;

namespace ColorBlinder.Controllers
{
  public class AnalyzeController : Controller
  {
    public async Task<ActionResult> Index(string url)
    {
      var httpClient = new CbHttpClient();
      var analyzeResult = await httpClient.RunAsync(url);
      var jsSerializer = new JavaScriptSerializer();
      var jsonData = jsSerializer.Serialize(analyzeResult);

      var model = new AnalyzeViewModel
      {
        FilterType = "Achromatomaly",
        OriginalImagePath = analyzeResult.original.Url,
        FilterImagePath = analyzeResult.Achromatomaly.Url,
        JsonData = new HtmlString(jsonData)
      };

      return View(model);
    }
  }
}
