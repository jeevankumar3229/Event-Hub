using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class DeviceRegistrationForm : Form
    {
        List<Device> devices;
        private readonly Form form1;

        public DeviceRegistrationForm(Form form1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += DeviceRegistrationForm_Closing;
            this.form1 = form1;
            
        }

        private void DeviceRegistrationForm_Closing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing a Listener Form and Opening the form Page");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string iothubconstring = textBox1.Text;
            string iothubname = textBox2.Text;
            string deviceconstring = textBox3.Text;
            string deviceid = textBox4.Text;
            if (!(String.IsNullOrWhiteSpace(iothubconstring)) && !(String.IsNullOrWhiteSpace(iothubname)) && !(String.IsNullOrWhiteSpace(deviceconstring)) && !(String.IsNullOrWhiteSpace(deviceid)))
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;

                Device device = new Device
                {
                    IotHubConnectionString = iothubconstring,
                    IotHubName=iothubname,
                    DeviceConnectionString=deviceconstring,
                    DeviceID=deviceid
                };

                string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\device.json";
                DeviceClient deviceclient= DeviceClient
            }
            else
            {
                LoggerConfig._LogError("All Fields in Device Registration Form need to be filled", null);
                MessageBox.Show("Every fields should be filled");

            }
            

        }

        private void DeviceRegistrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
