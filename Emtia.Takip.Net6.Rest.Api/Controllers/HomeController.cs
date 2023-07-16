using Microsoft.AspNetCore.Mvc;
using Emtia.Takip.Net6.Rest.Api.Model.ResultModel;
using static Emtia.Takip.Net6.Rest.Api.Model.KapalicarsiMarka;

namespace Emtia.Takip.Net6.Rest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [Route("GetEmtiaList")]
        [HttpGet]
        public async Task<Response<Model.KapalicarsiMarka>> GetAllCountries()
        {
            Response<Model.KapalicarsiMarka> retVal = new Response<Model.KapalicarsiMarka>();

            Model.KapalicarsiMarka emtia = new Model.KapalicarsiMarka();

            var GetAllCountries = Helper.ExchangeHelperApi.GetAllHaremExchange();
            var GuncelAltinKuru = Helper.ExchangeHelperApi.Guncel_Kur("TRY", "GA");
            var GuncelDolarKuru = Helper.ExchangeHelperApi.Guncel_Kur("TRY", "USD");
            var GuncelEuroKuru = Helper.ExchangeHelperApi.Guncel_Kur("TRY", "EUR");
            var GuncelNadirSatis = Helper.ExchangeHelperApi.GetAllNadir();

            if (GetAllCountries.Result.data == null || GuncelNadirSatis.Result == null)
            {
                retVal.Result = false;
                retVal.ResultCode = -1;
                retVal.Message = "İşlem Başarısuz..Veri alınamadı";
                retVal.Comment = " ";
                retVal.Data = null;
                retVal.UpdateTime = DateTime.Now.ToString();

                return retVal;
            }

            emtia.guncelAltin = GuncelAltinKuru;
            emtia.guncelDolarKuru = GuncelDolarKuru;
            emtia.guncelEuroKuru = GuncelEuroKuru;

            Model.KapalicarsiMarka.HaremAltin HAlltin = new Model.KapalicarsiMarka.HaremAltin();
            HAlltin.GuncelFiyat = GuncelAltinKuru;
            HAlltin.Alis24Ayar = GetAllCountries.Result.data.KULCEALTIN.alis;
            HAlltin.Satis24Ayar = GetAllCountries.Result.data.KULCEALTIN.satis;
            HAlltin.GuncelFiyatFarki = HAlltin.Satis24Ayar - GuncelAltinKuru;

            Gram Haremgram = new Gram();
            Haremgram._5gr = HAlltin.Satis24Ayar * 5;
            Haremgram._10gr = HAlltin.Satis24Ayar * 10;
            Haremgram._15gr = HAlltin.Satis24Ayar * 15;
            Haremgram._20gr = HAlltin.Satis24Ayar * 20;
            Haremgram._50gr = HAlltin.Satis24Ayar * 50;
            Haremgram._100gr = HAlltin.Satis24Ayar * 100;

            HAlltin.Gram = Haremgram;

            Model.KapalicarsiMarka.NadirGold NAltin = new Model.KapalicarsiMarka.NadirGold();
            NAltin.GuncelFiyat = GuncelAltinKuru;
            NAltin.Alis24Ayar = GuncelNadirSatis.Result.KULCEALTIN.alis;
            NAltin.Satis24Ayar = GuncelNadirSatis.Result.KULCEALTIN.satis;
            NAltin.GuncelFiyatFarki = NAltin.Satis24Ayar - GuncelAltinKuru;

            Gram nadirGram = new Gram();

            nadirGram._5gr = NAltin.Satis24Ayar * 5;
            nadirGram._10gr = NAltin.Satis24Ayar * 10;
            nadirGram._15gr = NAltin.Satis24Ayar * 15;
            nadirGram._20gr = NAltin.Satis24Ayar * 20;
            nadirGram._50gr = NAltin.Satis24Ayar * 50;
            nadirGram._100gr = NAltin.Satis24Ayar * 100;

            NAltin.Gram = nadirGram;

            NadirGumus nadirGumus = new NadirGumus();
            nadirGumus.Alis = GuncelNadirSatis.Result.GUMUSTRY.alis;
            nadirGumus.Satis = GuncelNadirSatis.Result.GUMUSTRY.satis;

            Gram nadirGumusGram = new Gram();

            nadirGumusGram._5gr = nadirGumus.Satis * 5;
            nadirGumusGram._10gr = nadirGumus.Satis * 10;
            nadirGumusGram._15gr = nadirGumus.Satis * 15;
            nadirGumusGram._20gr = nadirGumus.Satis * 20;
            nadirGumusGram._50gr = nadirGumus.Satis * 50;
            nadirGumusGram._100gr = nadirGumus.Satis * 100;
            nadirGumus.Gram = nadirGumusGram;

            emtia.haremaltin = HAlltin;
            emtia.nadirgold = NAltin;
            emtia.nadirgumus = nadirGumus;

            retVal.Result = true;
            retVal.ResultCode = 200;
            retVal.Message = "İşlem Başarılı";
            retVal.Comment = " ";
            retVal.Data = emtia;
            retVal.UpdateTime = DateTime.Now.ToString();

            return retVal;
        }
    }
}