using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    interface ISkeletonObserver
    {
        void Tracking(ISkeletonDataProcessor processor);
    }
}
