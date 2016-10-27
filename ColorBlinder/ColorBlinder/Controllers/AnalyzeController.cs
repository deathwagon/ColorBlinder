using System.Web.Mvc;
using ColorBlinder.Models;

namespace ColorBlinder.Controllers
{
    public class AnalyzeController : Controller
    {
        public ActionResult Index(string url)
        {
          var model = new AnalyzeViewModel
          {
            FromImagePath = "source/path",
            ToImagePath = "target/path"
          };

          return View(model);
        }
    }
}
