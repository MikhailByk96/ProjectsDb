using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Configuration;

namespace WindowsFormsApplication4
{
    public static class DbCRUD
    {
        public static bool IS_ADMIN = false;
        public static DataGridView GetAllAccessories()
        {
            DataGridView dw = new DataGridView();
            string sql = "SELECT ac.Id, SUBSTRING(ac.DatePost, 1, 10) AS DatePost, ac.Name,ac.Model,ac.SeriesNumber,ac.TechCharacteristics,ac.Price,SUBSTRING(ac.GuaranteePeriod, 1, 10) AS GuaranteePeriod,ac.GuaranteeOrganization,ac.BasicProduct,CONCAT(resp.Surname, ' ', resp.Name, ' ', resp.LastName ) AS FIORespPerson ,ac.OtherInfos FROM accessories ac INNER JOIN responsiblepersons resp on (resp.Id = ac.IdRespPerson)";
            
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                var accessories = conn.Query<AcessoriesModel>(sql);
                dw.DataSource = accessories;
            }

            return dw;
        }

        public static DataGridView GetAllProducts()
        {
            DataGridView dw = new DataGridView();
            string sql = "SELECT ac.Id, ac.NameProduct, ac.ModelProduct,ac.SerialNumberProduct,ac.TechCharProduct,ac.PriceProduct,SUBSTRING(ac.GuaranteePeriod,1,10) AS GuaranteePeriod  ,ac.GuaranteeOrganization,ac.WorkGroup,ac.Corpus,CONCAT(resp.Surname, ' ', resp.Name, ' ', resp.LastName ) AS FIORespPerson ,ac.OtherInfo FROM product ac INNER JOIN responsiblepersons resp on (resp.Id = ac.IdRespPerson)";
            
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                var accessories = conn.Query<ProductModel>(sql);
                dw.DataSource = accessories;
            }
            return dw;
        }

        public static DataGridView GetAllUsers() 
        {
            DataGridView dw = new DataGridView();
            dw.Enabled = false;
            string sql = "SELECT Login, Password, IsAdmin from admin";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                var admins = conn.Query<Admin>(sql);
                dw.DataSource = admins;
            }
            return dw;
        }

        public static void DeleteFromTable(string table,int key)
        {
            string sql = $"DELETE FROM {table} where Id = {key}";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                conn.Execute(sql);
            }
        }
        public static void AddUser(string login, string password, int isAdmin) 
        {
            string sql = "INSERT INTO admin(Login, Password, IsAdmin) values ('"+login+"', '"+password+"', "+isAdmin+")";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                conn.Execute(sql);
            }
        }

        public static DataGridView GetAllPersons()
        {
            DataGridView dw = new DataGridView();
            dw.Enabled = false;
            string sql = "SELECT Id, Surname, Name, LastName,Department,Post,WorkPhone  from responsiblepersons";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                var persons = conn.Query<ResponsiblePersonsModel>(sql);
                dw.DataSource = persons;
            }
            return dw;
        }

        public static void AddPerson(string surname, string name,string lastname, int dep, int post, string worknumber)
        {
            string sql = "INSERT INTO responsiblepersons(Surname, Name, LastName,Department, Post, WorkPhone ) values ('" + surname + "', '" + name + "', '" + lastname + "', " + dep + ", "+post+", '"+worknumber+"')";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                conn.Execute(sql);
            }
        }
        
    }
}
