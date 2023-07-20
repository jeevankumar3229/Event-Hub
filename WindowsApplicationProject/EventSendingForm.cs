using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class EventSendingForm : Form
    {
        
        public int width = 0;
        public int rowcount = 0;
        public DataGridViewButtonCell buttons;
        public User user;
        private readonly RegisteredEventHubUsers registeredEventHubUsers;
        //private readonly Form1 form1;
        public int count;
        public int totaltime;
       
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        

        string jsoneventdata;
        
        Thread thread = new Thread(DisplayMessageBox);
        EventHubProducerClient client;
        EventDataBatch batch;
        EventData eventData;

        private static void DisplayMessageBox()
        {
           
                Thread.Sleep(750);
                MessageBox.Show("In One Minute Only 60 Messages can be sent");
                LoggerConfig._LogInformation("In One Minute Only 60 Messages can be sent");
                
        }

        public EventSendingForm(DataGridViewButtonCell buttons,User user,RegisteredEventHubUsers registeredEventHubUsers)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.buttons = buttons;
            this.user = user;
            this.registeredEventHubUsers = registeredEventHubUsers;
            //this.form1 = form1;
            FormClosing += EventSendingForm_Closing;
            thread.Start();
            

            
            
        }

        

        private void EventSendingForm_Closing(object sender, FormClosingEventArgs e)
        {
            registeredEventHubUsers.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (width < 450)
            {

                AddKeyValueTextField();
            }
            else
            {
                LoggerConfig._LogError("Maximum no of additional fields to be added is reached",null);
                MessageBox.Show("Maximum limit is Reached");
            }
        }

        public void AddKeyValueTextField()
        {
            TextBox key = new TextBox();
            Label label = new Label();
            TextBox value = new TextBox();

            key.Name = "key";

            key.Size = new Size(100,30);

           

            key.Location = new System.Drawing.Point(300, 240 + rowcount * 30);

            label.Name = "labelname";
           
            label.Text = ":";
            
            label.Location= new System.Drawing.Point(425, 240 + rowcount * 30);

            value.Name = "value";

            value.Size = new Size(100, 30);

            value.Location = new System.Drawing.Point(450, 240 + rowcount * 30);

            Controls.Add(key);

            Controls.Add(value);

            Controls.Add(label);

            rowcount++;

            width = 240 + rowcount * 30;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            buttons.Value = "Stop";
            buttons.UseColumnTextForButtonValue = false;
            try {
                client = new EventHubProducerClient(user.EventHubNamespace, user.EventHubName);
                LoggerConfig._LogInformation("Successfully Connected to event hub");

                batch = await client.CreateBatchAsync();
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

                jsoneventdata = JsonConvert.SerializeObject(eventdata, Formatting.Indented);

                eventData = new EventData(Encoding.UTF8.GetBytes(jsoneventdata));
                batch.TryAdd(eventData);

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
                                    timer.Interval = totaltime*1000;
                                    timer.Tick += SendingtoEventHub;
                                    timer.Start();



                                }
                                else
                                {
                                    LoggerConfig._LogError("Enter a correct value for no of times field", null);
                                    MessageBox.Show("In One Minute Only 60 Messages can be sent");
                                    buttons.Value = "Start";
                                    buttons.UseColumnTextForButtonValue = false;
                                    textBox1.ReadOnly = false;
                                    textBox2.ReadOnly = false;
                                    textBox3.ReadOnly = false;
                                    button2.Enabled = true;
                                    button1.Enabled = true;

                                }
                            }
                            else
                            {
                                LoggerConfig._LogError("Enter a Postive Number in No of times Field  and Time Field", null);
                                MessageBox.Show("Enter a Positive Number for a No of times field and Time Field");
                                buttons.Value = "Start";
                                buttons.UseColumnTextForButtonValue = false;
                                textBox1.ReadOnly = false;
                                textBox2.ReadOnly = false;
                                textBox3.ReadOnly = false;
                                button2.Enabled = true;
                                button1.Enabled = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerConfig._LogError("Enter a Postive Number in No of times Field and Time Field ", ex);
                            MessageBox.Show("Enter a Postive Number in No of times Field and Time Field ");
                            buttons.Value = "Start";
                            buttons.UseColumnTextForButtonValue = false;
                            textBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                            textBox3.ReadOnly = false;
                            button1.Enabled = true;
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
                                timer.Tick += SendingtoEventHub;
                                timer.Start();

                            }
                            else
                            {
                                LoggerConfig._LogError("Enter a Postive Number in No of times Field ", null);
                                MessageBox.Show("Enter a Positive Number for a No of times field");
                                buttons.Value = "Start";
                                buttons.UseColumnTextForButtonValue = false;
                                textBox1.ReadOnly = false;
                                textBox2.ReadOnly = false;
                                textBox3.ReadOnly = false;
                                button2.Enabled = true;
                                button1.Enabled = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerConfig._LogError("Enter a Postive Number in No of times Field ", ex);
                            MessageBox.Show("Enter a Postive Number in No of times Field ");
                            buttons.Value = "Start";
                            buttons.UseColumnTextForButtonValue = false;
                            textBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                            textBox3.ReadOnly = false;
                            button2.Enabled = true;
                            button1.Enabled = true;

                        }
                        
                        
                    }
                    

                    else
                    {
                        
                        await sendingtoEventHub1();
                        LoggerConfig._LogInformation("event sent to Event Hub");
                        MessageBox.Show("Data Successfully Sent to Event Hub");
                        

                        this.Close();
                        buttons.Value = "Start";
                        buttons.UseColumnTextForButtonValue = false;
                    }

                }
                else
                {
                    LoggerConfig._LogError("Body Field cannot be Empty", null);
                    MessageBox.Show("Enter the Event Body");
                    buttons.Value = "Start";
                    buttons.UseColumnTextForButtonValue = false;
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    button2.Enabled = true;
                    button1.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("event not sent to Event Hub", ex);
                MessageBox.Show("Data not Sent to Event Hub");
                this.Close();
            }
        }

        private async void  SendingtoEventHub(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
                await client.SendAsync(batch);
                LoggerConfig._LogInformation("Message sent to Event Hub");

                
            }
            else
            {
                timer.Stop();
                this.Close();
                MessageBox.Show("Message sent to Event Hub");
                buttons.Value = "Start";
                buttons.UseColumnTextForButtonValue = false;

            }
        }

        public async Task sendingtoEventHub1()
        {
            
            await client.SendAsync(batch);
            


        }
        public List<string> GetKeyTextBoxContent()
        {
            List<string> key = new List<string>();

            foreach(Control c in Controls)
            {
                if(c is TextBox && c.Name=="key" )
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void EventSendingForm_Load(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
