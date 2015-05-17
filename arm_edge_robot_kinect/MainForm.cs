using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace leak
{
    public partial class MainForm : Form
    {
        private Kinect _kinect;
        private Recorder _recorder = new Recorder();
        private RobotArmEdgeControler _robotControler = new RobotArmEdgeControler();
        private HandsSkeletonObserver _handsSkeletonObserver;
        private GripperSkeletonObserver _gripperSkeletonObserver;
        
        public MainForm()
        {
            InitializeKinectSensor();
            InitializeComponent();
        }

        private void InitializeKinectSensor()
        {
            _kinect = new Kinect();
            _robotControler.Recorder = _recorder;
            _handsSkeletonObserver = new HandsSkeletonObserver(_robotControler);
            _gripperSkeletonObserver = new GripperSkeletonObserver(_robotControler);
            _kinect.AddSkeletonObserver(_handsSkeletonObserver);
            _kinect.AddSkeletonObserver(_gripperSkeletonObserver);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblKinectCountVal.Text = _kinect.KinectCount.ToString();
            lblElevationAngleVal.Text = _kinect.CameraElevationAngle.ToString();
            trackBarAngle.Value = _kinect.CameraElevationAngle;
            trackBarAngle.Visible = true;
        }

        private void trackBarAngle_MouseUp(object sender, MouseEventArgs e)
        {
            _kinect.CameraElevationAngle = trackBarAngle.Value;
            lblElevationAngleVal.Text = trackBarAngle.Value.ToString();
        }

        private void trackBarAngle_Scroll(object sender, EventArgs e)
        {
            lblElevationAngleVal.Text = trackBarAngle.Value.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _robotControler.Stop();
            _recorder.Save("rkinect.txt");
        }

        void ColorizeOnRecording()
        {
            btnRecord.BackColor = Color.Red;
            btnStop.BackColor = Color.LightGray;
            btnRewind.BackColor = Color.LightGray;
            btnPlay.BackColor = Color.LightGray;
        }

        void ColorizeOnNonRecording()
        {
            btnRecord.BackColor = Color.LightGray;
            btnStop.BackColor = Color.LightGray;
            btnRewind.BackColor = Color.LightGray;
            btnPlay.BackColor = Color.LightGray;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete("rkinect.txt");
            _recorder.Record();
            ColorizeOnRecording();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _robotControler.Stop();
            _recorder.Stop();
            _recorder.Save("rkinect.txt");
            _kinect.ShouldStop = true;
            ColorizeOnNonRecording();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText("rkinect.txt");
            Player player = new Player(_robotControler, json);
            player.Play();
            ColorizeOnNonRecording();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            //lblSkeletonStatus.Text = _kinect.SkeletonStatus;
        }

        private void btnRewind_Click(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText("rkinect.txt");
            Player player = new Player(_robotControler, json);
            player.Rewind();
            ColorizeOnNonRecording();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _kinect.ShouldStop = false;
            ColorizeOnNonRecording();
        }

    }
}
