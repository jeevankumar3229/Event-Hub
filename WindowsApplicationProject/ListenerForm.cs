using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Azure;
using Microsoft.ServiceBus.Messaging;
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
    public partial class ListenerForm : Form
    {

        List<User> user;
        private readonly Form1 form1;

        public ListenerForm(Form1 form1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += ListenerForm_Closing;
            this.form1 = form1;
        }

        private void ListenerForm_Closing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string listenername = textBox6.Text;
            string hubnamespace = textBox1.Text;
            string hubname = textBox5.Text;
            string consumergroup = textBox4.Text;
            string storageaccount = textBox3.Text;
            string containername = textBox2.Text;
            if (!(String.IsNullOrWhiteSpace(listenername)) && !(String.IsNullOrWhiteSpace(hubnamespace))&& !(String.IsNullOrWhiteSpace(hubname)) && !(String.IsNullOrWhiteSpace(consumergroup)) && !(String.IsNullOrWhiteSpace(storageaccount)) && !(String.IsNullOrWhiteSpace(containername)))
            {
                textBox6.ReadOnly = true;
                textBox1.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox2.ReadOnly = true;

                User l = new User
                {
                    Name = listenername,
                    EventHubNamespace = hubnamespace,
                    EventHubName = hubname,
                    ConsumerGroup = consumergroup,
                    StorageAccount = storageaccount,
                    Container = containername,
                    Type = "Listener"
                };

                string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\publisherandlistener.json";
                try
                {
                  
                    BlobContainerClient blob = new BlobContainerClient(l.StorageAccount, l.Container);
                    if (blob.Exists())
                    {

                        EventProcessorClient eventProcessorClient = new EventProcessorClient(blob, l.ConsumerGroup, l.EventHubNamespace, l.EventHubName);

                        eventProcessorClient.ProcessEventAsync += Processhandler;
                        eventProcessorClient.ProcessErrorAsync += Errorhandler;

                        eventProcessorClient.StartProcessing();

                        eventProcessorClient.StopProcessing();
                        
                        if (File.Exists(filepath))
                        { 
                            string filedata = File.ReadAllText(filepath);
                            user = JsonConvert.DeserializeObject<List<User>>(filedata);
                            user.Add(l);

                        }
                        else
                        {
                            user = new List<User> { l };

                        }
                        string data = JsonConvert.SerializeObject(user, Formatting.Indented);
                        File.WriteAllText(filepath, data);
                        LoggerConfig._LogInformation("Listener Successfully Registered");
                        MessageBox.Show("Listener Successfully Registered");
                        LoggerConfig._LogInformation("Opening form Page");
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Details");
                        textBox6.ReadOnly = false;
                        textBox1.ReadOnly = false;
                        textBox5.ReadOnly = false;
                        textBox4.ReadOnly = false;
                        textBox3.ReadOnly = false;
                        textBox2.ReadOnly = false;

                    }

                   
                }
                catch (Exception ex)
                {
                    LoggerConfig._LogError("Invalid Details", ex);
                    MessageBox.Show("Invalid Details");
                    textBox6.ReadOnly = false;
                    textBox1.ReadOnly = false;
                    textBox5.ReadOnly = false;
                    textBox4.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    textBox2.ReadOnly = false;

                }

            }
            else
            {
                LoggerConfig._LogError("All Fields in Listener Form need to be filled", null);
                MessageBox.Show("Every fields should be filled");
                
            }
            


        }

        private Task Errorhandler(ProcessErrorEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private Task Processhandler(ProcessEventArgs arg)
        {
            return Task.CompletedTask;
        }

        

        private void ListenerForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /*private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }*/

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing a Listener Form and Opening the form Page");
            this.Close();
        }
    }
}





