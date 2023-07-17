using System.Net;
using HtmlAgilityPack;
using System.Globalization;

namespace Emtia.Takip.Net6.Rest.Api.Helper
{
    public static class HtmlParserHelper
    {
        public static string GetHtmlCode(string url)
        {
            try
            {
                string requestUrl = string.Format(url.Trim());

                using (var client = new WebClient())
                {
                    string shrunk = client.DownloadString(requestUrl);

                    return shrunk != "error" ? shrunk : string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string getHtmlSourceAgility(string link)
        {
            HtmlWeb webSite = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = webSite.Load(link);
            return doc.DocumentNode.OuterHtml;
        }

        public static HtmlNodeCollection GetNodesToResult(string Url, string Nodes)
        {
            //.SelectNodes(@"div[@id='frmPnlProductGallery']/ul/li/a")
            //.SelectNodes("//div[@id='myID']")
            //.SelectNodes("//table[3]");
            //SelectNodes(".//span[@class='nobr']");
            //"//div[@class='link_row']//a[@href]");   var Link = getiframe[0].Attributes["href"].Value;

            string htmlCode = GetHtmlCode(Url);

            if (htmlCode == "")
                htmlCode = getHtmlSourceAgility(Url);

            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(htmlCode);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes(Nodes);
            return basliklar;
        }

        public static string FindStringBetween(string str, string from, string to)
        {
            int index = str.IndexOf(from);
            if (index == -1)
            {
                return null;
            }
            int num2 = str.IndexOf(to, (int)(index + from.Length));
            return ((num2 == -1) ? str.Substring(index + from.Length) : str.Substring(index + from.Length, (num2 - from.Length) - index));
        }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            //Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        //  private string _docHtml = string.Empty;
    }
}