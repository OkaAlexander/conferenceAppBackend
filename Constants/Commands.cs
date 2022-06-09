namespace server.Constants
{
    public class Commands
    {

        /// <summary>
        /// PARTICIPANTS COMMANDS;
        /// </summary>
        public const string AddParticipant = @"insert into dbo.conference_participants(id,name,phone,email," +
                                             "organization,position,disabled,disability,diet,location,picture,gender,conference_id,accomodation,package_id)" +
                                             " values(@id,@name,@phone,@email,@org,@pos,@dis,@disa,@diet,@loc,@pic,@gender,@cid,@acm,@pkid)";

        public const string RemoveParticipant = "delete from dbo.conference_participants where id=@id";
        
        public const string UpdateParticipant = "update dbo.conference_participants set name=@name,email=@mail,phone=@phone,location=@loc,gender=@gender,accomodation=@acm,position=@pos,diet=@diet,organization=@org where id=@id";
        //
        public const string GetParticipantById = "select * from dbo.conference_participants where id=@id";
        public const string GetParticipantByEmail = "select * from dbo.conference_participants where email=@mail";
        public const string CheckParticipant = "select * from dbo.conference_participants where (email=@mail and conference_id=@cid)";
        //
        public const string GetAllParticipants ="select * from conference_participants";
        /// <summary>
        /// CONFERENCES COMMANDS;
        /// </summary>
        public const string GetAllConferences = "select * from conferences";
        public const string GetallConferencePackages = "select * from conferencePackage";
        public const string GetConferenceById = "select * from dbo.conferences where id=@id";
        public const string AddConference = "insert into dbo.conferences(id,title,venue,description,start_date,end_date,status) values(@id,@title,@venue,@des,@sdate,@edate,@status)";


        /// <summary>
        /// USER COMMANDS;
        /// </summary>
        public const string GetAllUsers = "select * from dbo.users";
        public const string GetUserById = "select * from dbo.users where id=@id";
        public const string GetUserByUsername = "select * from dbo.users where username=@username";
        public const string RegisterUser = "insert into dbo.users(id,role,status,username,name,password) values(@id,@role,@status,@username,@name,@pwd)";


        public const string GetGuest = "select * from guest";
        public const string GetGuestById = "select * from guest where id=@id";
        public const string AddGuest = "insert into dbo.guest(id,name,role,portfolio,picture) values(@id,@name,@role,@port,@pic)";
        public const string DeleteGuest = "delete from dbo.guest where id=@id";
        public const string AddConferencePackage = "insert into dbo.conferencePackage(id,conferenceId,title,registrationFee,materialCost,costOfFeeding,costOfAccomodation,conferenceDuration) values(@id,@cid,@title,@rf,@mc,@cf,@ca,@cd)";
    }
}
