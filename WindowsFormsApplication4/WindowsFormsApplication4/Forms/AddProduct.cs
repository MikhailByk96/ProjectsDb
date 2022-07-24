using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication4
{
    public partial class AddProduct : Form
    {
        List<PersonsModel> persons = new List<PersonsModel>();
        public int GetIdByFio(List<PersonsModel> list, string s)
        {
            int key = 0;
            foreach (var item in list)
            {
                if (item.FIO.Equals(s))
                {
                    key = item.Id;
                }
            }
            return key;
        }
        public AddProduct()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            string sql = "SELECT resp.Id, CONCAT(resp.Surname, ' ', resp.Name, ' ', resp.LastName ) as FIO FROM responsiblepersons resp";
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                var accessories = conn.Query<PersonsModel>(sql);
                foreach (var item in accessories)
                {
                    comboBox1.Items.Add(item.FIO);
                    persons.Add(item);
                }

            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt1.Text) || string.IsNullOrEmpty(txt2.Text) || string.IsNullOrEmpty(txt3.Text) || string.IsNullOrEmpty(txt4.Text) || string.IsNullOrEmpty(txt5.Text) || string.IsNullOrEmpty(txt6.Text) || string.IsNullOrEmpty(txt7.Text) || string.IsNullOrEmpty(txt8.Text) || string.IsNullOrEmpty(txt9.Text))
            {
                MessageBox.Show("Имеются пустые поля");
                return;
            }
            else
            {
                StringBuilder sql = new StringBuilder("INSERT INTO product(NameProduct,ModelProduct,SerialNumberProduct,TechCharProduct,PriceProduct,GuaranteePeriod,GuaranteeOrganization,WorkGroup,Corpus,IdRespPerson,OtherInfo) VALUES (");
                sql.Append("'" + txt1.Text + "', ");
                sql.Append("'" + txt2.Text + "', ");
                sql.Append("'" + txt3.Text + "', ");
                sql.Append("'" + txt4.Text + "', ");
                sql.Append("" + Convert.ToInt32(txt5.Text) + ", ");
                sql.Append("'" + dateTimePicker2.Value.ToShortDateString() + "', ");
                sql.Append("'" + txt6.Text + "', ");
                sql.Append("'" + txt7.Text + "', ");
                sql.Append("'" + txt8.Text + "', ");
                sql.Append("'" + GetIdByFio(persons, comboBox1.SelectedItem.ToString()) + "', ");
                sql.Append("'" + txt9.Text + "')");
                using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    int k = conn.Execute(sql.ToString());
                    if (k > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены");
                    }
                }
            }
        }
    }
}
