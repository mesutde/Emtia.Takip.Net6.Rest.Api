namespace Emtia.Takip.Net6.Rest.Api.Model
{
    public class KapalicarsiMarka
    {
        public double guncelAltin { get; set; }
        public double guncelDolarKuru { get; set; }
        public double guncelEuroKuru { get; set; }

        public HaremAltin haremaltin { get; set; }
        public NadirGold nadirgold { get; set; }

        public NadirGumus nadirgumus { get; set; }

        public class Altin
        {
            public double GuncelFiyat { get; set; }
            public double GuncelFiyatFarki { get; set; }
            public double Alis24Ayar { get; set; }
            public double Satis24Ayar { get; set; }
            public Gram Gram { get; set; }
        }

        public class Gram
        {
            public double _5gr { get; set; }
            public double _10gr { get; set; }

            public double _15gr { get; set; }
            public double _20gr { get; set; }

            public double _50gr { get; set; }

            public double _100gr { get; set; }
        }

        public class HaremAltin : Altin
        {
        }

        public class NadirGold : Altin
        {
        }

        public class NadirGumus
        {
            public double Alis { get; set; }
            public double Satis { get; set; }
            public Gram Gram { get; set; }
        }
    }
}