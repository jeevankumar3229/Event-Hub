using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class IoTHubListenerForm : Form
    {
        private readonly ReadD2CFromIoTHubForm readD2CFromIoTHubForm;
        List<Listener> listeners;

        public IoTHubListenerForm(ReadD2CFromIoTHubForm readD2CFromIoTHubForm)
        {
            InitializeComponent();
            this.readD2CFromIoTHubForm = readD2CFromIoTHubForm;
            FormClosing += ListenerForm_Closing;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void ListenerForm_Closing(object sender, FormClosingEventArgs e)
        {
            readD2CFromIoTHubForm.Close();
        }

        private void IoTHubListenerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing a Listener Form");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int flag = 0;
            string listenername = textBox6.Text;
            string hubconstring = textBox1.Text;
            
            string consumergroup = textBox4.Text;
            string storageaccount = textBox3.Text;
            string containername = textBox2.Text;
            if (!(String.IsNullOrWhiteSpace(listenername)) && !(String.IsNullOrWhiteSpace(hubconstring)) && !(String.IsNullOrWhiteSpace(consumergroup)) && !(String.IsNullOrWhiteSpace(storageaccount)) && !(String.IsNullOrWhiteSpace(containername)))
            {
                textBox6.ReadOnly = true;
                textBox1.ReadOnly = true;
                
                textBox4.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox2.ReadOnly = true;

                Listener l = new Listener
                {
                    Name = listenername,
                    EventHubConnectionString=hubconstring,
                    ConsumerGroup = consumergroup,
                    StorageAccount = storageaccount,
                    Container = containername,
                    Type = "Listener"
                };

                string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\iothublistener.json";
                try
                {

                    BlobContainerClient blob = new BlobContainerClient(l.StorageAccount, l.Container);
                    if (blob.Exists())
                    {

                        EventProcessorClient eventProcessorClient = new EventProcessorClient(blob, l.ConsumerGroup, l.EventHubConnectionString);

                        eventProcessorClient.ProcessEventAsync += Processhandler;
                        eventProcessorClient.ProcessErrorAsync += Errorhandler;

                        eventProcessorClient.StartProcessing();

                        eventProcessorClient.StopProcessing();

                        if (File.Exists(filepath))
                        {
                            string filedata = File.ReadAllText(filepath);
                            listeners = JsonConvert.DeserializeObject<List<Listener>>(filedata);
                            foreach(Listener listener in listeners)
                            {
                                if ((listener.EventHubConnectionString == l.EventHubConnectionString) && (listener.ConsumerGroup == l.ConsumerGroup) && (listener.StorageAccount == l.StorageAccount) && (listener.Container == l.Container))
                                {
                                    flag = 1;
                                }
                            }

                            listeners.Add(l);

                        }
                        else
                        {
                            listeners = new List<Listener> { l };

                        }
                        if (flag == 0)
                        {
                            string data = JsonConvert.SerializeObject(listeners, Formatting.Indented);
                            File.WriteAllText(filepath, data);
                            LoggerConfig._LogInformation("Listener Successfully Registered");
                            MessageBox.Show("Listener Successfully Registered");
                        }
                        else
                        {
                            MessageBox.Show("Listener Already Registered");
                        }

                        LoggerConfig._LogInformation("Opening form Page");
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Details");
                        textBox6.ReadOnly = false;
                        textBox1.ReadOnly = false;
                        
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
    }
}
