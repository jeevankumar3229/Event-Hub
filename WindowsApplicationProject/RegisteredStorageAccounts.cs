using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsApplicationProject.StorageAccountRegistration;

namespace WindowsApplicationProject
{
    public partial class RegisteredStorageAccounts : Form
    {
        List<Storage_Account> storage;
       
        private readonly Form1 form1;

        public RegisteredStorageAccounts(Form1 form1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += RegisteredStorageAccount_FormClosing;
            this.form1 = form1;
        }

        private void RegisteredStorageAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void RegisteredStorageAccounts_Load(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\storageaccount.json";
                if (File.Exists(filepath))
                {
                    var text = File.ReadAllText(filepath);
                    if (text.Length > 0)
                    {
                        storage = JsonConvert.DeserializeObject<List<Storage_Account>>(text);
                        dataGridView1.DataSource = storage;
                        dataGridView1.Columns[0].Width = 355;
                        dataGridView1.Columns[1].Width = 200;
                        DataGridViewButtonColumn buttoncolumn = new DataGridViewButtonColumn();
                        buttoncolumn.Text = "Start";
                        buttoncolumn.HeaderText = "Action";
                        buttoncolumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(buttoncolumn);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.CellContentClick += datagridviewbuttonclick;
                    }


                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred", ex);
                MessageBox.Show("Error Occurred");
            }
        }

        private void datagridviewbuttonclick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex >= 0)
            {
                var account = storage[e.RowIndex];
                this.Hide();
                StorageAccountMainPage storageAccountMainPage = new StorageAccountMainPage(account,this);
                storageAccountMainPage.Show();
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
