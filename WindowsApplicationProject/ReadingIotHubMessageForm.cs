using Azure.Data.Tables;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.ServiceModel.Channels;

namespace WindowsApplicationProject
{
    public partial class ReadingIoTHubMessageForm : Form
    {
        private readonly ReadD2CFromIoTHubForm readD2CFromIoTHubForm;
        private readonly Listener listener;
        private readonly DataGridViewButtonCell buttons;
        TableServiceClient table;
        TableClient tableclient;
        int receivingtime;
        BlobContainerClient blob;
        EventProcessorClient client;

        public ReadingIoTHubMessageForm(ReadD2CFromIoTHubForm readD2CFromIoTHubForm,Listener listener,DataGridViewButtonCell buttons)
        {
            InitializeComponent();
            this.readD2CFromIoTHubForm = readD2CFromIoTHubForm;
            this.listener = listener;
            this.buttons = buttons;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Form_closing1;
        }

        private void Form_closing1(object sender, FormClosingEventArgs e)
        {
            readD2CFromIoTHubForm.Show();
            buttons.Value = "Start";
            buttons.UseColumnTextForButtonValue = false;
        }

        private void ReadingIoTHubMessageForm_Load(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Processing")
            {
                await client.StopProcessingAsync();
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                LoggerConfig._LogInformation("Event data processing stopped");
                button2.Text = "Start";
            }
            this.Close();

            buttons.Value = "Start";
            buttons.UseColumnTextForButtonValue = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            if (button2.Text == "Start")
            {
                button2.Text = "Processing";
                try
                {
                    LoggerConfig._LogInformation("Event receiving from event Hub ");
                    ReceivingFromEventHub();
                }
                catch (Exception ex)
                {
                    LoggerConfig._LogError("Error Occurred ", ex);
                }
            }
        }

        private async  void button3_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Processing")
            {

                await client.StopProcessingAsync();
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                LoggerConfig._LogInformation("Event data processing stopped");
                button2.Text = "Start";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            table = new TableServiceClient(listener.StorageAccount);
            tableclient = table.GetTableClient("IoTHub");
            tableclient.CreateIfNotExists();
            if ((!(String.IsNullOrWhiteSpace(textBox1.Text)) && !(String.IsNullOrWhiteSpace(textBox2.Text))))
            {
                blob = new BlobContainerClient(listener.StorageAccount,listener.Container);



                client = new EventProcessorClient(blob, listener.ConsumerGroup,listener.EventHubConnectionString);

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
            else if (!(String.IsNullOrWhiteSpace(textBox1.Text)) && (String.IsNullOrWhiteSpace(textBox2.Text)))
            {
                MessageBox.Show("No Data to Display");

                button2.Text = "Start";

            }
            else
            {
                blob = new BlobContainerClient(listener.StorageAccount, listener.Container);

                client = new EventProcessorClient(blob, listener.ConsumerGroup, listener.EventHubConnectionString);

                client.ProcessEventAsync += ProcessHandler1;

                client.ProcessErrorAsync += ErrorHandler;

                await client.StartProcessingAsync();
                LoggerConfig._LogInformation("Processing of Event data started");
            }


        }

        async Task<int> ProcessHandler(ProcessEventArgs eventdata)
        {
            //var bytes = eventdata.Data.EventBody.ToArray();
            //var bodyString = Encoding.UTF8.GetString(bytes);

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
            //var bytes = eventdata.Data.EventBody.ToArray();
            //var bodyString = Encoding.UTF8.GetString(bytes);

            var data = Convert.ToString(eventdata.Data.EventBody);
            var enqueuedtime = eventdata.Data.EnqueuedTime.ToLocalTime();


            var time = DateTime.Now.AddMinutes(-receivingtime);

            var jsondata = JsonConvert.DeserializeObject<WindowsApplicationProject.events>(data);

            if (enqueuedtime.TimeOfDay >= time.TimeOfDay)
            {
                //eventlist.Add(jsondata);
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
            var entity = new TableEntity("IoTHub", Guid.NewGuid().ToString())
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
            LoggerConfig._LogError("error occurred while processing Event Data", eventdata.Exception);
            MessageBox.Show("Error Occurred");
            return Task.CompletedTask;
        }
    }
}
