namespace server.Models
{
    public class MemberInfoModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int disabled { get; set; }
        public string disability { get; set; }
        public string diet { get; set; }
        public string gender { get; set; }
        public string conference_id { get; set; }
        public string position { get; set; }
        public string organization { get; set; }
        public string picture { get; set; }
        public string location { get; set; }
        public int accomodation { get; set; }
    }
}
