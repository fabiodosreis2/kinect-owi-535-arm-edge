using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    internal struct JointCoordinate
    {
        public int X;
        public int Y;
        public int Z;
    }

    internal interface ISkeletonDataProcessor
    {
        Dictionary<JointID, JointCoordinate> GetDataProcessed();
    }
}