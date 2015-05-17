using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ArmEdgeRobot;

namespace leak
{
    public struct RobotDirection
    {
        public RobotJoint joint;
        public TurnDirection dir;
        public bool Equals(RobotDirection jDir)
        {
            return this.joint == jDir.joint && this.dir == jDir.dir;
        }
    }

    class RobotArmEdgeControler : IRobotControler
    {
        private RobotControlerStatus robotStatus = RobotControlerStatus.Stoped;
        private ArmEdgeRobot.RobotArmEdge robot;
        private RobotDirection lastDirection;

        private bool isOpen = false;
        private static bool interruptThread = false;
        private Thread slowMoveThread;

        private Recorder recorder;

        public Recorder Recorder
        {
            set
            {
                recorder = value;
            }
            get { return recorder; }
        }
        
        public RobotArmEdgeControler()
        {
            robot = new RobotArmEdge();
        }

        private void MoveOpenGripperThread(object param)
        {
            RobotDirection jointDir = (RobotDirection) param;
            while (!interruptThread)
            {
                robot.TurnByDirection(jointDir.joint, jointDir.dir);
                Thread.Sleep(220);
                robot.Stop();
                Thread.Sleep(250);
            }
        }

        public void Move(RobotJoint joint, TurnDirection dir)
        {
            robotStatus = RobotControlerStatus.Started;
            RobotDirection currDirection;
            currDirection.joint = joint;
            currDirection.dir = dir;

            if (!lastDirection.Equals(currDirection))
            {
                bool isUp = (joint == RobotJoint.Elbow) && (dir == TurnDirection.Negative);
                if (slowMoveThread != null)
                {
                    interruptThread = true;
                    slowMoveThread.Join(500);
                }  
                if (isOpen && !isUp)
                {
                    RobotDirection threadParam;
                    threadParam.joint = joint;
                    threadParam.dir = dir;
                  
                    interruptThread = false;                    
                    slowMoveThread = new Thread(this.MoveOpenGripperThread) { IsBackground = true };
                    slowMoveThread.Start(threadParam);
                }
                else
                {
                    robot.TurnByDirection(joint, dir);
                }
                lastDirection = currDirection;
                recorder.RegisterMove(currDirection, isOpen);
            }

        }

        public void OpenGripper()
        {
            if (robotStatus == RobotControlerStatus.Stoped)
            {
                if (isOpen)
                {
                    return;
                }
                robot.OpenGripper(true);
                recorder.RegisterGripper(true);
                isOpen = true;
            }
        }

        public void CloseGripper()
        {
            if (robotStatus == RobotControlerStatus.Stoped)
            {
                if (!isOpen)
                {
                    return;
                }
                robot.OpenGripper(false);
                recorder.RegisterGripper(false);
                isOpen = false;
            }
        }

        public void Stop()
        {
            if (robotStatus == RobotControlerStatus.Stoped)
            {
                return;
            }

            interruptThread = true;
            robotStatus = RobotControlerStatus.Stoped;
            robot.Stop();
            recorder.RegisterStop();
        }


        public void Start()
        {
            robotStatus = RobotControlerStatus.Started;
        }

        public RobotControlerStatus Status()
        {
            return robotStatus;
        }
    }
}
