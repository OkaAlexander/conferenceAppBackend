namespace server.Models
{
    public class PackageModel
    {
        public string id { get; set; }
        public string conferenceId { get; set; }
        public string title { get; set; }
        public decimal registrationFee { get; set; }
        public decimal materialCost { get; set; }
        public decimal costOfFeeding { get; set; }
        public decimal costOfAccomodation { get; set; }
        public int conferenceDuration { get; set; }
    }
}
