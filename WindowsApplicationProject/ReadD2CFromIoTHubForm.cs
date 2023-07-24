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
    public partial class ReadD2CFromIoTHubForm : Form
    {
        
        private readonly RegisteredForms registeredForms;
        private readonly Device device;
        List<Listener> listeners;
        
        public ReadD2CFromIoTHubForm(RegisteredForms registeredForms,Device device)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += ReadD2CFromIoTHubFormClosing;
            this.registeredForms = registeredForms;
            this.device = device;
        }

        private void ReadD2CFromIoTHubFormClosing(object sender, FormClosingEventArgs e)
        {
            registeredForms.Show();
        }

        private void ReadD2CFromIoTHubForm_Load(object sender, EventArgs e)
        {
            try
            {
                string filepath1 = @"C:\Users\286968\OneDrive - Resideo\Desktop\iothublistener.json";
                

                if (File.Exists(filepath1))
                {
                    string data = File.ReadAllText(filepath1);
                    if (data.Length > 0)
                    {

                        listeners = JsonConvert.DeserializeObject<List<Listener>>(data);

                        dataGridView1.DataSource = listeners;

                        DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                        button.HeaderText = "Action";

                        button.Text = "Start";

                        button.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(button);

                        dataGridView1.ReadOnly = true;
                        dataGridView1.Columns[0].Width = 75;
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[3].Width = 107;
                        dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
                    }



                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred", ex);
                MessageBox.Show("Error Occurred");
            }
           
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            LoggerConfig._LogInformation("Opening a Listener Form");
            IoTHubListenerForm ioTHubListenerForm = new IoTHubListenerForm(this);
            ioTHubListenerForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex >= 0)
            {
                DataGridViewButtonCell buttoncell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (IsFormOpen(typeof(ReadingIoTHubMessageForm)))//when clicking the start button of type publisher,two similar forms are opening so this boolean method is used to avoiid that.
                {
                    return;
                }
                var listener = listeners[e.RowIndex];
                this.Hide();
                ReadingIoTHubMessageForm readingIoTHubMessageForm = new ReadingIoTHubMessageForm(this, listener,buttoncell);
                readingIoTHubMessageForm.Show();
            }
        }

        private bool IsFormOpen(Type type)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
