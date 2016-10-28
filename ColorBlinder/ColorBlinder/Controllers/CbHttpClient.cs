using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ColorBlinder.Controllers
{
  public class AnalyzeResults
  {
    public AnalyzeResult original { get; set; }

    public AnalyzeResult Achromatomaly { get; set; }

    public AnalyzeResult Protanopia { get; set; }

    public AnalyzeResult Protanomaly { get; set; }

    public AnalyzeResult Deuteranopia { get; set; }

    public AnalyzeResult Deuteranomaly { get; set; }

    public AnalyzeResult Tritanopia { get; set; }

    public AnalyzeResult Tritanomaly { get; set; }

    public AnalyzeResult Achromatopsia { get; set; }
  }

  public class AnalyzeResult
  {
    public string Url { get; set; }
    
    public double? Score { get; set; }

    public string EdgeUrl => Url.Insert(Url.LastIndexOf('/'), "/edges");
  }


  public class CbHttpClient
  {
    private readonly HttpClient _client = new HttpClient();

    private static async Task<AnalyzeResults> GetFilterImage(string url, HttpClient client)
    {
      AnalyzeResults result = null;
      var response = await client.GetAsync(url);

      if (response.IsSuccessStatusCode)
      {
        var responseBody = await response.Content.ReadAsStringAsync();
        var jsSerializer = new JavaScriptSerializer();
        result = jsSerializer.Deserialize<AnalyzeResults>(responseBody);
      }

      return result;
    }

    public async Task<AnalyzeResults> RunAsync(string url)
    {
      AnalyzeResults result = null;

      var uri = new Uri("http://10.33.168.147:8181/v1/screenfilter?url=" + url + "?staticfailovertest=1");
      _client.BaseAddress = uri;
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      result = await GetFilterImage(uri.ToString(), _client);

      return result;
    }
  }
}