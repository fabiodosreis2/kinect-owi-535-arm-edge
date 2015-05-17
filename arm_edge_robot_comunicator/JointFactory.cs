using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmEdgeRobot
{
    public class JointFactory
    {
        private JointFactory()
        {
        }

        public static Joint Build(RobotJoint jid)
        {
            Joint joint = null;
            switch (jid)
            {
                case RobotJoint.Base:
                {
                    joint = new Joint(jid, ArmEdgeComunicator.Instance(), 15000, 135, -135);
                    break;
                }
                case RobotJoint.Elbow:
                {
                    joint = new Joint(jid, ArmEdgeComunicator.Instance(), 15000, 135, -120);
                    break;
                }
                case RobotJoint.Shoulder:
                {
                    joint = new Joint(jid, ArmEdgeComunicator.Instance(), 13000, 110, -90);
                    break;
                }
                case RobotJoint.Wrist:
                {
                    joint = new Joint(jid, ArmEdgeComunicator.Instance(), 7000, 60, -60);
                    break;
                }
            }
            return joint;
        }
    }
}
