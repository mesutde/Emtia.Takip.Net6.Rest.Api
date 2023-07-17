namespace Emtia.Takip.Net6.Rest.Api.Model
{
    public class KapaliCarsiParaBirimleri
    {
        public class ParaBirimi
        {
            public string Para_Birimi { get; set; }
            public double GuncelKurTL { get; set; }
            public double AlisFiyati { get; set; }
            public double SatisFiyati { get; set; }
        }

        public List<HaremDoviz> haremDoviz { get; set; }
        public List<NadirDoviz> nadirDoviz { get; set; }

        public class HaremDoviz : ParaBirimi
        {
        }

        public class NadirDoviz : ParaBirimi
        {
        }
    }
}