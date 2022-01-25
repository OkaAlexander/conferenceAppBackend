namespace server.Models
{
    public class ConferenceModel:ResponseModel
    {
        public string id { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string venue { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public string time { get; set; }
    }
}
