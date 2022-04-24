namespace Hamsa
{
    partial class Main
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
            this.CameraBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowCamera = new System.Windows.Forms.CheckBox();
            this.comFingersList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CameraBox
            // 
            this.CameraBox.Location = new System.Drawing.Point(-2, -1);
            this.CameraBox.Name = "CameraBox";
            this.CameraBox.Size = new System.Drawing.Size(594, 453);
            this.CameraBox.TabIndex = 0;
            this.CameraBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hamsa";
            // 
            // chkShowCamera
            // 
            this.chkShowCamera.AutoSize = true;
            this.chkShowCamera.Location = new System.Drawing.Point(598, 260);
            this.chkShowCamera.Name = "chkShowCamera";
            this.chkShowCamera.Size = new System.Drawing.Size(92, 17);
            this.chkShowCamera.TabIndex = 2;
            this.chkShowCamera.Text = "Show Camera";
            this.chkShowCamera.UseVisualStyleBackColor = true;
            this.chkShowCamera.CheckedChanged += new System.EventHandler(this.chkShowCamera_CheckedChanged);
            // 
            // comFingersList
            // 
            this.comFingersList.FormattingEnabled = true;
            this.comFingersList.Items.AddRange(new object[] {
            "Thumb",
            "Index",
            "Middle",
            "Ring",
            "Pinky"});
            this.comFingersList.Location = new System.Drawing.Point(599, 107);
            this.comFingersList.Name = "comFingersList";
            this.comFingersList.Size = new System.Drawing.Size(174, 21);
            this.comFingersList.TabIndex = 4;
            this.comFingersList.SelectedIndexChanged += new System.EventHandler(this.comFingersList_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comFingersList);
            this.Controls.Add(this.chkShowCamera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CameraBox);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CameraBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowCamera;
        private System.Windows.Forms.ComboBox comFingersList;
    }
}

