using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoR.Entity;
using AoR.Entity.Interface;

namespace AoR.DAL
{
    public class UserRepository
    {
        public static object Create(IUser user)
        {
            var sqlConnStr = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var conn = new SqlConnection(sqlConnStr.ConnectionString);
            var command = new SqlCommand("UserCreate", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nick", user.Nick);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);

            object result = null;
            try
            {
                conn.Open();
                result = command.ExecuteScalar();
                conn.Close();
            }
            catch(Exception ex)
            {
            }
            return result;
        }

        public static int ReturnNumbeOnlineUsers(int minutes)
        {
            var sqlConnStr = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var conn = new SqlConnection(sqlConnStr.ConnectionString);
            var command = new SqlCommand("NumberUsersOnlineReturn", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@minutes", minutes);
 
            object result = null;
            try
            {
                conn.Open();
                result = command.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
            return (int)result;
        }
    }
}
