using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationProject
{
    public partial class ReadC2DFromDeviceForm : Form
    {
        private readonly RegisteredForms registeredForms;
        public readonly Device device;

        public ReadC2DFromDeviceForm(RegisteredForms registeredForms,Device device)
        {
            InitializeComponent();
            this.registeredForms = registeredForms;
            this.device = device;
            FormClosing += ReadC2DFromDeviceFormClosing;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void ReadC2DFromDeviceFormClosing(object sender, FormClosingEventArgs e)
        {
             registeredForms.Show();
            
        }

        private void ReadC2DFromDeviceForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("Body", "Body");
            dataGridView1.Columns.Add("Attributes", "Attributes");
            dataGridView1.Columns.Add("Time", "Time");
            dataGridView1.Columns[0].Width = 275;
            dataGridView1.Columns[1].Width = 275;
            dataGridView1.Columns[2].Width = 181;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            
            if (button2.Text == "Start")
            {
                button2.Text = "Processing";
                try
                {
                    LoggerConfig._LogInformation("Message receiving from Device: "+device.DeviceID);
                    DeviceClient deviceclient = DeviceClient.CreateFromConnectionString(device.DeviceConnectionString);
                    await ReceivingFromDevice(deviceclient);
                }
                catch (Exception ex)
                {
                    LoggerConfig._LogError("Error Occurred ", ex);
                }
            }
        }

        private async Task ReceivingFromDevice(DeviceClient deviceclient)
        {
            try
            {
                Microsoft.Azure.Devices.Client.Message message;
                do
                {
                    message = await deviceclient.ReceiveAsync();
                    if (message != null)
                    {
                        // MessageBox.Show("No message");
                        // button2.Text = "Start";
                        // break;

                        //}
                        //else
                        //{*/
                        try
                        {
                            string data = Encoding.ASCII.GetString(message.GetBytes());
                            var messagedata = JsonConvert.DeserializeObject<events>(data);
                            var time = message.EnqueuedTimeUtc.ToLocalTime();
                            dataGridView1.BeginInvoke(new Action(() =>
                            {

                                DataGridViewRow row = new DataGridViewRow();
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = messagedata.Body });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = messagedata.Attributes });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = time });
                                dataGridView1.Rows.Add(row);


                            }));


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Occurred");
                            LoggerConfig._LogError("Error Occurred ", ex);
                        }
                        await deviceclient.CompleteAsync(message);
                    }

                } while (message != null);
                button2.Text = "Start";
            }
            catch(Exception e)
            {
                MessageBox.Show("Error Occurred");
                LoggerConfig._LogError("Error Occurred ", e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Start")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Wait For Processing to Complete");
            }
        }
    }
}
