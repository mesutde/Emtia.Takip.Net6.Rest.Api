﻿using RestSharp;
using HtmlAgilityPack;
using Newtonsoft.Json;
using static Emtia.Takip.Net6.Rest.Api.Model.CanliDovizModel.Doviz;

namespace Emtia.Takip.Net6.Rest.Api.Helper
{
    public static class ExchangeHelperApi
    {
        public static async Task<HaremRoot> GetAllHaremExchange()
        {
            var options = new RestClientOptions("https://www.haremaltin.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/dashboard/ajax/doviz", Method.Get);
            request.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("x-requested-with", " XMLHttpRequest");
            request.AddHeader("Cookie", "PHPSESSID=j8lgfpgqf0n589u0ohr3vf8nqk; SERVERID=004");
            RestResponse response = await client.ExecuteAsync(request);
            HaremRoot haremRoot = JsonConvert.DeserializeObject<HaremRoot>(response.Content);
            return haremRoot;
        }

        public static async Task<Data> GetAllNadir()
        {
            Data data = new Data();

            HtmlNodeCollection NadirHtmlParse = HtmlParserHelper.GetNodesToResult("https://www.nadirdoviz.com/fiyatekrani/", ".//tr[@class='trsatir']");

            string[] altin = NadirHtmlParse[2].InnerText.Replace("Altın/TL", "").Trim().Split("\n");

            string[] gumus = NadirHtmlParse[5].InnerText.Replace("Gümüş/TL", "").Trim().Split("\n");

            KULCEALTIN GramAltin = new KULCEALTIN();

            GramAltin.satis = Double.Parse(altin[2]);
            GramAltin.alis = Double.Parse(altin[0]);

            data.KULCEALTIN = GramAltin;

            GUMUSTRY GramGumus = new GUMUSTRY();
            GramGumus.alis = Double.Parse(gumus[0]);
            GramGumus.satis = Double.Parse(gumus[2]);

            data.GUMUSTRY = GramGumus;

            return data;
        }

        public static double Guncel_Kur(string Ceviren, string Cevrilen)
        {
            string htmlcode = HtmlParserHelper.GetHtmlCode(string.Format("https://api.canlidoviz.com/items/current?marketId=0&code={0}&code={1}", Ceviren, Cevrilen));

            if (String.IsNullOrEmpty(htmlcode)) return 0;

            var myobjList = JsonConvert.DeserializeObject<List<Root>>(htmlcode);
            var myObj = myobjList[1];

            string lastbyprice = myObj.data.lastBuyPrice.ToString();
            double lastsellprice = myObj.data.lastSellPrice;
            string lastUpdateprice = myObj.data.lastUpdateDate.ToString();

            return lastsellprice;
        }
    }
}