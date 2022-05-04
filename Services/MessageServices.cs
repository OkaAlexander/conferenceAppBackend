using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace server.Services
{
    public class MessageServices
    {
        public async Task SendSMS(string phoneNumber, string message)
        {
            try
            {

                var contact = new string(phoneNumber.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                var request = new { sender = "UENR", message = message, recipients = new string[] { contact } };
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var endPoint = $"https://sms.arkesel.com/api/v2/sms/send";
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("api-key", "YXNqUHJVV2tqYXZDUmRrYmhkeW8");
                var response = await client.PostAsync(endPoint, data);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public string FormatEndpointMessage(string name,string title,string sdate,string edate,string venue)
        {


            string msg = string.Format(@"Hi {0}, Your Registration for {1} Conference is Successfull, Conference starts on {2} to {3}, Venue: {4}. For more info contact: 0244854109 OR 0543311515. Thank You :)", name,title,sdate,edate,venue);
            return msg;
        }
    }
}

