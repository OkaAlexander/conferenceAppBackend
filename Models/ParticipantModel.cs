
using Microsoft.AspNetCore.Http;

namespace server.Models
{
    public class ParticipantModel:ResponseModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string hotel { get; set; }
        public string room { get; set; }
        public string special_need { get; set; }
        public string gender { get; set; }
        public string conference_id { get; set; }
        public string position { get; set; }
        public string organization { get; set; }
        public string picture { get; set; }
        public string remark { get; set; }
        public IFormFile file { get; set; }
    }
}
