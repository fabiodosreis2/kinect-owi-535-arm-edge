using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmEdgeRobot;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    class HandsSkeletonObserver : ISkeletonObserver
    {
        private readonly IRobotControler _robotControler;

        private JointCoordinate _lastRightCoords;
        private JointCoordinate _lastLeftCoords;

        private bool isFirstTrack = true;

        public HandsSkeletonObserver(IRobotControler robotControler)
        {
            _robotControler = robotControler;
        }

        private bool ShouldStop(JointCoordinate leftHip,
                                JointCoordinate rightHand,
                                JointCoordinate leftHand)
        {
            if (leftHand.Y < leftHip.Y)
            {
                return true;
            }

            if (rightHand.Y >= (leftHip.Y + 50))
            {
                return true;
            }
            return false;
        }

        public void Tracking(ISkeletonDataProcessor data)
        {
            if (data == null)
            {
                _robotControler.Stop();
                isFirstTrack = true;
                return;
            }

            Dictionary<JointID, JointCoordinate> j = data.GetDataProcessed();
           
            JointCoordinate currRightHand = j[JointID.HandRight];
            JointCoordinate currLeftHand = j[JointID.HandLeft];
            JointCoordinate currLeftHip = j[JointID.HipLeft];

            if (isFirstTrack)
            {
                _lastLeftCoords = currLeftHand;
                _lastRightCoords = currRightHand;
                isFirstTrack = false;
            }

            if (ShouldStop(currLeftHip, currRightHand, currLeftHand))
            {
                _robotControler.Stop();
                isFirstTrack = true;
                return;
            }

            _robotControler.Start();

            int rightHandDiffX = Math.Abs(_lastRightCoords.X - currRightHand.X);
            int rightHandDiffY = Math.Abs(_lastRightCoords.Y - currRightHand.Y);

            if (rightHandDiffX > 2 || rightHandDiffY > 2)
            {
                if (rightHandDiffX > rightHandDiffY)
                {
                    if (currRightHand.X > _lastRightCoords.X)
                    {
                        // move right
                        _robotControler.Move(RobotJoint.Base, TurnDirection.Positive);
                    }
                    else
                    {
                        // move left
                        _robotControler.Move(RobotJoint.Base, TurnDirection.Negative);
                    }
                }
                else
                {
                    if (currRightHand.Y > _lastRightCoords.Y)
                    {
                        // move down
                        _robotControler.Move(RobotJoint.Elbow, TurnDirection.Positive);
                    }
                    else
                    {
                        // move up
                        _robotControler.Move(RobotJoint.Elbow, TurnDirection.Negative);
                    }
                }
            }

            _lastRightCoords = currRightHand;
            _lastLeftCoords = currLeftHand;
        }
    }
}
