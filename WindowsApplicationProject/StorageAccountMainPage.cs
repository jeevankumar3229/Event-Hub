using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage.Table;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;
using static System.Windows.Forms.LinkLabel;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsApplicationProject
{
    public partial class StorageAccountMainPage : Form
    {
        private readonly Storage_Account storage_Account;
        private readonly RegisteredStorageAccounts registeredStorageAccounts;
        
        List<string> list = new List<string>();
        BlobServiceClient client;
        BlobContainerClient blobContainerClient;
        int click = 0;

        public StorageAccountMainPage(Storage_Account storage_Account,RegisteredStorageAccounts registeredStorageAccounts)
        {
            InitializeComponent();
            this.storage_Account = storage_Account;
            this.registeredStorageAccounts = registeredStorageAccounts;
            
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += StorageAccountMain_FormClosing;
            
            comboBox1.Click += combo_click;
            comboBox2.Click += combo2_click;
            dataGridView1.AutoGenerateColumns = false;
           
            dataGridView1.CellContentClick += content_click;
        }

        private void combo2_click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            

            CloudStorageAccount account = CloudStorageAccount.Parse(storage_Account.Storage_Account_Connection);

            var containers = account.CreateCloudBlobClient();

            foreach (CloudBlobContainer container in containers.ListContainers())
            {
                comboBox2.Items.Add(container.Name);
            }

            /*foreach (string containername in containersnames)
            {
                comboBox2.Items.Add(containername);
            }*/
        }

        private void combo_click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            

            CloudStorageAccount account = CloudStorageAccount.Parse(storage_Account.Storage_Account_Connection);

            var table = account.CreateCloudTableClient();

            foreach (CloudTable clienttable in table.ListTables())
            {
                comboBox1.Items.Add(clienttable.Name);
            }

            /*foreach (string tablename in tables)
            {
                comboBox1.Items.Add(tablename);
            }*/
        }

        private void StorageAccountMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            registeredStorageAccounts.Show();
        }

        private void StorageAccountMainPage_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {

                var data = comboBox1.SelectedItem.ToString();
                int count = 0;
                var index = -1;


                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storage_Account.Storage_Account_Connection);

                var tableclient = cloudStorageAccount.CreateCloudTableClient();

                var table1 = tableclient.GetTableReference(data.ToString());


                dataGridView1.Columns.Clear();

                List<string> columnlist = new List<string>();


                TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>();

                foreach (DynamicTableEntity dynamicTableEntity in table1.ExecuteQuery(query))
                {
                    if (count == 0)
                    {
                        dataGridView1.Columns.Add("Partition Key", "Partition Key");
                        columnlist.Add("Partition Key");
                        dataGridView1.Columns.Add("Row Key", "Row Key");
                        columnlist.Add("Row Key");
                        dataGridView1.Columns.Add("TimeStamp", "TimeStamp");
                        columnlist.Add("TimeStamp");
                        count++;

                    }



                    foreach (var property in dynamicTableEntity.Properties)
                    {
                        if (!(dataGridView1.Columns.Contains(property.Key)))
                        {
                            dataGridView1.Columns.Add(property.Key, property.Key);
                            columnlist.Add(property.Key);

                        }

                    }


                }




                foreach (DynamicTableEntity dynamicTableEntity1 in table1.ExecuteQuery(query))
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = dynamicTableEntity1.PartitionKey });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = dynamicTableEntity1.RowKey });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = dynamicTableEntity1.Timestamp });
                    int j = 3;

                    foreach (var property in dynamicTableEntity1.Properties)
                    {

                        foreach (var columnname in columnlist)
                        {
                            if (columnname == property.Key)
                            {
                                index = columnlist.IndexOf(columnname);

                            }
                        }
                        for (int i = j; i <= index; i++)
                        {
                            if (i == index)
                            {
                                var value = property.Value.PropertyType;
                                switch (value)
                                {
                                    case EdmType.Binary:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.BinaryValue });
                                        break;
                                    case EdmType.String:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.StringValue });
                                        break;
                                    case EdmType.Boolean:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.BooleanValue });
                                        break;
                                    case EdmType.DateTime:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.DateTime });
                                        break;
                                    case EdmType.Double:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.DoubleValue });
                                        break;
                                    case EdmType.Guid:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.GuidValue });
                                        break;
                                    case EdmType.Int32:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.Int32Value });
                                        break;
                                    case EdmType.Int64:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = property.Value.Int64Value });
                                        break;

                                    default: break;



                                }
                            }
                            else
                            {
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = " " });
                            }
                        }
                        j = index + 1;



                    }
                    dataGridView1.Rows.Add(row);
                }


            }
            else
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                click = 1;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("Name", "Name");
                dataGridView1.Columns[0].Width = 730;


                var combo2data = comboBox2.SelectedItem.ToString();
                client = new BlobServiceClient(storage_Account.Storage_Account_Connection);
                blobContainerClient = client.GetBlobContainerClient(combo2data);
                var blobs = blobContainerClient.GetBlobs();

                foreach (BlobItem blobItem in blobs)
                {
                    DataGridViewRow dataGridViewRow = new DataGridViewRow();

                    dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = blobItem.Name });
                    list.Add(blobItem.Name); //this list is required to display the content of blob when you click on blob object
                    MemoryStream memoryStream = new MemoryStream();


                    dataGridView1.Rows.Add(dataGridViewRow);
                }



            }
            else
            {
                MessageBox.Show("Error Occurred");
            }
        }
        

        private void content_click(object sender, DataGridViewCellEventArgs e)
        {
            if (click == 1)
            {
                if (e.RowIndex >= 0)
                {

                    string blobitemname = list[e.RowIndex];
                    BlobClient blobClient = blobContainerClient.GetBlobClient(blobitemname);
                    MemoryStream memoryStream = new MemoryStream();
                    try
                    {
                        blobClient.DownloadTo(memoryStream);
                        try
                        {

                            Image photo = Image.FromStream(memoryStream);
                            this.Hide();
                            DisplayPictureForm pictureform = new DisplayPictureForm(photo, this);
                            pictureform.Show();

                        }
                        catch (Exception)
                        {
                            var regex = new Regex(@"\d+$");

                            if (regex.Match(blobitemname).Success)
                            {
                                MessageBox.Show("No data to show");
                            }
                            else
                            {
                                object content = new StreamReader(memoryStream).ReadToEnd();
                                MessageBox.Show(content.ToString());
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No data to show");
                    }


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
