using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Bots.Stock
{
    public class GetStockCommand
    {
        public string Command = "/stock={code}";

        public async Task Get(string command, IServiceProvider service)
        {
            var httpFactory = service.GetService<IHttpClientFactory>();
            var httpClient = httpFactory.CreateClient();

            string stockCode = "aapl.us";
            var response = await httpClient.GetAsync($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv");
            if(!response.IsSuccessStatusCode)
            {

            }
            var responseText = await response.Content.ReadAsStringAsync();
            var entries = responseText.Split('\n').Last().Split(',');
            var result = $"{entries[0]} quote is {double.Parse(entries[5]):C} per share.";
        }
    }
}