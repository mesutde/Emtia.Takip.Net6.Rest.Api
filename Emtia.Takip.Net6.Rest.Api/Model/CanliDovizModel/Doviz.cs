namespace Emtia.Takip.Net6.Rest.Api.Model.CanliDovizModel
{
    public class Doviz
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public double todayLowestSellPrice { get; set; }
            public double todayHighestSellPrice { get; set; }
            public double yesterdayClosingSellPrice { get; set; }
            public double lastBuyPrice { get; set; }
            public double lastSellPrice { get; set; }
            public double dailyChange { get; set; }
            public double dailyChangePercentage { get; set; }
            public DateTime lastUpdateDate { get; set; }
        }

        public class Market
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

        public class Root
        {
            public string code { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public Data data { get; set; }
            public Market market { get; set; }
        }
    }
}