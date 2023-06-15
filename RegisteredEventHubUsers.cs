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

namespace WindowsApplicationProject
{
    public partial class RegisteredEventHubUsers : Form
    {
        public static List<User> user;
        public RegisteredEventHubUsers()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Form_closing1;
        }

        private void Form_closing1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void RegisteredEventHubUsers_Load(object sender, EventArgs e)
        {
            string filepath1 = @"C:\Users\286968\OneDrive - Resideo\Desktop\publisherandlistener.json";

            if (File.Exists(filepath1))
            {
                string data = File.ReadAllText(filepath1);

                user = JsonConvert.DeserializeObject<List<User>>(data);

                dataGridView1.DataSource = user;

                DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                button.HeaderText = "Action";

                button.Text = "Start";

                button.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(button);

                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[1].Width = 135;

                dataGridView1.CellContentClick += dataGridView1_CellContentClick;




            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex >= 0)
            {
                DataGridViewButtonCell buttoncell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                User u = user[e.RowIndex];

                if (buttoncell.Value.ToString() == "Start" && u.Type.ToString() == "Publisher")
                {
                    this.Hide();
                    LoggerConfig._LogInformation("Opening Event Sending Form");
                    EventSendingForm events = new EventSendingForm(buttoncell, u);
                    events.Show();

                }

                else if (buttoncell.Value.ToString() == "Start" && u.Type.ToString() == "Listener")
                {
                    this.Hide();
                    LoggerConfig._LogInformation("Opening a Event Receiving Form");
                    EventReceivingForm form = new EventReceivingForm(buttoncell, u);
                    form.Show();

                }

            }

        }
        public class User
        {
            public string Name { get; set; }
            public string EventHubNamespace { get; set; }
            public string EventHubName { get; set; }
            public string ConsumerGroup { get; set; }
            public string StorageAccount { get; set; }
            public string Container { get; set; }
            public string Type { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
