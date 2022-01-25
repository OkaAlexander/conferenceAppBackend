namespace server.Models
{
    public class UserModel:ResponseModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public int role { get; set; }
        public int status { get; set; }
        public string password { get; set; }
    }
}
