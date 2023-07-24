using Microsoft.Azure.Devices;
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
    public partial class RegisteredForms : Form
    {
        private readonly Form1 form1;
        public static List<Device> devices;

        public RegisteredForms(Form1 form1 )
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += RegisteredDeviceFormclosing;
            this.form1 = form1;
        }

        private void RegisteredDeviceFormclosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void RegisteredForms_Load(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\device.json";

                if (File.Exists(filepath))
                {
                    string data = File.ReadAllText(filepath);
                    if (data.Length > 0)
                    {

                        devices = JsonConvert.DeserializeObject<List<Device>>(data);

                        dataGridView1.DataSource = devices;
                        DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                        button.HeaderText = "Action";

                        button.Text = "D2C";

                        button.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(button);
                        DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();

                        button1.HeaderText = "Action";

                        button1.Text = "Read D2C from Iot hub";

                        button1.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(button1);
                        DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();

                        button2.HeaderText = "Action";

                        button2.Text = "C2D";

                        button2.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(button2);
                        DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();

                        button3.HeaderText = "Action";

                        button3.Text = "Read C2D from device";

                        button3.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(button3);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[1].Width = 80;
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[3].Width = 80;
                        dataGridView1.Columns[4].Width = 80;
                        dataGridView1.Columns[5].Width = 150;
                        dataGridView1.Columns[6].Width = 80;
                        dataGridView1.Columns[7].Width = 150;
                        dataGridView1.CellContentClick += dataGridView1_CellContentClick;
                    }

                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred", ex);
                MessageBox.Show("Error Occurred");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.Count - 4 && e.RowIndex >= 0)
            {
                var device = devices[e.RowIndex];
                this.Hide();
                Device2CloudMessageForm device2CloudMessageForm = new Device2CloudMessageForm(this,device);
                device2CloudMessageForm.Show();
            }
            if (e.ColumnIndex == dataGridView1.Columns.Count - 3 && e.RowIndex >= 0)
            {
                var device = devices[e.RowIndex];
                this.Hide();
                ReadD2CFromIoTHubForm readD2CFromIoTHubForm = new ReadD2CFromIoTHubForm(this, device);
                readD2CFromIoTHubForm.Show();
            }
            if (e.ColumnIndex == dataGridView1.Columns.Count - 2 && e.RowIndex >= 0)
            {
                var device = devices[e.RowIndex];
                this.Hide();
                Cloud2DeviceMessagingForm cloud2DeviceMessagingForm = new Cloud2DeviceMessagingForm(this, device);
                cloud2DeviceMessagingForm.Show();
            }
            if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex >= 0)
            {
                var device = devices[e.RowIndex];
                this.Hide();
                ReadC2DFromDeviceForm readC2DFromDeviceForm = new ReadC2DFromDeviceForm(this, device);
                readC2DFromDeviceForm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
