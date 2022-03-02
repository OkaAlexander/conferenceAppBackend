using server.Models;
using System.Data;
namespace server.Functions
{
    public class ModelFunction
    {

        public ConferenceModel FormatConferenceModelInfo(DataTable dt)
        {
            ConferenceModel model = new ConferenceModel();
            foreach (DataRow row in dt.Rows)
            {
                model.venue = row["venue"].ToString();
                model.title = row["title"].ToString();
                model.start_date = row["start_date"].ToString();
                model.end_date = row["end_date"].ToString();
                model.description = row["description"].ToString();
            }

            return model;
        }

        public GuestModel ForamtGuetModelInfo(DataTable dt)
        {
            try
            {
                GuestModel model = new GuestModel();
                foreach (DataRow row in dt.Rows)
                {
                    model.role = row["role"].ToString();
                    model.id = row["id"].ToString();
                    model.name = row["name"].ToString();
                    model.portfolio = row["porfolio"].ToString();
                    model.picture = row["picture"].ToString();
                }
                return model;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ParticipantModel FormatMemberInfo(DataTable tb)
        {
            try
            {
                ParticipantModel model = new ParticipantModel();
                foreach (DataRow row in tb.Rows)
                {
                    model.id = row["id"].ToString();
                    model.name = row["name"].ToString();
                    model.phone = row["phone"].ToString();
                    model.email = row["email"].ToString();
                    model.organization = row["organization"].ToString();
                    model.diet = row["diet"].ToString();
                    model.disability = row["disability"].ToString();
                    model.accomodation = int.Parse(row["accomodation"].ToString());
                    model.gender = row["gender"].ToString();
                    model.picture = row["picture"].ToString();
                    model.position = row["position"].ToString();
                    model.disabled =int.Parse( row["disabled"].ToString());
                    model.conference_id = row["conference_id"].ToString();
                    model.location = row["location"].ToString();
                }
                return model;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
