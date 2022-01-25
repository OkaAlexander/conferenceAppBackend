namespace server.Models
{
    public class ResponseModel
    {
        public string error { get; set; }
        public string message { get; set; }
        public bool valid { get; set; }
        public bool success { get; set; }
    }
}
