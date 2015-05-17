using System;
using System.Linq;

namespace ArmEdgeRobot
{
    public class RobotArmEdge
    {
        private Joint _base, _wrist, _shoulder, _elbow;
        private Joint[] _joints;
        
        public RobotArmEdge()
        {
            _base = JointFactory.Build(RobotJoint.Base);
            _wrist = JointFactory.Build(RobotJoint.Wrist);
            _shoulder = JointFactory.Build(RobotJoint.Shoulder);
            _elbow = JointFactory.Build(RobotJoint.Elbow);

            _joints = new Joint[4];
            _joints[0] = _base;
            _joints[1] = _wrist;
            _joints[2] = _shoulder;
            _joints[3] = _elbow;
        }

        public void SaveState()
        {
            foreach (Joint joint in _joints)
            {
                joint.SaveState();
            }
        }

        public void OpenGripper(bool open)
        {
            ArmEdgeComunicator.Instance().OpenGripper(open);
        }

        public void Stop()
        {
            ArmEdgeComunicator.Instance().Stop();
        }

        public void MoveToZero()
        {
            var jointReversed = _joints.Reverse();
            foreach (var joint in jointReversed)
            {
                joint.TurnToAngle(0);
            }
        }

        public  void TurnByDirection(RobotJoint jid, TurnDirection dir)
        {
            if (jid == RobotJoint.Wrist)
                _wrist.TurnByDirection(dir);
            else if (jid == RobotJoint.Elbow)
                _elbow.TurnByDirection(dir);
            else if (jid == RobotJoint.Shoulder)
                _shoulder.TurnByDirection(dir);
            else if (jid == RobotJoint.Base)
                _base.TurnByDirection(dir);
            else
                throw new Exception("Unknown joint ID: " + jid);
        }

        public void TurnByOffset(RobotJoint jid, int offsetAngle)
        {
            if (jid == RobotJoint.Wrist)
                _wrist.TurnByOffset(offsetAngle);
            else if (jid == RobotJoint.Elbow)
                _elbow.TurnByOffset(offsetAngle);
            else if (jid == RobotJoint.Shoulder)
                _shoulder.TurnByOffset(offsetAngle);
            else if (jid == RobotJoint.Base)
                _base.TurnByOffset(offsetAngle);
            else
                throw new Exception("Unknown joint ID: " + jid);
        }

        public void TurnToAngle(RobotJoint jid, int angle)
        {
            if (jid ==  RobotJoint.Wrist)
              _wrist.TurnToAngle(angle);
            else if (jid == RobotJoint.Elbow)
              _elbow.TurnToAngle(angle);
            else if (jid == RobotJoint.Shoulder)
              _shoulder.TurnToAngle(angle);
            else if (jid == RobotJoint.Base)
              _base.TurnToAngle(angle);
            else
               throw new Exception("Unknown joint ID: " + jid);
        }


        public void Wait(int ms)
        {
            ArmEdgeComunicator.Wait(ms);
        }


        public void ShowAngles()
        {
            Console.WriteLine("Current Angles:");
            foreach (var j in _joints)
            {
                Console.WriteLine( "  " + j.Name + ": " + j.CurrAngle);
            }
        }
    }
}
