using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using server.Models;
using server.Services;
using server.Functions;
using server.Utils;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Data;
namespace server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        ControllerServices services = new ControllerServices();
        AssetFunctions afunctions=new AssetFunctions();
        Validation validation = new Validation();
        public ConferenceController(IConfiguration _config,IWebHostEnvironment webhost)
        {
            configuration = _config;
            webHostEnvironment = webhost;
        }

        [HttpPost("conferences/get")]
        public  JsonResult GetConferences()
        {
            try
            {
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetConferences(configuration)));
            }
            catch (Exception  ex)
            {

               return Response404(ex.Message);
            }
        }
        [HttpPost("packages/get")]
        public JsonResult GetConferencePackages()
        {
            try
            {
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetAllConferencePackages(configuration)));
            }
            catch (Exception ex)
            {

               return Response404(ex.Message);
            }
        }
        [HttpPost("conference/add")]
        public JsonResult AddConference(ConferenceModel model)
        {
            try
            {
                model = services.AddConference(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetConferences(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("participants/get")]
        public JsonResult GetParticipants()
        {
            try
            {
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetAllParticipants(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }
        [HttpPost("participant/add")]
        public async Task<JsonResult> AddParticipant([FromForm] ParticipantModel model)
        {
            try
            {
                var file = Request.Form["file"];
                model.name = Request.Form["name"].ToString();
                model.phone = Request.Form["phone"].ToString();
                model.gender = Request.Form["gender"].ToString();
                model.email = Request.Form["email"].ToString().Trim();
                model.position = Request.Form["position"].ToString().TrimEnd();
                model.disability = Request.Form["disability"].ToString();
                model.disabled =int.Parse(Request.Form["disabled"].ToString());
                model.conference_id = Request.Form["conference_id"].ToString();
                model.diet = Request.Form["diet"].ToString().TrimEnd();
                model.location = Request.Form["location"].ToString();
                model.organization = Request.Form["organization"].ToString().TrimEnd();
                model.accomodation = int.Parse(Request.Form["accomodation"].ToString());
                model.package_id = Request.Form["package_id"].ToString();
                model.file =Request.Form.Files.Count>0? Request.Form.Files[0]:null;
                validation.ValidateParticipant(model);
                if (Request.Form.Files.Count>0)
                {
                    model.picture = afunctions.HandleUploadedFile(model, webHostEnvironment);
                }
                else
                {
                    model.picture = "";
                }
                model = services.AddMember(model, configuration);
                ConferenceModel cm = new ConferenceModel();
                cm.id = model.conference_id;
                ConferenceModel cmodel = new ModelFunction().FormatConferenceModelInfo(services.GetConferenceById(cm, configuration));
                string msg = new MessageServices().FormatEndpointMessage(name: model.name, title: cmodel.title, sdate: cmodel.start_date, edate: cmodel.end_date, venue: cmodel.venue);
                await new MessageServices().SendSMS(phoneNumber: model.phone.TrimEnd(), msg);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetAllParticipants(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("participant/update")]
        public JsonResult UpdateInfo(ParticipantModel model)
        {
            try
            {

                model = services.UpdateInfo(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(model.message));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }
        [HttpPost("participant/login")]
        public JsonResult LoginMember(ParticipantModel model)
        {
            try
            {

                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.MemberLogin(model, configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }
        [HttpPost("member/add")]
        public async Task<JsonResult> AddMember([FromForm] ParticipantModel model)
        {
            try
            {
                var file = Request.Form["file"];
                model.name = Request.Form["name"].ToString();
                model.phone = Request.Form["phone"].ToString().TrimEnd();
                model.gender = Request.Form["gender"].ToString();
                model.email = Request.Form["email"].ToString().TrimEnd();
                model.position = Request.Form["position"].ToString();
                model.disability = Request.Form["disability"].ToString();
                model.disabled = int.Parse(Request.Form["disabled"].ToString());
                model.conference_id = Request.Form["conference_id"].ToString();
                model.diet = Request.Form["diet"].ToString();
                model.location = Request.Form["location"].ToString();
                model.organization = Request.Form["organization"].ToString();
                model.accomodation = int.Parse(Request.Form["accomodation"].ToString());
                model.file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                validation.ValidateParticipant(model);
                if (Request.Form.Files.Count>0)
                {
                    model.picture = afunctions.HandleUploadedFile(model, webHostEnvironment);
                }
                else
                {
                    model.picture = "";
                }
                model = services.RegisterMember(model, configuration);
                ConferenceModel cm = new ConferenceModel();
                cm.id = model.conference_id;
                ConferenceModel cmodel = new ModelFunction().FormatConferenceModelInfo(services.GetConferenceById(cm,configuration));
                string msg = new MessageServices().FormatEndpointMessage(name:model.name,title:cmodel.title,sdate:cmodel.start_date,edate:cmodel.end_date,venue:cmodel.venue);
                await new MessageServices().SendSMS(phoneNumber: model.phone.TrimEnd(), msg);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("member/remove")]
        public JsonResult AdminRemoveParticipant(MemberInfoModel model)
        {
            try
            {
                services.RemoveMember(model,configuration);
                Response.StatusCode = StatusCodes.Status200OK;
                DeleteMemberResponseModel rm = new DeleteMemberResponseModel();
                rm.message = "Participant Removed Successfully";
                rm.data = services.GetAllParticipants(configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(rm));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("user/add")]
        public JsonResult AddUser(UserModel model)
        {
            try
            {
                model = services.AddUser(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(model.message));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }
        
        [HttpPost("user/login")]
        public JsonResult LoginUser(UserModel model)
        {
            try
            {
                UserModel user = services.UserLogin(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(user));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }
        /// <summary>
        /// 404 HANDLER
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private JsonResult Response404(string error)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new JsonResult(error);
        }

        [HttpPost("guest/add")]
        public JsonResult AddMember([FromForm] GuestModel model)
        {
            try
            {
                var file = Request.Form["file"];
                model.name = Request.Form["name"].ToString();
                model.role = Request.Form["role"].ToString();
                model.portfolio = Request.Form["portfolio"].ToString();
                model.id = Request.Form["position"].ToString();
               
                if (model.file != null || model.file.Length > 0)
                {
                    model.picture = afunctions.HandleGuestFile(model, webHostEnvironment);
                }
                services.RegisterGuest(model, configuration);
                ConferenceModel cm = new ConferenceModel();
                
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetGuest(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("guest/get")]
        public JsonResult GetMember()
        {
            try
            {
              
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetGuest(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }

        [HttpPost("guest/delete")]
        public JsonResult DeleteMember(GuestModel model)
        {
            try
            {
                services.DeleteGuest(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetGuest(configuration)));
            }
            catch (Exception ex)
            {

                return Response404(ex.Message);
            }
        }


    }
}
