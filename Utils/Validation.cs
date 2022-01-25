using System.Text.RegularExpressions;
using server.Models;
using System;
namespace server.Utils
{
    public class Validation
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public void ValidateParticipant(ParticipantModel model)
        {
            try
            {
                long valid = 0;
                if (!regex.IsMatch(model.email))
                {
                    throw GenerateStack(error: "Invalid Email Address");
                }

                if(model.phone==null ||!long.TryParse(model.phone,out valid) || model.phone.Length != 10)
                {
                    throw GenerateStack(error: "Invalid Phone Number");
                }
                if(model.name==null|| model.name.Length <= 0)
                {
                    throw GenerateStack(error: "Participant Name Required");

                }
                if(model.gender==null || model.gender.Length <= 0)
                {
                    throw GenerateStack(error: "Gender Required");
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private Exception GenerateStack(string error,string source="")
        {
            Exception ex = new Exception(error);
            ex.Source=source;
            return ex;
        }

        public void ValidateConference(ConferenceModel model)
        {
            if(model.venue==null|| model.venue.Length <= 0)
            {
                throw GenerateStack(error: "Please Enter Conference Venue",source:"Conference Details");
            }
            if(model.date==null||model.date.Length <= 0)
            {
                throw GenerateStack(error: "Please Enter Conference Date");
            }
            if (model.title == null || model.title.Length <= 0)
            {
                throw GenerateStack(error: "Please Enter Conference Title");
            }
            if (model.time == null || model.time.Length <= 0)
            {
                throw GenerateStack(error: "Please Enter Conference Time");
            }
        }

        public void ValidateUser(UserModel model)
        {
            if (model.username == null || model.username.Length <= 0)
            {
                throw GenerateStack(error: "Username Required");
            }
            if (model.name == null || model.name.Length <= 0)
            {
                throw GenerateStack(error: "Name Required");
            }
            if (model.password == null || model.password.Length <= 0)
            {
                throw GenerateStack(error: "Password Required");
            }
        }
    }
}
