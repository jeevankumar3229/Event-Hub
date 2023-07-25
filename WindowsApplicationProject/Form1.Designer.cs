namespace WindowsApplicationProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.eVENTHUBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storageAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredStorageAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ioTHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eVENTHUBToolStripMenuItem,
            this.storageAccountToolStripMenuItem,
            this.ioTHubToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(751, 63);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip2_ItemClicked);
            // 
            // eVENTHUBToolStripMenuItem
            // 
            this.eVENTHUBToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.eVENTHUBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rEToolStripMenuItem,
            this.listenerToolStripMenuItem,
            this.registeredUsersToolStripMenuItem});
            this.eVENTHUBToolStripMenuItem.Name = "eVENTHUBToolStripMenuItem";
            this.eVENTHUBToolStripMenuItem.Padding = new System.Windows.Forms.Padding(20);
            this.eVENTHUBToolStripMenuItem.Size = new System.Drawing.Size(106, 59);
            this.eVENTHUBToolStripMenuItem.Text = "Event Hub";
            this.eVENTHUBToolStripMenuItem.Click += new System.EventHandler(this.eVENTHUBToolStripMenuItem_Click);
            // 
            // rEToolStripMenuItem
            // 
            this.rEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerToolStripMenuItem});
            this.rEToolStripMenuItem.Name = "rEToolStripMenuItem";
            this.rEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rEToolStripMenuItem.Text = "Publisher";
            this.rEToolStripMenuItem.Click += new System.EventHandler(this.rEToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // listenerToolStripMenuItem
            // 
            this.listenerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerToolStripMenuItem1});
            this.listenerToolStripMenuItem.Name = "listenerToolStripMenuItem";
            this.listenerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.listenerToolStripMenuItem.Text = "Listener";
            // 
            // registerToolStripMenuItem1
            // 
            this.registerToolStripMenuItem1.Name = "registerToolStripMenuItem1";
            this.registerToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.registerToolStripMenuItem1.Text = "Register";
            this.registerToolStripMenuItem1.Click += new System.EventHandler(this.registerToolStripMenuItem1_Click);
            // 
            // registeredUsersToolStripMenuItem
            // 
            this.registeredUsersToolStripMenuItem.Name = "registeredUsersToolStripMenuItem";
            this.registeredUsersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.registeredUsersToolStripMenuItem.Text = "Registered Users";
            this.registeredUsersToolStripMenuItem.Click += new System.EventHandler(this.registeredUsersToolStripMenuItem_Click);
            // 
            // storageAccountToolStripMenuItem
            // 
            this.storageAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerToolStripMenuItem2,
            this.registeredStorageAccountsToolStripMenuItem});
            this.storageAccountToolStripMenuItem.Name = "storageAccountToolStripMenuItem";
            this.storageAccountToolStripMenuItem.Size = new System.Drawing.Size(107, 59);
            this.storageAccountToolStripMenuItem.Text = "Storage Account";
            this.storageAccountToolStripMenuItem.Click += new System.EventHandler(this.storageAccountToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem2
            // 
            this.registerToolStripMenuItem2.Name = "registerToolStripMenuItem2";
            this.registerToolStripMenuItem2.Size = new System.Drawing.Size(225, 22);
            this.registerToolStripMenuItem2.Text = "Register";
            this.registerToolStripMenuItem2.Click += new System.EventHandler(this.registerToolStripMenuItem2_Click);
            // 
            // registeredStorageAccountsToolStripMenuItem
            // 
            this.registeredStorageAccountsToolStripMenuItem.Name = "registeredStorageAccountsToolStripMenuItem";
            this.registeredStorageAccountsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.registeredStorageAccountsToolStripMenuItem.Text = "Registered Storage Accounts";
            this.registeredStorageAccountsToolStripMenuItem.Click += new System.EventHandler(this.registeredStorageAccountsToolStripMenuItem_Click);
            // 
            // ioTHubToolStripMenuItem
            // 
            this.ioTHubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceToolStripMenuItem});
            this.ioTHubToolStripMenuItem.Name = "ioTHubToolStripMenuItem";
            this.ioTHubToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ioTHubToolStripMenuItem.Size = new System.Drawing.Size(73, 59);
            this.ioTHubToolStripMenuItem.Text = "IoT Hub";
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerToolStripMenuItem3,
            this.registeredDevicesToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // registerToolStripMenuItem3
            // 
            this.registerToolStripMenuItem3.Name = "registerToolStripMenuItem3";
            this.registerToolStripMenuItem3.Size = new System.Drawing.Size(172, 22);
            this.registerToolStripMenuItem3.Text = "Register";
            this.registerToolStripMenuItem3.Click += new System.EventHandler(this.registerToolStripMenuItem3_Click);
            // 
            // registeredDevicesToolStripMenuItem
            // 
            this.registeredDevicesToolStripMenuItem.Name = "registeredDevicesToolStripMenuItem";
            this.registeredDevicesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.registeredDevicesToolStripMenuItem.Text = "Registered Devices";
            this.registeredDevicesToolStripMenuItem.Click += new System.EventHandler(this.registeredDevicesToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(887, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(727, 316);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(646, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 27);
            this.button2.TabIndex = 24;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 405);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Form1";
            this.Text = "HOME";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem eVENTHUBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem registeredUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storageAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem registeredStorageAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ioTHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem registeredDevicesToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}

