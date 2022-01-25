using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using server.Models;
using server.Services;
using server.Functions;
using server.Utils;
using Microsoft.AspNetCore.Hosting;

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
        [HttpPost("conference/add")]
        public JsonResult AddConference(ConferenceModel model)
        {
            try
            {
                model = services.AddConference(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(model.message));
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
        public JsonResult AddParticipant([FromForm] ParticipantModel model)
        {
            try
            {
                var file = Request.Form["file"];
                model.name = Request.Form["name"].ToString();
                model.phone = Request.Form["phone"].ToString();
                model.gender = Request.Form["gender"].ToString();
                model.email = Request.Form["email"].ToString();
                model.position = Request.Form["position"].ToString();
                model.hotel = Request.Form["hotel"].ToString();
                model.room = Request.Form["room"].ToString();
                model.conference_id = Request.Form["conference_id"].ToString();
                model.special_need = Request.Form["special_need"].ToString();
                model.remark = Request.Form["remark"].ToString();
                model.organization = Request.Form["organization"].ToString();
                model.file = Request.Form.Files[0];
                validation.ValidateParticipant(model);
                if (model.file != null ||model.file.Length>0)
                {
                    model.picture = afunctions.HandleUploadedFile(model, webHostEnvironment);
                }
                model = services.AddMember(model, configuration);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(services.GetAllParticipants(configuration)));
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

    }
}
