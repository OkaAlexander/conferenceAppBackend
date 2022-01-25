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

                router.cmd = new SqlCommand(Commands.AddParticipant, router.Connection(config));
                router.cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                router.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.name;
                router.cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = model.phone;
                router.cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.email;
                router.cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = model.gender;
                router.cmd.Parameters.Add("@hot", SqlDbType.VarChar).Value = model.hotel;
                router.cmd.Parameters.Add("@rm", SqlDbType.VarChar).Value = model.room;
                router.cmd.Parameters.Add("@remark", SqlDbType.VarChar).Value = model.remark;
                router.cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = model.special_need;
                router.cmd.Parameters.Add("@pos", SqlDbType.VarChar).Value = model.position;
                router.cmd.Parameters.Add("@pic", SqlDbType.VarChar).Value = model.picture;
                router.cmd.Parameters.Add("@org", SqlDbType.VarChar).Value = model.organization;
                router.cmd.Parameters.Add("@cid", SqlDbType.VarChar).Value = model.conference_id;

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
                router.cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = model.date;
                router.cmd.Parameters.Add("@des", SqlDbType.VarChar).Value = model.description;
                router.cmd.Parameters.Add("@status", SqlDbType.Int).Value = model.status;
                router.cmd.Parameters.Add("@time", SqlDbType.VarChar).Value = model.time;

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

    }
}
