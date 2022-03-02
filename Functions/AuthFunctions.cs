using server.Models;
using Scrypt;
using System.Data;
using System;
namespace server.Functions
{
    public class AuthFunctions
    {
        public string HashPassword(string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Encode(password); 
        }


        public bool verifyPassword(string password,string hpassword)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Compare(password, hpassword);
        }


        public UserModel FormatInfo(DataTable dt)
        {
            try
            {
                UserModel model = new UserModel();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        model.id = row["id"].ToString();
                        model.name = row["name"].ToString();
                        model.username = row["username"].ToString();
                        model.status = int.Parse(row["status"].ToString());
                        model.role = int.Parse(row["role"].ToString());
                        model.password = row["password"].ToString();
                    }
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Exception GenerateStack(string error)
        {
            Exception ex = new Exception(error);
            return ex;
        }
    }
}
