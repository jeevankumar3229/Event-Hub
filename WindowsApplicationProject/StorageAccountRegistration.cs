using Microsoft.WindowsAzure.Storage;
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
using static WindowsApplicationProject.StorageAccountRegistration;

namespace WindowsApplicationProject
{
    public partial class StorageAccountRegistration : Form
    {
        List<Storage_Account> storage_account;
        private readonly Form1 form1;

        public StorageAccountRegistration(Form1 form1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += StorageAccount_FormClosing;
            this.form1 = form1;
        }

        private void StorageAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void StorageAccountRegistration_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            int flag = 0;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            string storage_account_connection = textBox1.Text;

            string storage_account_name = textBox2.Text;
            try
            {

                if (!(String.IsNullOrWhiteSpace(storage_account_connection)) && !(String.IsNullOrWhiteSpace(storage_account_name)))
                {


                    Storage_Account account = new Storage_Account
                    {
                        Storage_Account_Connection = storage_account_connection,
                        Storage_Account_Name = storage_account_name,

                    };

                    CloudStorageAccount cloud = CloudStorageAccount.Parse(account.Storage_Account_Connection);




                    if (cloud.Credentials.AccountName.Equals(account.Storage_Account_Name) && account.Storage_Account_Connection.EndsWith("core.windows.net"))
                    {


                        string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\storageaccount.json";

                        if (File.Exists(filepath))
                        {
                            string filedata = File.ReadAllText(filepath);
                            if (filedata.Length > 0)
                            {

                                storage_account = JsonConvert.DeserializeObject<List<Storage_Account>>(filedata);

                                foreach (Storage_Account storage_Account in storage_account)
                                {
                                    if ((String.Equals(storage_Account.Storage_Account_Connection, account.Storage_Account_Connection, StringComparison.OrdinalIgnoreCase)) && (String.Equals(storage_Account.Storage_Account_Name, account.Storage_Account_Name, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        flag = 1;
                                    }
                                }


                                storage_account.Add(account);
                            }
                            else
                            {
                                storage_account = new List<Storage_Account> { account };
                            }
                        }
                        else
                        {
                            storage_account = new List<Storage_Account> { account };
                        }
                        if (flag == 0)
                        {
                            string data = JsonConvert.SerializeObject(storage_account, Newtonsoft.Json.Formatting.Indented);
                            File.WriteAllText(filepath, data);
                            LoggerConfig._LogInformation("Storage Account Successfully Registered");
                            MessageBox.Show("Storage Account Successfully Registered");
                        }
                        else
                        {
                            MessageBox.Show("Storage Account Already Registered");
                        }
                        LoggerConfig._LogInformation("Opening form Page");
                        this.Close();

                    }
                    else
                    {
                        LoggerConfig._LogError("Invalid Details", null);
                        MessageBox.Show("Invalid Details");
                        textBox1.ReadOnly = false;
                        textBox2.ReadOnly = false;
                        button1.Enabled = true;
                        button2.Enabled = true;
                    }
                }
                else
                {
                    LoggerConfig._LogError("All Fields in Storage Account Form need to be filled", null);
                    MessageBox.Show("Every fields should be filled");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    button1.Enabled = true;
                    button2.Enabled = true;

                }
            }
            catch(Exception ex)
            {
                LoggerConfig._LogError("Error Occurred", ex);
                MessageBox.Show("Error Occurred");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
