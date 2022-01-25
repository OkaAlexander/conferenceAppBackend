namespace server.Constants
{
    public class Commands
    {

        /// <summary>
        /// PARTICIPANTS COMMANDS;
        /// </summary>
        public const string AddParticipant = @"insert into dbo.conference_participants(id,name,phone,email," +
                                             "organization,position,hotel,room,special_need,remark,picture,gender,conference_id)" +
                                             " values(@id,@name,@phone,@email,@org,@pos,@hot,@rm,@sn,@remark,@pic,@gender,@cid)";
        //
        public const string GetParticipantById = "select * from dbo.conference_participants where id=@id";
        //
        public const string GetAllParticipants ="select * from conference_participants";
        /// <summary>
        /// CONFERENCES COMMANDS;
        /// </summary>
        public const string GetAllConferences = "select * from dbo.conferences";
        public const string GetConferenceById = "select * from dbo.conferences where id=@id";
        public const string AddConference = "insert into dbo.conferences(id,title,venue,description,date,status,time) values(@id,@title,@venue,@des,@date,@status,@time)";


        /// <summary>
        /// USER COMMANDS;
        /// </summary>
        public const string GetAllUsers = "select * from dbo.users";
        public const string GetUserById = "select * from dbo.users where id=@id";
        public const string GetUserByUsername = "select * from dbo.users where username=@username";
        public const string RegisterUser = "insert into dbo.users(id,role,status,username,name,password) values(@id,@role,@status,@username,@name,@pwd)";

    }
}
