using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Azure;
using Microsoft.Rest;
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
    public partial class DeviceRegistrationForm : Form
    {
        List<Device> devices;
        private readonly Form1 form1;

        public DeviceRegistrationForm(Form1 form1)
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

        private async  void button1_Click(object sender, EventArgs e)
        {
            int flag = 0;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            button1.Enabled = false;
            button2.Enabled = false;
            string iothubconstring = textBox1.Text;
            string hostname = textBox2.Text;
            string deviceconstring = textBox3.Text;
            string deviceid = textBox4.Text;
            if (!(String.IsNullOrWhiteSpace(iothubconstring)) && !(String.IsNullOrWhiteSpace(iothubconstring)) && !(String.IsNullOrWhiteSpace(deviceconstring)) && !(String.IsNullOrWhiteSpace(deviceid)))
            {
                

                Device device = new Device
                {
                    IotHubConnectionString = iothubconstring,
                    HostName = hostname,
                    DeviceConnectionString=deviceconstring,
                    DeviceID=deviceid
                };
                try
                {
                    string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\device.json";
                    RegistryManager registryManager = RegistryManager.CreateFromConnectionString(device.IotHubConnectionString);
                    var deviceobject = await registryManager.GetDeviceAsync(device.DeviceID);
                    if (deviceobject != null)
                    {
                        DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(device.DeviceConnectionString);
                        if (device.DeviceConnectionString.Contains(device.DeviceID) && device.DeviceConnectionString.Contains(device.HostName) && device.IotHubConnectionString.Contains(device.HostName))
                        {
                            if (File.Exists(filepath))
                            {
                                string filedata = File.ReadAllText(filepath);
                                devices = JsonConvert.DeserializeObject<List<Device>>(filedata);
                                foreach(Device device1 in devices)
                                {
                                    if((device1.IotHubConnectionString==device.IotHubConnectionString)&&(device1.HostName==device.HostName) && (device1.DeviceConnectionString == device.DeviceConnectionString) && (device1.DeviceID == device.DeviceID))
                                    {
                                        flag = 1;
                                    }
                                }
                                devices.Add(device);

                            }
                            else
                            {
                                devices = new List<Device> { device };

                            }
                            if (flag == 0)
                            {
                                string data = JsonConvert.SerializeObject(devices, Formatting.Indented);
                                File.WriteAllText(filepath, data);
                                LoggerConfig._LogInformation("Device Successfully Registered");
                                MessageBox.Show("Device Successfully Registered");
                            }
                            else
                            {
                                MessageBox.Show("Device Already Registered");
                            }
                            LoggerConfig._LogInformation("Opening form Page");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Details");
                            textBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                            textBox3.ReadOnly = false;
                            textBox4.ReadOnly = false;
                            button1.Enabled = true;
                            button2.Enabled = true; 
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid Details");
                        textBox1.ReadOnly = false;
                        textBox2.ReadOnly = false;
                        textBox3.ReadOnly = false;
                        textBox4.ReadOnly = false;
                        button1.Enabled = true;
                        button2.Enabled = true;
                    }
                    
                    
                }
                catch(Exception)
                {
                    MessageBox.Show("Invalid Details");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    textBox4.ReadOnly = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                }

              
                
            }
            else
            {
                LoggerConfig._LogError("All Fields in Device Registration Form need to be filled", null);
                MessageBox.Show("Every fields should be filled");
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                button1.Enabled = true;
                button2.Enabled = true;

            }
            

        }

        private void DeviceRegistrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
