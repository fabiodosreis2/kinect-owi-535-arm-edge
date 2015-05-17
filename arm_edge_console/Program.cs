using System;
using System.Collections.Generic;
using System.Text;
using ArmEdgeRobot;

namespace ArmEdgeConsole
{
    class Program
    {
        static void ShowDemostration()
        {
            
        }

        static void Main(string[] args)
        {
            RobotArm robot = new RobotArm();
            if (args.Length <= 1)
            {
                robot.MoveToZero();
                return;
            }

            Arguments commandLine = new Arguments(args);
            if (commandLine["showdemo"] == "1")
            {
                ShowDemostration();
            }

            JointId jid = JointId.None;
            if (commandLine["joint"] == "base")
            {
                jid = JointId.Base;
            }
            else if (commandLine["joint"] == "wrist")
            {
                jid = JointId.Wrist;
            }
            else if (commandLine["joint"] == "shoulder")
            {
                jid = JointId.Shoulder;
            }
            else if (commandLine["joint"] == "elbow")
            {
                jid = JointId.Elbow;
            }

            if (jid == JointId.None)
            {
                Console.WriteLine("unknow joint");
                return;
            }

            int offset = 0;
            int angleValue = 0;
            bool angleArg = false;
            if (commandLine["offset"] != null)
            {
                offset = Convert.ToInt32(commandLine["offset"]);
            }
            else if (commandLine["angle"] != null)
            {
                angleValue = Convert.ToInt32(commandLine["angle"]);
                angleArg = true;
            }

            if (offset != 0)
            {
                robot.TurnByOffset(jid, offset);
                robot.Close();
            }
            else if (angleArg)
            {
                robot.TurnToAngle(jid, angleValue);
                robot.Close();
            }
            else
            {
                Console.WriteLine("you need to pass an angle or offset");
            }
        }
    }
}
