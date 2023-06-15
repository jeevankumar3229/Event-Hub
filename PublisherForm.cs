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
        public PublisherForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Publisher_FormClosing;
        }

        private void Publisher_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Form1 home = new Form1();
            home.Show();
            //this.Close();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
           
            string data1 = textBox1.Text;
            string data2 = textBox3.Text;
            string data3 = textBox2.Text;
            
            if (!(String.IsNullOrWhiteSpace(data1)) && !(String.IsNullOrWhiteSpace(data2)) && !(String.IsNullOrWhiteSpace(data1)))
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                User l = new User
                {
                    Name = data1,
                    EventHubNamespace = data2,
                    EventHubName = data3,
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

                        List<User> user = JsonConvert.DeserializeObject<List<User>>(filedata);
                        
                        user.Add(l);
                    }
                    else
                    {
                        List<User> user = new List<User> { l };
                    }
                    string data = JsonConvert.SerializeObject(user, Formatting.Indented);
                    File.WriteAllText(filepath, data);
                    LoggerConfig._LogInformation("Publisher Successfully Registered");
                    MessageBox.Show("Publisher Successfully Registered");
                    LoggerConfig._LogInformation("Opening Home Page");
                    Form1 home = new Form1();
                    home.Show();
                    this.Hide();
                    await client.DisposeAsync();
                    

                }
                catch(Exception ex)
                {
                    LoggerConfig._LogError("Invalid ConnectionString to eventHubNamespace or Invalid eventhub name", ex);
                    MessageBox.Show("Invalid Details");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PublisherForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing a Publisher Form");
            /*Form1 home = new Form1();
            home.Show();*/
            this.Close();
        }
    }
}
