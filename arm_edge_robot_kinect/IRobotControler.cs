using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leak
{
    public enum RobotControlerStatus
    {
        Stoped,
        Started
    }

    interface IRobotControler
    {
        void Move(ArmEdgeRobot.RobotJoint joint, ArmEdgeRobot.TurnDirection dir);
        void OpenGripper();
        void CloseGripper();
        void Stop();
        void Start();
        RobotControlerStatus Status();
    }
}
