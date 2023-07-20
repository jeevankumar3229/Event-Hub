using Azure.Messaging.EventHubs.Producer;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static WindowsApplicationProject.ListenerForm;

namespace WindowsApplicationProject
{
    public partial class PublisherForm : Form
    {
        EventHubProducerClient client;
        List<User> user;
        private readonly Form1 form1;

        public PublisherForm(Form1 form1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Publisher_FormClosing;
            this.form1 = form1;
        }

        private void Publisher_FormClosing(object sender, FormClosingEventArgs e)
        {

            form1.Show();
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            int flag = 0;
            string publishername = textBox1.Text;
            string hubnamespace = textBox3.Text;
            string hubname = textBox2.Text;
            
            if (!(String.IsNullOrWhiteSpace(publishername)) && !(String.IsNullOrWhiteSpace(hubnamespace)) && !(String.IsNullOrWhiteSpace(hubname)))
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                User l = new User
                {
                    Name = publishername,
                    EventHubNamespace = hubnamespace,
                    EventHubName = hubname,
                    ConsumerGroup = null,
                    StorageAccount = null,
                    Container = null,
                    Type = "Publisher"
                };

                try
                {
                    client = new EventHubProducerClient(l.EventHubNamespace, l.EventHubName);

                    var properties = await client.GetEventHubPropertiesAsync();

                    string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\publisherandlistener.json";

                    if (File.Exists(filepath))
                    {
                        string filedata = File.ReadAllText(filepath);

                        user = JsonConvert.DeserializeObject<List<User>>(filedata);
                        foreach(User user1 in user)
                        {
                            if ((user1.EventHubNamespace == l.EventHubNamespace) && (user1.EventHubName == l.EventHubName))
                            {
                                flag = 1;
                            }
                        }
                        
                        user.Add(l);
                    }
                    else
                    {
                        user = new List<User> { l };
                    }
                    if (flag == 0)
                    {
                        string data = JsonConvert.SerializeObject(user, Formatting.Indented);
                        File.WriteAllText(filepath, data);
                        LoggerConfig._LogInformation("Publisher Successfully Registered");
                        MessageBox.Show("Publisher Successfully Registered");
                    }
                    else
                    {
                        MessageBox.Show("Publisher Already Registered");
                    }
                
                    LoggerConfig._LogInformation("Opening form Page");

                    this.Close();

                    await client.DisposeAsync();
                    
                    

                }
                catch(Exception ex)
                {
                    LoggerConfig._LogError("Invalid ConnectionString to eventHubNamespace or Invalid eventhub name", ex);
                    MessageBox.Show("Invalid Details");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    await client.DisposeAsync();
                }
                
            }
            else
            {
                LoggerConfig._LogError("All Fields in Publisher Form need to be filled", null);
                MessageBox.Show("Every fields should be filled");
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }
        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PublisherForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing a Publisher Form");
            this.Close();
        }
    }
}
