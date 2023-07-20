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

        public static List<User> users;
        private readonly Form1 form1;

        public RegisteredEventHubUsers(Form1 form1)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Form_closing1;
            this.form1 = form1;
        }

        private void Form_closing1(object sender, FormClosingEventArgs e)
        {

            form1.Show();
        }

        private void RegisteredEventHubUsers_Load(object sender, EventArgs e)
        {
            string filepath1 = @"C:\Users\286968\OneDrive - Resideo\Desktop\publisherandlistener.json";

            if (File.Exists(filepath1))
            {
                string data = File.ReadAllText(filepath1);

                users = JsonConvert.DeserializeObject<List<User>>(data);

                dataGridView1.DataSource = users;

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

                User u = users[e.RowIndex];

                if (buttoncell.Value.ToString() == "Start" && u.Type.ToString() == "Publisher")
                {
                    if (IsFormOpen(typeof(EventSendingForm)))//when clicking the start button of type publisher,two similar forms are opening so this boolean method is used to avoiid that.
                    {
                        return;
                    } 
                    this.Hide();
                    LoggerConfig._LogInformation("Opening Event Sending Form");
                    EventSendingForm eventsendingform = new EventSendingForm(buttoncell, u,this);
                    eventsendingform.Show();
                    
                }

                else if (buttoncell.Value.ToString() == "Start" && u.Type.ToString() == "Listener")
                {
                    this.Hide();
                    LoggerConfig._LogInformation("Opening a Event Receiving Form");
                    EventReceivingForm eventreceivingform = new EventReceivingForm(buttoncell, u,this);
                    eventreceivingform.Show();


                }

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsFormOpen(Type formtype)
        {
            foreach(Form form in Application.OpenForms)
            {
                if (form.GetType() == formtype)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
