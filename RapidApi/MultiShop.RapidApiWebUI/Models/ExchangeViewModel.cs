namespace MultiShop.RapidApiWebUI.Models
{
    public class ExchangeViewModel
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string request_id { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string from_symbol { get; set; }
            public string to_symbol { get; set; }
            public string type { get; set; }
            public float exchange_rate { get; set; }
            public float previous_close { get; set; }
            public string last_update_utc { get; set; }
            public Time_Series time_series { get; set; }
            public int utc_offset_sec { get; set; }
            public int interval_sec { get; set; }
            public string period { get; set; }
        }

        public class Time_Series
        {
            
        }
    }
}
