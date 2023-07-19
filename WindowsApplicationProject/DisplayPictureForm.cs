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
    public partial class DisplayPictureForm : Form
    {
        private readonly Image image;
        private readonly StorageAccountMainPage storageAccountMainPage;

        public DisplayPictureForm(Image image,StorageAccountMainPage storageAccountMainPage)
        {
            InitializeComponent();
            this.image = image;
            this.storageAccountMainPage = storageAccountMainPage;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            FormClosing += DisplayPicture_Close;
        }

        private void DisplayPicture_Close(object sender, FormClosingEventArgs e)
        {
            storageAccountMainPage.Show();
        }

        private void DisplayPictureForm_Load(object sender, EventArgs e)
        {
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = image;
            Controls.Add(pictureBox1);
        }
    }
}
