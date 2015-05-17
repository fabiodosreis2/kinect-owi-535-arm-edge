namespace leak
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblKinectValue = new System.Windows.Forms.Label();
            this.lblKinectCountVal = new System.Windows.Forms.Label();
            this.lblElevationAngle = new System.Windows.Forms.Label();
            this.lblElevationAngleVal = new System.Windows.Forms.Label();
            this.trackBarAngle = new System.Windows.Forms.TrackBar();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblSkeletonStatus = new System.Windows.Forms.Label();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKinectValue
            // 
            this.lblKinectValue.AutoSize = true;
            this.lblKinectValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKinectValue.Location = new System.Drawing.Point(21, 253);
            this.lblKinectValue.Name = "lblKinectValue";
            this.lblKinectValue.Size = new System.Drawing.Size(61, 20);
            this.lblKinectValue.TabIndex = 1;
            this.lblKinectValue.Text = "Kinects";
            // 
            // lblKinectCountVal
            // 
            this.lblKinectCountVal.AutoSize = true;
            this.lblKinectCountVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKinectCountVal.Location = new System.Drawing.Point(22, 282);
            this.lblKinectCountVal.Name = "lblKinectCountVal";
            this.lblKinectCountVal.Size = new System.Drawing.Size(16, 18);
            this.lblKinectCountVal.TabIndex = 2;
            this.lblKinectCountVal.Text = "0";
            // 
            // lblElevationAngle
            // 
            this.lblElevationAngle.AutoSize = true;
            this.lblElevationAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElevationAngle.Location = new System.Drawing.Point(21, 311);
            this.lblElevationAngle.Name = "lblElevationAngle";
            this.lblElevationAngle.Size = new System.Drawing.Size(175, 20);
            this.lblElevationAngle.TabIndex = 3;
            this.lblElevationAngle.Text = "Camera elevation angle";
            // 
            // lblElevationAngleVal
            // 
            this.lblElevationAngleVal.AutoSize = true;
            this.lblElevationAngleVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElevationAngleVal.Location = new System.Drawing.Point(22, 343);
            this.lblElevationAngleVal.Name = "lblElevationAngleVal";
            this.lblElevationAngleVal.Size = new System.Drawing.Size(16, 18);
            this.lblElevationAngleVal.TabIndex = 4;
            this.lblElevationAngleVal.Text = "0";
            // 
            // trackBarAngle
            // 
            this.trackBarAngle.AutoSize = false;
            this.trackBarAngle.LargeChange = 1;
            this.trackBarAngle.Location = new System.Drawing.Point(44, 334);
            this.trackBarAngle.Maximum = 27;
            this.trackBarAngle.Minimum = -27;
            this.trackBarAngle.Name = "trackBarAngle";
            this.trackBarAngle.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAngle.Size = new System.Drawing.Size(45, 90);
            this.trackBarAngle.TabIndex = 5;
            this.trackBarAngle.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAngle.Scroll += new System.EventHandler(this.trackBarAngle_Scroll);
            this.trackBarAngle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarAngle_MouseUp);
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(368, 392);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 6;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(368, 363);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(368, 306);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // lblSkeletonStatus
            // 
            this.lblSkeletonStatus.AutoSize = true;
            this.lblSkeletonStatus.Location = new System.Drawing.Point(56, 390);
            this.lblSkeletonStatus.Name = "lblSkeletonStatus";
            this.lblSkeletonStatus.Size = new System.Drawing.Size(0, 13);
            this.lblSkeletonStatus.TabIndex = 9;
            // 
            // btnRewind
            // 
            this.btnRewind.Location = new System.Drawing.Point(368, 277);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(75, 23);
            this.btnRewind.TabIndex = 10;
            this.btnRewind.Text = "Rewind";
            this.btnRewind.UseVisualStyleBackColor = true;
            this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(368, 333);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(277, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 197);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 13;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 29);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(266, 196);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 443);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRewind);
            this.Controls.Add(this.lblSkeletonStatus);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.trackBarAngle);
            this.Controls.Add(this.lblElevationAngleVal);
            this.Controls.Add(this.lblElevationAngle);
            this.Controls.Add(this.lblKinectCountVal);
            this.Controls.Add(this.lblKinectValue);
            this.Name = "MainForm";
            this.Text = "OWI-535 Robotic Arm Edge Kinect Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKinectValue;
        private System.Windows.Forms.Label lblKinectCountVal;
        private System.Windows.Forms.Label lblElevationAngle;
        private System.Windows.Forms.Label lblElevationAngleVal;
        private System.Windows.Forms.TrackBar trackBarAngle;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblSkeletonStatus;
        private System.Windows.Forms.Button btnRewind;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

