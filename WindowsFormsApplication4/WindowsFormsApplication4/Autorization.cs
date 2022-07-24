using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Имеются пустые поля");
                return;
            }
            else if (!Admin.IsUserExists(txtLogin.Text, txtPassword.Text))
            {
                MessageBox.Show("Такого пользователя не существует");
            }
            else {
                DbCRUD.IS_ADMIN = Admin.GetIsAdmin(txtLogin.Text, txtPassword.Text);
                new MainForm().Show();
            }
        }
    }
}
