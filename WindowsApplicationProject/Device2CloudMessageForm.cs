using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class Device2CloudMessageForm : Form
    {
        public int width = 0;
        public int rowcount = 0;
        private readonly RegisteredForms registeredForms;
        private readonly Device device;
        public int count;
        public int totaltime;
        DeviceClient deviceclient;
        Microsoft.Azure.Devices.Client.Message message;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Thread thread = new Thread(DisplayMessageBox);

        private static void DisplayMessageBox()
        {
            Thread.Sleep(750);
            MessageBox.Show("In One Minute Only 60 Messages can be sent");
            LoggerConfig._LogInformation("In One Minute Only 60 Messages can be sent");
        }

        public Device2CloudMessageForm(RegisteredForms registeredForms,Device device)
        {
            InitializeComponent();
            this.registeredForms = registeredForms;
            this.device = device;
            FormClosing += Device2CloudMessageFormClosing;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            thread.Start();
        }

        private void Device2CloudMessageFormClosing(object sender, FormClosingEventArgs e)
        {
            registeredForms.Show();
        }

        private void Device2CloudMessageForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (width < 430)
            {

                AddKeyValueTextField();
            }
            else
            {
                LoggerConfig._LogError("Maximum no of additional fields to be added is reached", null);
                MessageBox.Show("Maximum limit is Reached");
            }
        }
        public void AddKeyValueTextField()
        {
            TextBox key = new TextBox();
            Label label = new Label();
            TextBox value = new TextBox();

            key.Name = "key";

            key.Size = new Size(100, 30);



            key.Location = new System.Drawing.Point(340, 260 + rowcount * 30);

            label.Name = "labelname";

            label.Text = ":";

            label.Location = new System.Drawing.Point(465, 260 + rowcount * 30);

            value.Name = "value";

            value.Size = new Size(100, 30);

            value.Location = new System.Drawing.Point(490, 260 + rowcount * 30);

            Controls.Add(key);

            Controls.Add(value);

            Controls.Add(label);

            rowcount++;

            width = 260 + rowcount * 30;
        }
        public List<string> GetKeyTextBoxContent()
        {
            List<string> key = new List<string>();

            foreach (Control c in Controls)
            {
                if (c is TextBox && c.Name == "key")
                {
                    key.Add(c.Text);

                }
            }

            return key;
        }
        public List<string> GetValueTextBoxContent()
        {
            List<string> value = new List<string>();

            foreach (Control c in Controls)
            {
                if (c is TextBox && c.Name == "value")
                {
                    value.Add(c.Text);

                }
            }

            return value;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            button2.Enabled = false;
            try
            {
                deviceclient = DeviceClient.CreateFromConnectionString(device.DeviceConnectionString);
                await deviceclient.OpenAsync();
                List<string> keydata = GetKeyTextBoxContent();
                List<string> valuedata = GetValueTextBoxContent();

                Dictionary<string, string> attributes = new Dictionary<string, string>();

                if (keydata.Count == valuedata.Count)
                {
                    for (int i = 0; i < keydata.Count; i++)
                    {
                        if (!(String.IsNullOrWhiteSpace(keydata[i])) && !(String.IsNullOrWhiteSpace(valuedata[i])))
                        {
                            attributes.Add(keydata[i], valuedata[i]);
                        }


                    }
                }

                string jsondata = JsonConvert.SerializeObject(attributes, Formatting.Indented);

                Dictionary<string, string> eventdata = new Dictionary<string, string>();

                eventdata.Add("Body", textBox1.Text);

                eventdata.Add("Attributes", jsondata);
                var data = JsonConvert.SerializeObject(eventdata, Formatting.Indented);

                message = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(data));

                if (!(String.IsNullOrWhiteSpace(textBox1.Text)))
                {
                    if (!String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
                    {

                        var times = textBox2.Text;

                        var time = textBox3.Text;
                        try
                        {

                            int n1 = Convert.ToInt32(times);
                            int n2 = Convert.ToInt32(time);
                            count = n1;
                            if (n1 > 0 && n2 > 0)
                            {
                                if (n1 <= n2 * 60)
                                {

                                    totaltime = (n2 * 60) / n1;
                                    timer.Interval = totaltime * 1000;
                                    timer.Tick += SendingtoIoTHub;
                                    timer.Start();



                                }
                                else
                                {
                                    LoggerConfig._LogError("Enter a correct value for no of times field", null);
                                    MessageBox.Show("In One Minute Only 60 Messages can be sent");
                                    textBox1.ReadOnly = false;
                                    textBox2.ReadOnly = false;
                                    textBox3.ReadOnly = false;
                                    button2.Enabled = true;

                                }
                            }
                            else
                            {
                                LoggerConfig._LogError("Enter a Postive Number in No of times Field  and Time Field", null);
                                MessageBox.Show("Enter a Positive Number for a No of times field and Time Field");
                                textBox1.ReadOnly = false;
                                textBox2.ReadOnly = false;
                                textBox3.ReadOnly = false;
                                button2.Enabled = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerConfig._LogError("Enter a Postive Number in No of times Field and Time Field ", ex);
                            MessageBox.Show("Enter a Postive Number in No of times Field and Time Field ");
                            textBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                            textBox3.ReadOnly = false;
                            button2.Enabled = true;


                        }


                    }
                    else if (!String.IsNullOrWhiteSpace(textBox2.Text) && String.IsNullOrWhiteSpace(textBox3.Text))
                    {

                        var times = textBox2.Text;


                        try
                        {

                            int n = Convert.ToInt32(times);
                            count = n;
                            if (n > 0)
                            {
                                timer.Interval = 1000;
                                timer.Tick += SendingtoIoTHub;
                                timer.Start();

                            }
                            else
                            {
                                LoggerConfig._LogError("Enter a Postive Number in No of times Field ", null);
                                MessageBox.Show("Enter a Positive Number for a No of times field");
                                textBox1.ReadOnly = false;
                                textBox2.ReadOnly = false;
                                textBox3.ReadOnly = false;
                                button2.Enabled = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerConfig._LogError("Enter a Postive Number in No of times Field ", ex);
                            MessageBox.Show("Enter a Postive Number in No of times Field ");
                            textBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                            textBox3.ReadOnly = false;
                            button2.Enabled = true;

                        }


                    }


                    else
                    {

                        await sendingtoIoTHub1();
                        LoggerConfig._LogInformation("Message sent to IoT Hub");
                        MessageBox.Show("Data Successfully Sent to IoT Hub");


                        this.Close();
                        
                    }

                }
                else
                {
                    LoggerConfig._LogError("Body Field cannot be Empty", null);
                    MessageBox.Show("Enter the Message Body");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    button2.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                LoggerConfig._LogError("event not sent to IoT Hub", ex);
                MessageBox.Show("Data not Sent to IoT Hub");
                this.Close();
            }
        }

        private async void SendingtoIoTHub(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
                await deviceclient.SendEventAsync(message);
                LoggerConfig._LogInformation("Message sent to IoT Hub");


            }
            else
            {
                timer.Stop();
                this.Close();
                await deviceclient.CloseAsync();
                MessageBox.Show("Message sent to IoT Hub");
               

            }
        }

        private async Task sendingtoIoTHub1()
        {
            await deviceclient.SendEventAsync(message);
            await deviceclient.CloseAsync();
        }
    }
}
