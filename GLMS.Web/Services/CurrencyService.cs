using GLMS.API.Interfaces;
using Newtonsoft.Json.Linq;

namespace GLMS.Web.Services;

public class CurrencyService(IHttpClientFactory httpClientFactory, IConfiguration config) : ICurrencyService
{
    public async Task<decimal> GetZarRateAsync()
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            string apiKey = config["CurrencySettings:ApiKey"]!;
            string url = $"{config["CurrencySettings:BaseUrl"]}{apiKey}/latest/USD";

            var response = await client.GetStringAsync(url);
            var data = JObject.Parse(response);

            // Navigate the JSON to get the ZAR rate
            return data["conversion_rates"]?["ZAR"]?.Value<decimal>() ?? 18.50m; // Fallback rate
        }
        catch
        {
            return 18.50m; // Professional fallback if API is down
        }
    }

    public async Task<decimal> ConvertUsdToZarAsync(decimal usdAmount)
    {
        decimal rate = await GetZarRateAsync();
        return usdAmount * rate;
    }
}
