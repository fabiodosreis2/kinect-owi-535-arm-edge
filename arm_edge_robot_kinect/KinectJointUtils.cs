using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    class KinectJointUtils
    {
        static public void ExtractCoordinates(Microsoft.Research.Kinect.Nui.Joint joint, out int x, out int y)
        {
            float depthX = 0;
            float depthY = 0;

            // convert skeleton position to depthimage and generate two floats between 0.0 and 1.0
            Kinect.RunTime.SkeletonEngine.SkeletonToDepthImage(joint.Position, out depthX, out depthY);


            // convert to 320x240 so that GetColorPixelCoordinatesFromDepthPixel suports
            depthX = Math.Max(0, Math.Min(depthX * 320, 320));
            depthY = Math.Max(0, Math.Min(depthY * 240, 240));

            ImageViewArea imageView = new ImageViewArea();

            int tempX = 0;
            int tempY = 0;

            // convert DepthPixel values to ColorPixel values. yes!! what we need now
            Kinect.RunTime.NuiCamera.GetColorPixelCoordinatesFromDepthPixel(
                                        ImageResolution.Resolution640x480,
                                        imageView,
                                        (int)depthX,
                                        (int)depthY, 0,
                                        out tempX, out tempY);
            x = tempX;
            y = tempY;
        }
    }
}
