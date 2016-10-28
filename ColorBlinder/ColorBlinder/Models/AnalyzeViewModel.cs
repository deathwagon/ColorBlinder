using ColorBlinder.Controllers;
using System.Web;

namespace ColorBlinder.Models
{
  public class AnalyzeViewModel
  {
    public string FilterType { get; set; }
    public string OriginalImagePath { get; set; }
    public string FilterImagePath { get; set; }
    public IHtmlString JsonData { get; set; }
  }
}