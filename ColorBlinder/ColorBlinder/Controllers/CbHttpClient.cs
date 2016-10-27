using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ColorBlinder.Controllers
{
  public class AnalyzeResult
  {
    public string original { get; set; }

    public string filterOne { get; set; }
  }

  public class CbHttpClient
  {
    private readonly HttpClient _client = new HttpClient();

    private static async Task<AnalyzeResult> GetFilterImage(string url, HttpClient client)
    {
      AnalyzeResult result = null;
      var response = await client.GetAsync(url);

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

      var uri = new Uri("http://localhost:1905/v1/screenfilter?url=" + url);
      _client.BaseAddress = uri;
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      result = await GetFilterImage(uri.ToString(), _client);

      return result;
    }
  }
}