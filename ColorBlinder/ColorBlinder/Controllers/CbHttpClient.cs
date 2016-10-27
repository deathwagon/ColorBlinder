using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ColorBlinder.Controllers
{
  public class AnalyzeResult
  {
    public string OriginalImagePath { get; set; }
    public string FilterImagePath { get; set; }
  }

  public class CbHttpClient
  {
    private static readonly HttpClient Client = new HttpClient();

    static async Task<AnalyzeResult> GetFilterImage(string url)
    {
      AnalyzeResult result = null;
      var response = await Client.GetAsync(url);

      if (response.IsSuccessStatusCode)
      {
        var responseBody = await response.Content.ReadAsStringAsync();
        var jsSerializer = new JavaScriptSerializer();
        result = jsSerializer.Deserialize<AnalyzeResult>(responseBody);
      }

      return result;
    }

    public async Task<AnalyzeResult> RunAsync(string url)
    {
      AnalyzeResult result = null;

      var uri = new Uri("http://localhost:2974/v1/screenfilter?url=" + url);
      Client.BaseAddress = uri;
      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      result = await CbHttpClient.GetFilterImage(uri.ToString());

      return result;
    }
  }
}