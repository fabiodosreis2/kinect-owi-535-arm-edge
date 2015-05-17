using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace leak
{
    class KinectSkeletonDataProcessor : ISkeletonDataProcessor
    {
        private SkeletonData skeletonData;

        private List<string> handRight = new List<string>();
        private List<string> handLeft = new List<string>();
        private List<string> hipLeft = new List<string>();
        private List<string> elbowRight = new List<string>();
        private List<string> elbowLeft = new List<string>();

        public KinectSkeletonDataProcessor(SkeletonData data)
        {
            skeletonData = data;
        }

        public  KinectSkeletonDataProcessor()
        {
            
        }

        

        public void SetSkeletonData(SkeletonData data)
        {
            skeletonData = data;
        }

        Dictionary<JointID, JointCoordinate> ISkeletonDataProcessor.GetDataProcessed()
        {
            Dictionary<JointID, JointCoordinate> dictionary = new Dictionary<JointID, JointCoordinate>();
            JointCoordinate coordinate = new JointCoordinate();

            KinectJointUtils.ExtractCoordinates(skeletonData.Joints[JointID.HandRight],
                                                out coordinate.X,
                                                out coordinate.Y);
            dictionary.Add(JointID.HandRight, coordinate);

            handRight.Add(coordinate.X + ";" + coordinate.Y);
            


           //System.Console.WriteLine("HandRight X = " + coordinate.X + " Y = " + coordinate.Y);
        

            KinectJointUtils.ExtractCoordinates(skeletonData.Joints[JointID.HandLeft],
                                                out coordinate.X,
                                                out coordinate.Y);
            dictionary.Add(JointID.HandLeft, coordinate);


            handLeft.Add(coordinate.X + ";" + coordinate.Y);

            //System.Console.WriteLine("HandLeft X = " + coordinate.X + " Y = " + coordinate.Y);

            KinectJointUtils.ExtractCoordinates(skeletonData.Joints[JointID.HipLeft],
                                                out coordinate.X,
                                                out coordinate.Y);
            dictionary.Add(JointID.HipLeft, coordinate);

            hipLeft.Add(coordinate.X + ";" + coordinate.Y);

            //System.Console.WriteLine("HipLeft X = " + coordinate.X + " Y = " + coordinate.Y);

            KinectJointUtils.ExtractCoordinates(skeletonData.Joints[JointID.ElbowRight],
                                                out coordinate.X,
                                                out coordinate.Y);
            dictionary.Add(JointID.ElbowRight, coordinate);

            elbowRight.Add(coordinate.X + ";" + coordinate.Y);

            //System.Console.WriteLine("ElbowRight X = " + coordinate.X + " Y = " + coordinate.Y);

            KinectJointUtils.ExtractCoordinates(skeletonData.Joints[JointID.ElbowLeft],
                                                out coordinate.X,
                                                out coordinate.Y);
            dictionary.Add(JointID.ElbowLeft, coordinate);

            elbowLeft.Add(coordinate.X + ";" + coordinate.Y);

            //System.Console.WriteLine("ElbowLeft X = " + coordinate.X + " Y = " + coordinate.Y);
            return dictionary;
        }

        ~KinectSkeletonDataProcessor() 
        {

            /*List<string> all = new List<string>();

            all.Add("mao direita");
            all.AddRange(handRight);

            all.Add("mao esquerda");
            all.AddRange(handLeft);

            all.Add("quadril esquerdo");
            all.AddRange(hipLeft);

            all.Add("cotovelo esquerdo");
            all.AddRange(elbowLeft);

            all.Add("cotovelo direito");
            all.AddRange(elbowRight);*/

            //System.IO.File.WriteAllLines("C:\\ALL.csv", all);

        }
    }
}
