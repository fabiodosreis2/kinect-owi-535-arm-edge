using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    class GripperSkeletonObserver : ISkeletonObserver
    {
        private IRobotControler robotControler;

        public GripperSkeletonObserver(IRobotControler controler)
        {
            robotControler = controler;
        }

        public void Tracking(ISkeletonDataProcessor processor)
        {
            if (processor == null) return;

            Dictionary<JointID, JointCoordinate> j = processor.GetDataProcessed();
            int handRightX = j[JointID.HandRight].X;
            int handRightY = j[JointID.HandRight].Y;

            int handLeftX = j[JointID.HandLeft].X;
            int handLeftY = j[JointID.HandLeft].Y;

            int elbowRightX = j[JointID.ElbowRight].X;
            int elbowRightY = j[JointID.ElbowRight].Y;

            int elbowLeftX = j[JointID.ElbowLeft].X;
            int elbowLeftY = j[JointID.ElbowLeft].Y;


            if (handRightX > elbowRightX && handLeftX < elbowLeftX)
            {
                int diffRightHandElbowX = Math.Abs(handRightX - elbowRightX);

                int diffLeftHandElbowX = Math.Abs(handLeftX - elbowLeftX);

                if (diffRightHandElbowX >= 40 && diffLeftHandElbowX >= 40)
                {
                    robotControler.OpenGripper();
                }
            }
            else if (handRightX < elbowRightX && handLeftX > elbowLeftX)
            {
                int diffRigthLeftHandsX = Math.Abs(handRightX - handLeftX);
                if (diffRigthLeftHandsX < 50)
                {
                    robotControler.CloseGripper();
                }
            }
        }
    }
}
