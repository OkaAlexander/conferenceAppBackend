using server.Models;
using System;
using Microsoft.Extensions.Configuration;
using server.Config;
using System.Data;
using server.Utils;
using server.Functions;
using Microsoft.Data.SqlClient;
using server.Constants;
namespace server.Services
{
    public class ControllerServices
    {
        PkFunctions functions = new PkFunctions();
        Validation validation = new Validation();
        AuthFunctions authentication = new AuthFunctions();
        ServerConnection router = new ServerConnection();
        ModelFunction models = new ModelFunction();

        /// <summary>
        /// ADD PARTICIPANT
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public ParticipantModel AddMember(ParticipantModel model, IConfiguration config)
        {
            try
            {
                validation.ValidateParticipant(model);
                model.id = functions.GenerateParticipantId();
                while (GetMemberByID(model, config).Rows.Count > 0)
                {
                    model.id = functions.GenerateParticipantId();
                }
                if(GetMemberByEmailAndConference(model,config).Rows.Count > 0)
                {
                    throw new Exception("You have already registered for this conference.For more info contact 000 000 0000. Thank You");
                }

                router.cmd = new SqlCommand(Commands.AddParticipant, router.Connection(config));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = model.phone;
                router.cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.email;
                router.cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = model.gender;
                router.cmd.Parameters.Add("@dis", SqlDbType.Int).Value = model.disabled;
                router.cmd.Parameters.Add("@disa", SqlDbType.VarChar).Value = model.disability;
                router.cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = model.location;
                router.cmd.Parameters.Add("@diet", SqlDbType.VarChar).Value = model.diet;
                router.cmd.Parameters.Add("@pos", SqlDbType.VarChar).Value = model.position;
                router.cmd.Parameters.Add("@pic", SqlDbType.VarChar).Value = model.picture;
                router.cmd.Parameters.Add("@org", SqlDbType.VarChar).Value = model.organization;
                router.cmd.Parameters.Add("@cid", SqlDbType.VarChar).Value = model.conference_id;
                router.cmd.Parameters.Add("@acm", SqlDbType.Int).Value = model.accomodation;

                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
                model.success = true;
                model.message = "Participant Added";
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ParticipantModel RegisterMember(ParticipantModel model, IConfiguration config)
        {
            try
            {
                validation.ValidateParticipant(model);
                model.id = functions.GenerateParticipantId();
                while (GetMemberByID(model, config).Rows.Count > 0)
                {
                    model.id = functions.GenerateParticipantId();
                }
                if (GetMemberByEmailAndConference(model, config).Rows.Count > 0)
                {
                    throw new Exception("You have already registered for this conference.For more info contact 000 000 0000. Thank You");
                }

                router.cmd = new SqlCommand(Commands.AddParticipant, router.Connection(config));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = model.phone;
                router.cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.email;
                router.cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = model.gender;
                router.cmd.Parameters.Add("@dis", SqlDbType.Int).Value = model.disabled;
                router.cmd.Parameters.Add("@disa", SqlDbType.VarChar).Value = model.disability;
                router.cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = model.location;
                router.cmd.Parameters.Add("@diet", SqlDbType.VarChar).Value = model.diet;
                router.cmd.Parameters.Add("@pos", SqlDbType.VarChar).Value = model.position;
                router.cmd.Parameters.Add("@pic", SqlDbType.VarChar).Value = model.picture;
                router.cmd.Parameters.Add("@org", SqlDbType.VarChar).Value = model.organization;
                router.cmd.Parameters.Add("@cid", SqlDbType.VarChar).Value = model.conference_id;
                router.cmd.Parameters.Add("@acm", SqlDbType.Int).Value = model.accomodation;

                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
                model.success = true;
                model.message = "Participant Added";
                return models.FormatMemberInfo(GetMemberByEmail(model,config));
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public const string UpdateParticipant = @"update dbo.conference_participants set name=@name,email=@mail,phone=@phone,location=@loc,gender=@gender,accomdation=@acm,postion=@pos,diet=@diet,institution=@org where id=@id";
        public ParticipantModel UpdateInfo(ParticipantModel model,IConfiguration config)
        {
            try
            {
                router.cmd = new SqlCommand(Commands.UpdateParticipant, router.Connection(config));
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = model.phone;
                router.cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = model.email;
                router.cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = model.location;
                router.cmd.Parameters.Add("@acm", SqlDbType.Int).Value = model.accomodation;
                router.cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = model.gender;
                router.cmd.Parameters.Add("@pos", SqlDbType.VarChar).Value = model.position;
                router.cmd.Parameters.Add("@diet", SqlDbType.VarChar).Value = model.diet;
                router.cmd.Parameters.Add("@org", SqlDbType.VarChar).Value = model.organization;
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;

                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();

                model.success = true;
                model.message = "Info Updated Successfull";
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ADD NEW CONFERENCE
        /// </summary>
        /// <param name="model"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public ConferenceModel AddConference(ConferenceModel model, IConfiguration configuration)
        {
            try
            {
                validation.ValidateConference(model);
                model.id = functions.GenerateConferenceId();
                while (GetConferenceById(model, configuration).Rows.Count > 0)
                {
                    model.id = functions.GenerateConferenceId();
                }

                model.status = 0;
                router.cmd = new SqlCommand(Commands.AddConference, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = model.title;
                router.cmd.Parameters.Add("@venue", SqlDbType.VarChar).Value = model.venue;
                router.cmd.Parameters.Add("@sdate", SqlDbType.VarChar).Value = model.start_date;
                router.cmd.Parameters.Add("@edate", SqlDbType.VarChar).Value = model.end_date;
                router.cmd.Parameters.Add("@des", SqlDbType.VarChar).Value = model.description;
                router.cmd.Parameters.Add("@status", SqlDbType.Int).Value = model.status;

                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
                model.success = true;
                model.message = "Conference Added";
                return model;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserModel AddUser(UserModel model,IConfiguration configuration)
        {
            try
            {
                validation.ValidateUser(model);
                model.id = functions.GenerateUserId();
                while (GetUserById(model, configuration).Rows.Count > 0)
                {
                    model.id = functions.GenerateUserId();
                }
                if (GetUserByUsername(model, configuration).Rows.Count > 0)
                {
                    authentication.GenerateStack("Username already exist");
                }
                model.status = 1;
                model.role = 1;
                model.password = authentication.HashPassword(model.password);
                router.cmd = new SqlCommand(Commands.RegisterUser, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = model.username;
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@role", SqlDbType.Int).Value = model.role;
                router.cmd.Parameters.Add("@status", SqlDbType.Int).Value = model.status;
                router.cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = model.password;

                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
                model.success = true;
                model.message = "User Added";
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public  UserModel UserLogin(UserModel model,IConfiguration configuration)
        {
            try
            {
                DataTable dt = GetUserByUsername(model, configuration);
                if (dt.Rows.Count > 0)
                {
                    UserModel user = authentication.FormatInfo(dt);
                    if (authentication.verifyPassword(model.password, user.password))
                    {
                        user.password = "";
                        return user;
                    }
                    else
                    {
                        throw authentication.GenerateStack("Invalid Login Password");
                    }
                }
                else
                {
                    throw authentication.GenerateStack("invalid Username");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GET MEMBER BY ID
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DataTable GetMemberByID(ParticipantModel model, IConfiguration configuration)
        {
            try
            {
                router.da = new SqlDataAdapter();
                router.tb = new DataTable();
                router.cmd = new SqlCommand(Commands.GetParticipantById, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetMemberByEmail(ParticipantModel model, IConfiguration configuration)
        {
            try
            {
                router.da = new SqlDataAdapter();
                router.tb = new DataTable();
                router.cmd = new SqlCommand(Commands.GetParticipantByEmail, router.Connection(configuration));
                router.cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = model.email;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetMemberByEmailAndConference(ParticipantModel model, IConfiguration configuration)
        {
            try
            {
                router.da = new SqlDataAdapter();
                router.tb = new DataTable();
                router.cmd = new SqlCommand(Commands.CheckParticipant, router.Connection(configuration));
                router.cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = model.email;
                router.cmd.Parameters.Add("@cid", SqlDbType.VarChar).Value = model.conference_id;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GET CONFERENCE BY ID;
        /// </summary>
        /// <param name="model"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DataTable GetConferenceById(ConferenceModel model, IConfiguration configuration)
        {
            try
            {
                router.da = new SqlDataAdapter();
                router.tb = new DataTable();
                router.cmd = new SqlCommand(Commands.GetConferenceById, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetUserById(UserModel model,IConfiguration configuration)
        {
            try
            {
                router.tb = new DataTable();
                router.da = new SqlDataAdapter();
                router.cmd = new SqlCommand(Commands.GetUserById, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetUserByUsername(UserModel model, IConfiguration configuration)
        {
            try
            {
                router.tb = new DataTable();
                router.da = new SqlDataAdapter();
                router.cmd = new SqlCommand(Commands.GetUserByUsername, router.Connection(configuration));
                router.cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = model.username;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetUserByUsers(UserModel model, IConfiguration configuration)
        {
            try
            {
                router.tb = new DataTable();
                router.da = new SqlDataAdapter();
                router.cmd = new SqlCommand(Commands.GetAllUsers, router.Connection(configuration));
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataTable GetConferences(IConfiguration configuration)
        {
            try
            {
                router.da = new SqlDataAdapter();
                router.tb = new DataTable();
                router.cmd = new SqlCommand(Commands.GetAllConferences, router.Connection(configuration));
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// GET ALL PARTICIPANTS
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DataTable GetAllParticipants(IConfiguration configuration)
        {
            try
            {

                router.tb = new DataTable();
                router.da = new SqlDataAdapter();
                router.cmd = new SqlCommand(Commands.GetAllParticipants, router.Connection(configuration));
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();
                return router.tb;

            }
            catch (Exception)
            {

                throw;
            }
        }


        ////////////////
        ///
        public void RegisterGuest(GuestModel model,IConfiguration config)
        {
            try
            {
                model.id = functions.GenerateGuestId();
                while (GetGuestById(model, config).Rows.Count > 0)
                {
                    model.id = functions.GenerateGuestId();
                }

                router.cmd = new SqlCommand(Commands.AddGuest, router.Connection(config));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@role", SqlDbType.VarChar).Value = model.role;
                router.cmd.Parameters.Add("@port", SqlDbType.VarChar).Value = model.portfolio;
                router.cmd.Parameters.Add("@pic", SqlDbType.VarChar).Value = model.picture;
                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetGuestById(GuestModel model,IConfiguration configuration)
        {
            try
            {
                router.tb = new DataTable();
                router.da = new SqlDataAdapter();

                router.cmd = new SqlCommand(Commands.GetGuestById, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();

                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetGuest(IConfiguration configuration)
        {
            try
            {
                router.tb = new DataTable();
                router.da = new SqlDataAdapter();

                router.cmd = new SqlCommand(Commands.GetGuest, router.Connection(configuration));
                router.OpenConnection();
                router.da.SelectCommand = router.cmd;
                router.da.Fill(router.tb);
                router.CloseConnection();

                return router.tb;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteGuest(GuestModel model,IConfiguration configuration)
        {
            try
            {
                router.cmd = new SqlCommand(Commands.DeleteGuest, router.Connection(configuration));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.OpenConnection();
                router.cmd.ExecuteNonQuery();
                router.CloseConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ParticipantModel MemberLogin(ParticipantModel model,IConfiguration conf)
        {
            try
            {
                if (GetMemberByEmail(model, conf).Rows.Count <= 0)
                {
                    throw new Exception("Invalid Email Address");
                }
                else
                {
                    if(GetMemberByID(model,conf).Rows.Count <= 0)
                    {
                        throw new Exception("Invalid Participant ID");
                    }
                    else
                    {
                        return models.FormatMemberInfo(GetMemberByEmail(model,conf));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
