using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace WindowsFormsApplication4
{
    public class Admin
    {
        
        public string Login { get; set; }
        public string Password { get; set; }
        public string IsAdmin { get; set; }

        public static bool IsUserExists(string login, string password) 
        {
            bool result = false;
            int count = 0;
            string sql = string.Format("SELECT COUNT(Id) FROM admin WHERE Login = '{0}' AND Password = '{1}'",login,password);
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString)) 
            {
                count = conn.QueryFirst<int>(sql);
            }
            result = count <= 0 ? false : true; 
            return result;
        }

        public static bool GetIsAdmin(string login, string pass) 
        {
            bool result = false;
            int count = 0;
            string sql = string.Format("SELECT IsAdmin FROM admin WHERE Login = '{0}' AND Password = '{1}'", login, pass);
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                count = conn.QueryFirst<int>(sql);
            }
            result = count == 0 ? false : true;
            return result;
        }
    }


}
