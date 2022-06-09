using System.Collections.Generic;

namespace server.Models
{
    public class ConferenceModel:ResponseModel
    {
        public string id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string title { get; set; }
        public string venue { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public List<PackageModel> package { get; set; } = new List<PackageModel>();
    }
}
