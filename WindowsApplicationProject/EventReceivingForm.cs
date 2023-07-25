using Azure.Data.Tables;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    
    public partial class EventReceivingForm : Form
    {
        private User user;
        int receivingtime;
        
        private readonly RegisteredEventHubUsers registeredEventHubUsers;
        private DataGridViewButtonCell buttons;
        TableServiceClient table;
        TableClient tableclient;

        BlobContainerClient blob;
        EventProcessorClient client;
        
        public EventReceivingForm(DataGridViewButtonCell buttons,User user,RegisteredEventHubUsers registeredEventHubUsers)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.buttons = buttons;
            FormClosing += EventReceivingForm_Closing;
            this.user = user;
            this.registeredEventHubUsers = registeredEventHubUsers;
            
        }

        private void EventReceivingForm_Closing(object sender, FormClosingEventArgs e)
        {
            registeredEventHubUsers.Show();
            buttons.Value = "Start";
            buttons.UseColumnTextForButtonValue = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EventReceivingForm_Load(object sender, EventArgs e)
        {
            buttons.Value = "Stop";
            buttons.UseColumnTextForButtonValue = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;
            
            dataGridView1.Columns.Add("Body", "Body");
            dataGridView1.Columns.Add("Attributes", "Attributes");
            dataGridView1.Columns.Add("Time", "Time");
            dataGridView1.Columns[0].Width = 350;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 193;
           
        }

        
        

        private async void ReceivingFromEventHub()
        {
            if (textBox3.Text != null)
            {

                try
                {
                    receivingtime = Convert.ToInt32(textBox3.Text);
                    if (receivingtime <= 0)
                    {
                        throw new Exception();
                    }

                }
                catch (Exception)
                {

                    receivingtime = 5;
                    MessageBox.Show("Time is set to 5 mins by default because the entered time is either invalid or less than zero or equal to zero ");
                }
            }
            else
            {
                receivingtime = 5;
            }
            table = new TableServiceClient(user.StorageAccount);
            tableclient = table.GetTableClient("EventHub");
            tableclient.CreateIfNotExists();
            if ((!(String.IsNullOrWhiteSpace(textBox1.Text)) && !(String.IsNullOrWhiteSpace(textBox2.Text))))
            {
                blob = new BlobContainerClient(user.StorageAccount, user.Container);

                

                client = new EventProcessorClient(blob, user.ConsumerGroup, user.EventHubNamespace, user.EventHubName);

                client.ProcessEventAsync += ProcessHandler;

                client.ProcessErrorAsync += ErrorHandler;

                await client.StartProcessingAsync();
                LoggerConfig._LogInformation("Processing of Event data started");

                

                
            }
            else if ((String.IsNullOrWhiteSpace(textBox1.Text)) && !(String.IsNullOrWhiteSpace(textBox2.Text)))
            {
                MessageBox.Show("No Data to Display");
                button2.Text = "Start";

            }
            else if(!(String.IsNullOrWhiteSpace(textBox1.Text)) && (String.IsNullOrWhiteSpace(textBox2.Text)))
            {
                MessageBox.Show("No Data to Display");
               
                button2.Text = "Start";
                
            }
            else
            {
                blob = new BlobContainerClient(user.StorageAccount, user.Container);

                client = new EventProcessorClient(blob, user.ConsumerGroup, user.EventHubNamespace, user.EventHubName);

                client.ProcessEventAsync += ProcessHandler1;

                client.ProcessErrorAsync += ErrorHandler;

                await client.StartProcessingAsync();
                LoggerConfig._LogInformation("Processing of Event data started");
            }
            

        }

         async Task<int> ProcessHandler(ProcessEventArgs eventdata)
        {
          
            var data = Convert.ToString(eventdata.Data.EventBody);
            var enqueuedtime = eventdata.Data.EnqueuedTime.ToLocalTime();
            var time = DateTime.Now.AddMinutes(-5);
            var jsondata = JsonConvert.DeserializeObject<events>(data);
           
            if (jsondata.Attributes.Contains(textBox1.Text) && jsondata.Attributes.Contains(textBox2.Text))
            {
                if (enqueuedtime.TimeOfDay >= time.TimeOfDay)
                {
                    
                    dataGridView1.BeginInvoke(new Action(() =>
                    {

                        DataGridViewRow row = new DataGridViewRow();
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = jsondata.Body });
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = jsondata.Attributes });
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = enqueuedtime });
                        dataGridView1.Rows.Add(row);


                    }));
                }
                
                
            }
            await eventdata.UpdateCheckpointAsync();
            var entity = new TableEntity("EventHub", Guid.NewGuid().ToString())
            {
                {"Body",jsondata.Body },
                {"Attributes",jsondata.Attributes },
                {"Enqueued_Time",enqueuedtime }
            };
            tableclient.AddEntity(entity);
            return 0;







        }
        async Task<int> ProcessHandler1(ProcessEventArgs eventdata)
        {

            var data = Convert.ToString(eventdata.Data.EventBody);
            var enqueuedtime = eventdata.Data.EnqueuedTime.ToLocalTime();
           
            
            var time = DateTime.Now.AddMinutes(-receivingtime);
           
            var jsondata = JsonConvert.DeserializeObject<events>(data);

            if (enqueuedtime.TimeOfDay >= time.TimeOfDay)
            {
                
                dataGridView1.BeginInvoke(new Action(() =>
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = jsondata.Body });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = jsondata.Attributes });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = enqueuedtime });
                    dataGridView1.Rows.Add(row);

                }));
            }
            await eventdata.UpdateCheckpointAsync();
            var entity = new TableEntity("EventHub", Guid.NewGuid().ToString())
            {
                {"Body",jsondata.Body },
                {"Attributes",jsondata.Attributes },
                {"Enqueued_Time",enqueuedtime }
            };
            tableclient.AddEntity(entity);
            return 0;




        }

        Task ErrorHandler(ProcessErrorEventArgs eventdata)
        {
            LoggerConfig._LogError("error occurred while processing Event Data",eventdata.Exception);
            MessageBox.Show("Error Occurred");
            return Task.CompletedTask;
        }

        


        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            if (button2.Text == "Start")
            {
                button2.Text = "Processing";
                try
                {
                    LoggerConfig._LogInformation("Event receiving from event Hub ");
                    ReceivingFromEventHub();
                }
                catch(Exception ex)
                {
                    LoggerConfig._LogError("Error Occurred ", ex);
                }
            }
            
          
        }

        

        private async  void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button2.Text == "Processing")
                {
                    await client.StopProcessingAsync();
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    LoggerConfig._LogInformation("Event data processing stopped");
                    button2.Text = "Start";
                }
                this.Close();

                buttons.Value = "Start";
                buttons.UseColumnTextForButtonValue = false;
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred",ex);
                this.Close();
            }
        }

        private async  void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (button2.Text == "Processing")
                {

                    await client.StopProcessingAsync();
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    LoggerConfig._LogInformation("Event data processing stopped");
                    button2.Text = "Start";
                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred", ex);
                MessageBox.Show("Error Occurred");
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
