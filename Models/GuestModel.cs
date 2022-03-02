using Microsoft.AspNetCore.Http;

namespace server.Models
{
    public class GuestModel:ResponseModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string portfolio { get; set; }
        public string picture { get; set; }
        public IFormFile file { get; set; }
    }
}
