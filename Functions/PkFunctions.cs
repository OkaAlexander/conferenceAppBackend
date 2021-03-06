using System;
using server.Constants;
namespace server.Functions
{
    public class PkFunctions
    {

        
        public string GenerateConferenceId()
        {
        Random rand = new Random();
            return ConstantValues.CONFERENCE + rand.Next(100, 999);
        }

        public string GeneratePackageId()
        {
            Random random = new Random();
            return "PID" + random.Next(100, 99999);
        }

        public string GenerateUserId()
        {
            Random rand = new Random();
            return ConstantValues.USER + rand.Next(100, 999);
        }
        public string GenerateParticipantId()
        {
            Random random = new Random();
            return ConstantValues.PARTICIPANT + random.Next(100, 999);
        }

        public string GenerateGuestId()
        {
            Random rand = new Random();
            return ConstantValues.GUEST + rand.Next(100, 999).ToString();
        }
    }
}
