using Microsoft.Win32;
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
using Serilog;

namespace WindowsApplicationProject
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += Form1_closing;
            DisplayContentinTextBox();
           
        }

        private void DisplayContentinTextBox()
        {
            string name = @"EVENT HUB :
Event Hubs is a modern big data streaming platform and event ingestion service that can seamlessly integrate with other Azure and Microsoft services, such as Stream Analytics, Power BI, and Event Grid, along with outside services like Apache Spark.
In the Event Hub Section,First Register the Publisher and the Listener Separately,and then send or receive events to or from the Event Hub. 
STORAGE ACCOUNT:
A storage account is a container that bands a set of Azure Storage services together.
In the Storage Account Section,First Register the Storage Account, and then get the list of tables and the containers in that Storage Account.
IOT HUB:
IoT hub is a Platform-as-a-Service (PaaS) managed service and can be used as a focal point that allows devices to be connected to the Internet. 
In the IoT Hub Section,First Register the Device,and then perform the operations such as Device-to-Cloud Messaging , Reading Device-to-Cloud Messages from the Iot Hub,Cloud-to-Device Messaging, Reading Cloud-to-Device Messages from the Device side.";
            textBox1.Text = name;
           
        }

        private void Form1_closing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        
        private void eVENTHUBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoggerConfig._LogInformation("Opening a Publisher Form");
            PublisherForm publisher = new PublisherForm(this);
            publisher.Show();
            
        }

        private void registerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoggerConfig._LogInformation("Opening a Listener Form");
            ListenerForm listener = new ListenerForm(this);
            listener.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Closing the Application");
            this.Close();
            
        }

       
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void registeredUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoggerConfig._LogInformation("Opening a RegisteredUsers Form");
            RegisteredEventHubUsers users = new RegisteredEventHubUsers(this);
            users.Show();
           
        }

        private void registerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Opening a Storage Account Registration Form");
            this.Hide();
            StorageAccountRegistration storageAccountRegistration = new StorageAccountRegistration(this);
            storageAccountRegistration.Show();
        }

        private void storageAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void registeredStorageAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Opening a Registered Storage Account Form");
            this.Hide();
            RegisteredStorageAccounts registeredStorageAccounts = new RegisteredStorageAccounts(this);
            registeredStorageAccounts.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void registerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Opening a Device Registration Form");
            this.Hide();
            DeviceRegistrationForm deviceRegistrationForm = new DeviceRegistrationForm(this);
            deviceRegistrationForm.Show();
        }

        private void registeredDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoggerConfig._LogInformation("Opening a Registered Devices Form");
            this.Hide();
            RegisteredForms registeredForms = new RegisteredForms(this);
            registeredForms.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
