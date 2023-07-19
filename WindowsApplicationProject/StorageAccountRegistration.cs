﻿using Microsoft.WindowsAzure.Storage;
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
            string storage_account_connection = textBox1.Text;

            string storage_account_name = textBox2.Text;

            if (!(String.IsNullOrWhiteSpace(storage_account_connection)) && !(String.IsNullOrWhiteSpace(storage_account_name)))
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;

                Storage_Account account = new Storage_Account
                {
                    Storage_Account_Connection = storage_account_connection,
                    Storage_Account_Name = storage_account_name,

                };

                CloudStorageAccount cloud = CloudStorageAccount.Parse(account.Storage_Account_Connection);




                if (cloud.Credentials.AccountName.Equals(account.Storage_Account_Name))
                {


                    string filepath = @"C:\Users\286968\OneDrive - Resideo\Desktop\storageaccount.json";

                    if (File.Exists(filepath))
                    {
                        string filedata = File.ReadAllText(filepath);

                        storage_account = JsonConvert.DeserializeObject<List<Storage_Account>>(filedata);

                        storage_account.Add(account);
                    }
                    else
                    {
                        storage_account = new List<Storage_Account> { account };
                    }
                    string data = JsonConvert.SerializeObject(storage_account, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filepath, data);
                    LoggerConfig._LogInformation("Storage Account Successfully Registered");
                    MessageBox.Show("Storage Account Successfully Registered");
                    LoggerConfig._LogInformation("Opening form Page");
                    this.Close();
                    
                }
                else
                {
                    LoggerConfig._LogError("Invalid Details", null);
                    MessageBox.Show("Invalid Details");
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                }
            }
            else
            {
                LoggerConfig._LogError("All Fields in Storage Account Form need to be filled", null);
                MessageBox.Show("Every fields should be filled");
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}