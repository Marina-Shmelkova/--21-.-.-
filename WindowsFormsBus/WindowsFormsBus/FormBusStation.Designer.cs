namespace WindowsFormsBus
{
    partial class FormBusStation
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
            this.pictureBoxBusStation = new System.Windows.Forms.PictureBox();
            this.buttonBus = new System.Windows.Forms.Button();
            this.buttonTrolleybus = new System.Windows.Forms.Button();
            this.groupBoxStation = new System.Windows.Forms.GroupBox();
            this.OutPutBus = new System.Windows.Forms.Button();
            this.maskedTextBoxBus = new System.Windows.Forms.MaskedTextBox();
            this.labelBus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBusStation)).BeginInit();
            this.groupBoxStation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxBusStation
            // 
            this.pictureBoxBusStation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxBusStation.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBusStation.Name = "pictureBoxBusStation";
            this.pictureBoxBusStation.Size = new System.Drawing.Size(553, 450);
            this.pictureBoxBusStation.TabIndex = 0;
            this.pictureBoxBusStation.TabStop = false;
            // 
            // buttonBus
            // 
            this.buttonBus.Location = new System.Drawing.Point(699, 12);
            this.buttonBus.Name = "buttonBus";
            this.buttonBus.Size = new System.Drawing.Size(96, 42);
            this.buttonBus.TabIndex = 1;
            this.buttonBus.Text = "Припарковать автобус";
            this.buttonBus.UseVisualStyleBackColor = true;
            this.buttonBus.Click += new System.EventHandler(this.buttonSetBus_Click);
            // 
            // buttonTrolleybus
            // 
            this.buttonTrolleybus.Location = new System.Drawing.Point(699, 60);
            this.buttonTrolleybus.Name = "buttonTrolleybus";
            this.buttonTrolleybus.Size = new System.Drawing.Size(96, 40);
            this.buttonTrolleybus.TabIndex = 2;
            this.buttonTrolleybus.Text = "Припарковать троллейбус";
            this.buttonTrolleybus.UseVisualStyleBackColor = true;
            this.buttonTrolleybus.Click += new System.EventHandler(this.buttonSetTrolleybus_Click);
            // 
            // groupBoxStation
            // 
            this.groupBoxStation.Controls.Add(this.OutPutBus);
            this.groupBoxStation.Controls.Add(this.maskedTextBoxBus);
            this.groupBoxStation.Controls.Add(this.labelBus);
            this.groupBoxStation.Location = new System.Drawing.Point(679, 119);
            this.groupBoxStation.Name = "groupBoxStation";
            this.groupBoxStation.Size = new System.Drawing.Size(125, 109);
            this.groupBoxStation.TabIndex = 3;
            this.groupBoxStation.TabStop = false;
            this.groupBoxStation.Text = "Забрать транспортное средство";
            // 
            // OutPutBus
            // 
            this.OutPutBus.Location = new System.Drawing.Point(9, 79);
            this.OutPutBus.Name = "OutPutBus";
            this.OutPutBus.Size = new System.Drawing.Size(75, 23);
            this.OutPutBus.TabIndex = 2;
            this.OutPutBus.Text = "Забрать";
            this.OutPutBus.UseVisualStyleBackColor = true;
            this.OutPutBus.Click += new System.EventHandler(this.buttonTakeBus_Click);
            // 
            // maskedTextBoxBus
            // 
            this.maskedTextBoxBus.Location = new System.Drawing.Point(51, 53);
            this.maskedTextBoxBus.Name = "maskedTextBoxBus";
            this.maskedTextBoxBus.Size = new System.Drawing.Size(48, 20);
            this.maskedTextBoxBus.TabIndex = 1;
            // 
            // labelBus
            // 
            this.labelBus.AutoSize = true;
            this.labelBus.Location = new System.Drawing.Point(6, 53);
            this.labelBus.Name = "labelBus";
            this.labelBus.Size = new System.Drawing.Size(39, 13);
            this.labelBus.TabIndex = 0;
            this.labelBus.Text = "Место";
            // 
            // FormBusStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxStation);
            this.Controls.Add(this.buttonTrolleybus);
            this.Controls.Add(this.buttonBus);
            this.Controls.Add(this.pictureBoxBusStation);
            this.Name = "FormBusStation";
            this.Text = "FormParking";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBusStation)).EndInit();
            this.groupBoxStation.ResumeLayout(false);
            this.groupBoxStation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBusStation;
        private System.Windows.Forms.Button buttonBus;
        private System.Windows.Forms.Button buttonTrolleybus;
        private System.Windows.Forms.GroupBox groupBoxStation;
        private System.Windows.Forms.Button OutPutBus;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxBus;
        private System.Windows.Forms.Label labelBus;
    }
}