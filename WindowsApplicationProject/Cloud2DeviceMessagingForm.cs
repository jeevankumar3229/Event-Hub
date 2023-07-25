using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Rest;
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
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class Cloud2DeviceMessagingForm : Form
    {
        
        public int width = 0;
        public int rowcount = 0;

        private readonly RegisteredForms registeredForms;
        private readonly Device device;
        
        ServiceClient serviceClient;
        Microsoft.Azure.Devices.Message message;
        
        public Cloud2DeviceMessagingForm(RegisteredForms registeredForms,Device device)
        {
            InitializeComponent();
            
            FormClosing += Cloud2DeviceMessagingFormClosing;
            this.registeredForms = registeredForms;
            this.device = device;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            
        }

        private void Cloud2DeviceMessagingFormClosing(object sender, FormClosingEventArgs e)
        {
            registeredForms.Show();
        }

        private void Cloud2DeviceMessagingForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (width < 450)
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



            key.Location = new System.Drawing.Point(355, 210 + rowcount * 30);

            label.Name = "labelname";

            label.Text = ":";

            label.Location = new System.Drawing.Point(480, 210 + rowcount * 30);

            value.Name = "value";

            value.Size = new Size(100, 30);

            value.Location = new System.Drawing.Point(505, 210 + rowcount * 30);

            Controls.Add(key);

            Controls.Add(value);

            Controls.Add(label);

            rowcount++;

            width = 220 + rowcount * 30;
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
            button2.Enabled = false;
            textBox1.ReadOnly = true;
            try
            {
                serviceClient = ServiceClient.CreateFromConnectionString(device.IotHubConnectionString);
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

                message = new Microsoft.Azure.Devices.Message(Encoding.UTF8.GetBytes(data));

                if (!(String.IsNullOrWhiteSpace(textBox1.Text)))
                {
                    
                        await sendingtoDevice();
                        LoggerConfig._LogInformation("Message sent to Device "+device.DeviceID);
                        MessageBox.Show("Data Successfully Sent to Device "+device.DeviceID);


                        this.Close();

                    

                }
                else
                {
                    LoggerConfig._LogError("Body Field cannot be Empty", null);
                    MessageBox.Show("Enter the Message Body");
                    textBox1.ReadOnly = false;
                    button2.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                LoggerConfig._LogError("message not sent to Device "+device.DeviceID, ex);
                MessageBox.Show("Data not Sent to Device "+device.DeviceID);
                this.Close();
            }
        }

        private async Task sendingtoDevice()
        {
            await serviceClient.SendAsync(device.DeviceID,message);
        }

       
    }
}
