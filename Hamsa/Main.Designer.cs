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
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkShowCamera = new System.Windows.Forms.CheckBox();
            this.comFingersList = new System.Windows.Forms.ComboBox();
            this.lblCursorFinger = new System.Windows.Forms.Label();
            this.comLMB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CameraBox
            // 
            this.CameraBox.Location = new System.Drawing.Point(-2, -1);
            this.CameraBox.Name = "CameraBox";
            this.CameraBox.Size = new System.Drawing.Size(640, 480);
            this.CameraBox.TabIndex = 0;
            this.CameraBox.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Aharoni", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(137, 185);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(358, 95);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Hamsa";
            // 
            // chkShowCamera
            // 
            this.chkShowCamera.AutoSize = true;
            this.chkShowCamera.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chkShowCamera.Location = new System.Drawing.Point(645, 253);
            this.chkShowCamera.Name = "chkShowCamera";
            this.chkShowCamera.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowCamera.Size = new System.Drawing.Size(135, 27);
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
            this.comFingersList.Location = new System.Drawing.Point(763, 188);
            this.comFingersList.Name = "comFingersList";
            this.comFingersList.Size = new System.Drawing.Size(174, 21);
            this.comFingersList.TabIndex = 4;
            this.comFingersList.SelectedIndexChanged += new System.EventHandler(this.comFingersList_SelectedIndexChanged);
            // 
            // lblCursorFinger
            // 
            this.lblCursorFinger.AutoSize = true;
            this.lblCursorFinger.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCursorFinger.Location = new System.Drawing.Point(645, 188);
            this.lblCursorFinger.Name = "lblCursorFinger";
            this.lblCursorFinger.Size = new System.Drawing.Size(114, 23);
            this.lblCursorFinger.TabIndex = 6;
            this.lblCursorFinger.Text = "Cursor Finger";
            // 
            // comLMB
            // 
            this.comLMB.FormattingEnabled = true;
            this.comLMB.Items.AddRange(new object[] {
            "Thumb",
            "Index",
            "Middle",
            "Ring",
            "Pinky"});
            this.comLMB.Location = new System.Drawing.Point(763, 224);
            this.comLMB.Name = "comLMB";
            this.comLMB.Size = new System.Drawing.Size(174, 21);
            this.comLMB.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(645, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "LMB Finger";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 480);
            this.Controls.Add(this.CameraBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comLMB);
            this.Controls.Add(this.lblCursorFinger);
            this.Controls.Add(this.comFingersList);
            this.Controls.Add(this.chkShowCamera);
            this.Controls.Add(this.lblTitle);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CameraBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkShowCamera;
        private System.Windows.Forms.ComboBox comFingersList;
        private System.Windows.Forms.Label lblCursorFinger;
        private System.Windows.Forms.ComboBox comLMB;
        private System.Windows.Forms.Label label1;
    }
}

