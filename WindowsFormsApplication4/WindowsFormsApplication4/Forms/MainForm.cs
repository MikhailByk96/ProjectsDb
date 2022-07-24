using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace WindowsFormsApplication4
{
    public partial class MainForm : Form
    {
        List<int> accIdsOff = new List<int>();
        List<int> prodIdsOff = new List<int>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) {
                dataGridView1.DataSource = DbCRUD.GetAllAccessories().DataSource;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.DataSource = DbCRUD.GetAllProducts().DataSource;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddAccesories().Show();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) 
            {
                string table = string.Empty;
                int key = 0;
                if (comboBox1.SelectedIndex == 0)
                {
                    table = "accessories";
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    table = "product";
                }
                key = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                DbCRUD.DeleteFromTable(table, key);
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.DataSource = DbCRUD.GetAllAccessories().DataSource;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.DataSource = DbCRUD.GetAllProducts().DataSource;
                }
                dataGridView2.DataSource = DbCRUD.GetAllAccessories().DataSource;
                dataGridView3.DataSource = DbCRUD.GetAllProducts().DataSource;
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DbCRUD.IS_ADMIN == false) 
            {
                tabControl1.TabPages[4].Parent = null;
                tabControl1.TabPages[3].Parent = null;
                tabControl1.TabPages[2].Parent = null;
            }
            dataGridView2.DataSource = DbCRUD.GetAllAccessories().DataSource;
            dataGridView3.DataSource = DbCRUD.GetAllProducts().DataSource;
            dataGridView4.DataSource = DbCRUD.GetAllUsers().DataSource;
            dataGridView5.DataSource = DbCRUD.GetAllPersons().DataSource;
            dataGridView6.DataSource = DbCRUD.GetAllAccessories().DataSource;
            dataGridView7.DataSource = DbCRUD.GetAllProducts().DataSource;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Имеются пустые поля");
                return;
            }
            else 
            {
                int isAdmin = checkBox1.Checked ? 1 : 0;
                DbCRUD.AddUser(txtLogin.Text, txtPassword.Text, isAdmin);
                dataGridView4.DataSource = DbCRUD.GetAllUsers().DataSource;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Имеются пустые поля");
                return;
            }
            else 
            {
                DbCRUD.AddPerson(textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text), textBox6.Text);
                MessageBox.Show("Данные успешно добавлены");
                dataGridView5.DataSource = DbCRUD.GetAllPersons().DataSource;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AddProduct().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        if(dataGridView2.Rows[i].Cells[j].Value == null)
                        {
                            excelApp.Cells[i + 2, j + 1] = string.Empty;
                        }
                        else
                        {
                            excelApp.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        }
                        
                    }
                }
                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
            else 
            {
                MessageBox.Show("Пустая таблица.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dataGridView3.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dataGridView3.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {
                        if (dataGridView3.Rows[i].Cells[j].Value == null)
                        {
                            excelApp.Cells[i + 2, j + 1] = string.Empty;
                        }
                        else
                        {
                            excelApp.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
            else
            {
                MessageBox.Show("Пустая таблица.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            DocX doc = DocX.Load(path + "\\Doc.docx");
            doc.ReplaceText("[NumberAct]", textBox7.Text);
            doc.ReplaceText("[dateSpis]", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
            doc.ReplaceText("[ListAss]", string.IsNullOrEmpty(richTextBox1.Text) ? string.Empty : richTextBox1.Text);
            doc.ReplaceText("[ListProd]", string.IsNullOrEmpty(richTextBox2.Text) ? string.Empty : richTextBox2.Text);
            doc.ReplaceText("[ListOfJuries]", string.IsNullOrEmpty(richTextBox3.Text) ? string.Empty : richTextBox3.Text);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Документы Word|*.doc";
            saveFile.Title = "Сохранить документ";
            if (DialogResult.OK == saveFile.ShowDialog()) 
            {
                string docName = saveFile.FileName;
                doc.SaveAs(docName);
            }
            //;
            //MessageBox.Show(path);
        }

        private void dataGridView6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text += "Список списываемых комплектующих \n";
            for(int i = 0; i < dataGridView6.Columns.Count; i++)
            {
                if (dataGridView6.Rows[dataGridView6.CurrentCell.RowIndex].Cells[i].Value != null)
                {
                    richTextBox1.Text += dataGridView6.Rows[dataGridView6.CurrentCell.RowIndex].Cells[i].Value.ToString() + ",";
                }
            }
            accIdsOff.Add(Convert.ToInt32(dataGridView6.Rows[dataGridView6.CurrentCell.RowIndex].Cells[0].Value.ToString()));
            richTextBox1.Text += "\n";
             
        }

        private void dataGridView7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            richTextBox2.Text += "Список списываемых комплектующих \n";
            for (int i = 0; i < dataGridView7.Columns.Count; i++)
            {
                if (dataGridView7.Rows[dataGridView7.CurrentCell.RowIndex].Cells[i].Value != null)
                {
                    richTextBox2.Text += dataGridView7.Rows[dataGridView7.CurrentCell.RowIndex].Cells[i].Value.ToString() + ",";
                }
            }
            prodIdsOff.Add(Convert.ToInt32(dataGridView7.Rows[dataGridView7.CurrentCell.RowIndex].Cells[0].Value.ToString()));
            richTextBox2.Text += "\n";
        }

        private void dataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string table = "responsiblepersons";
                int key = Convert.ToInt32(dataGridView5[0, dataGridView5.CurrentRow.Index].Value.ToString());
                DbCRUD.DeleteFromTable(table, key);
                dataGridView5.DataSource = DbCRUD.GetAllPersons().DataSource;
            }
        }
    }
}
