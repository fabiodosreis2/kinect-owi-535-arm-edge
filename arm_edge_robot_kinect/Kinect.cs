using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    class Kinect
    {
        private static Microsoft.Research.Kinect.Nui.Runtime _runTime;
        private readonly int _kinectCount = 0;
        private int _cameraElevationAngle = 0;
        private string _instanceName;
        private string _kinectStatus;
        private readonly List<ISkeletonObserver> _skeletonObservers = new List<ISkeletonObserver>();
        private string _skeletonStatus;

        public bool ShouldStop = false;

        public int KinectCount
        {
            get { return _kinectCount; }
        }

        public string SkeletonStatus
        {
            get { return _skeletonStatus; }
        }

        public int CameraElevationAngle
        {
            set 
            { 
                _cameraElevationAngle = value;
                _runTime.NuiCamera.ElevationAngle = _cameraElevationAngle;
            }

            get { return _cameraElevationAngle; }

        }

        public static Microsoft.Research.Kinect.Nui.Runtime RunTime
        {
            get { return _runTime; }
        }

        public void AddSkeletonObserver(ISkeletonObserver observer)
        {
            _skeletonObservers.Add(observer);
        }

        public Kinect()
        {
            _kinectCount = Microsoft.Research.Kinect.Nui.Runtime.Kinects.Count;
            if (_kinectCount <= 0) return;

            _runTime = Microsoft.Research.Kinect.Nui.Runtime.Kinects[0];
            _instanceName = _runTime.InstanceName;
            _kinectStatus = _runTime.Status.ToString();
            _runTime.Initialize(RuntimeOptions.UseSkeletalTracking);
            _runTime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(OnSkeletonFrameReady);
            _cameraElevationAngle = _runTime.NuiCamera.ElevationAngle;

            _runTime.SkeletonEngine.TransformSmooth = true;
            var smoothparameters = new TransformSmoothParameters
                                    {
                                        Smoothing = 0.7f,
                                        Correction = 0.3f,
                                        Prediction = 0.4f,
                                        JitterRadius = 1.0f,
                                        MaxDeviationRadius = 0.5f
                                    };
            _runTime.SkeletonEngine.SmoothParameters = smoothparameters;
        }

        private KinectSkeletonDataProcessor kinectSkeletonProc = new KinectSkeletonDataProcessor();

        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (ShouldStop)
            {
                return;
            }
            SkeletonFrame allSkeletons = e.SkeletonFrame;
            SkeletonData skeletonData = null;

            foreach (SkeletonData skeleton in allSkeletons.Skeletons)
            {
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    skeletonData = skeleton;
                    break;
                }
            }

            if (skeletonData != null)
            {
                kinectSkeletonProc.SetSkeletonData(skeletonData);
                _skeletonStatus = "skeleton is ok";
                foreach (var skeletonObserver in _skeletonObservers)
                {
                    skeletonObserver.Tracking(kinectSkeletonProc);
                }
            }
            else
            {
                _skeletonStatus = "skeleton unknown";
            }

        }
    }
}
