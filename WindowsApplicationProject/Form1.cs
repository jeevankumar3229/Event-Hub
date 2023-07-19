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
    }
}
