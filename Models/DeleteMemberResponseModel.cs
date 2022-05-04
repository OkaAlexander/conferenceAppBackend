using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft;
namespace server.Models
{
    public class DeleteMemberResponseModel
    {
        public string message { get; set; }
        public  Object data { get; set; }
    }
}
