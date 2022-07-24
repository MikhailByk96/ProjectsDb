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
    public partial class AddAccesories : Form
    {
        List<PersonsModel> persons = new List<PersonsModel>();
        public int GetIdByFio(List<PersonsModel> list, string s) 
        {
            int key = 0;
            foreach (var item in list)
            { 
                if(item.FIO.Equals(s))
                {
                    key = item.Id;
                }
            }
            return key;
        }
        public AddAccesories()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtOrg.Text) || string.IsNullOrEmpty(txtSeries.Text))
            {
                MessageBox.Show("Имеются пустые поля");
                return;
            }
            else
            {
                DateTime tm = dateTimePicker1.Value;

                StringBuilder sql = new StringBuilder("INSERT INTO accessories(DatePost,Name,Model,SeriesNumber,TechCharacteristics,Price,GuaranteePeriod,GuaranteeOrganization,BasicProduct,IdRespPerson,OtherInfos) VALUES (");
                sql.Append("'"+tm.ToString("dd.MM.yyyy")+"', ");
                sql.Append("'"+txtName.Text+"', ");
                sql.Append("'"+txtModel.Text+"', ");
                sql.Append("'"+txtSeries.Text+"', ");
                sql.Append("'"+txtTCh.Text+"', ");
                sql.Append(""+Convert.ToInt32(textBox1.Text)+",");
                sql.Append("'" + dateTimePicker2.Value.ToString("dd.MM.yyyy") + "', ");
                sql.Append("'"+txtOrg.Text+"', ");
                sql.Append("'"+textBox3.Text+"', ");
                sql.Append("'" + GetIdByFio(persons,comboBox1.SelectedItem.ToString()) + "', ");
                sql.Append("'" + textBox2.Text + "')");
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

        private void AddAccesories_Load(object sender, EventArgs e)
        {
            string sql = "SELECT resp.Id, CONCAT(resp.Surname, ' ', resp.Name, ' ', resp.LastName ) as FIO from responsiblepersons resp";
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
    }
}
