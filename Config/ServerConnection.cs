using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
namespace server.Config
{
    public class ServerConnection
    {
        private  SqlConnection con;
        public DataTable tb;
        public SqlDataAdapter da;
        public SqlCommand cmd;

        /// <summary>
        /// GET THE DATABASE CONNECTION INSTANCE
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public SqlConnection Connection(IConfiguration configuration)
        {
            try
            {
                string connectionstr = configuration.GetConnectionString("conference");
                con = new SqlConnection(connectionstr);
                return con;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return new SqlConnection("");
            }
        }

        /// <summary>
        /// OPEN THE CONNECTION INSTANCE
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                else
                {
                    con.Close();
                    con.Open();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// CLOSE THE CONNECTION INSTANCE
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                else
                {
                    con.Open();
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        
    }
}
