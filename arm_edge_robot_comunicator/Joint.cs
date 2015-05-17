using System;
using System.IO;

namespace ArmEdgeRobot
{
    public class Joint
    {
        private const String JointInfoExt = "ji.txt";
        private const int MinTime = 200;
        private readonly RobotJoint _jointId;
        private readonly ArmEdgeComunicator _comunicator;
        private int _rotationTime;
        private int _posLimit;
        private int _negLimit;
        private int _currAngle;
        private float _rotRate;
        
        public int RotationTime
        {
            get { return _rotationTime; }
            set { _rotationTime = value; }
        }

        public int PosLimit
        {
            get { return _posLimit; }
            set { _posLimit = value; }
        }

        public int NegLimit
        {
            get { return _negLimit; }
            set { _negLimit = value; }
        }

        public int CurrAngle
        {
            get { return _currAngle; }
        }

        public String Name
        {
            get { return Convert.ToString(_jointId).ToLower(); }
        }

        public Joint(RobotJoint id, ArmEdgeComunicator ac)
        {
            _jointId = id;
            _comunicator = ac;

            ReadJointInfo(Name + JointInfoExt);
            CheckInfo();
        }

        public Joint(RobotJoint id, ArmEdgeComunicator ac,
                     int rotationTime, int negLimit, int posLimit)
        {
            _jointId = id;
            _comunicator = ac;

            // set
            _rotationTime = rotationTime;
            _negLimit = negLimit;
            _posLimit = posLimit;
            ReadJointInfo(Name + JointInfoExt);
            CheckInfo();
        }

        public void SaveState()
        {
            WriteJointInfo(Name + JointInfoExt);
        }

        public void TurnByOffset(int offsetAngle)
        {
            int newAngle = WithinLimits(_currAngle + offsetAngle);
            TimedAngleTurn(newAngle);
        }

        private int WithinLimits(int angle)
        {
            if (angle >= _posLimit)
            {
                angle = _posLimit - 1;
            }
            else if (angle <= _negLimit)
            {
                angle = _negLimit + 1;
            }
            return angle;
        }

        private void TimedAngleTurn(int angle)
        {
            int offsetAngle = angle - _currAngle;
            if (offsetAngle < 0)
            {
                int time = Convert.ToInt32(Math.Round(-offsetAngle/_rotRate));
                if (time > MinTime)
                {
                    _comunicator.Turn(_jointId, TurnDirection.Negative, time);
                    _currAngle = angle;
                }
            }
            else
            {
                int time = Convert.ToInt32(Math.Round(offsetAngle/_rotRate));
                if (time > MinTime)
                {
                    _comunicator.Turn(_jointId, TurnDirection.Positive, time);
                    _currAngle = angle;
                }
            }
        }

        public void TurnByDirection(TurnDirection dir)
        {
            _comunicator.Stop();
            _comunicator.Turn(_jointId, dir);
        }

        public void TurnToAngle(int angle)
        {
           TimedAngleTurn(WithinLimits(angle));
        }

        public bool IsInRange(int angle)
        {
            return ((angle > _negLimit) && (angle < _posLimit));
        }

        private void CheckInfo()
        {
            // check rotation time 
            if (_rotationTime == 0)
            {
                throw new Exception("Positive rotation time cannot be 0");
            }

            if (_rotationTime < 0)
            {
                _rotationTime = -_rotationTime;
            }

            if (_posLimit == 0)
            {
                throw new Exception("Positive limit cannot be 0");
            }

            if (_posLimit < 0)
            {
                _posLimit = -_posLimit;
            }

            if (_negLimit == 0)
            {
                throw new Exception("Negative limit cannot be 0");
            }
            
            if (_negLimit > 0)
            {
                _negLimit = -_negLimit; // don't negative value
            }

            _rotRate = (float)(_posLimit - _negLimit)/_rotationTime;

            // check current angle 
            if ((_currAngle >= _posLimit) || (_currAngle <= _negLimit))
            {
                _currAngle = 0;
            }
        }

        /* Joint info format
         * rotateTime=<rotationTime int>
         * posLimit=<poslimit int>
         * negLimit=<neglimit int>
         * currAngle=<currAngle int>
         */
        private void ReadJointInfo(String filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }
            TextReader textFile = new StreamReader(filename);
            // _rotationTime
            String line = textFile.ReadLine();
            if (line == null)
                return;
            String[] lineToks = line.Split('=');
            _rotationTime = Convert.ToInt32(lineToks[1]);

            // _posLimit
            line = textFile.ReadLine();
            if (line == null)
                return;
            lineToks = line.Split('=');
            _posLimit = Convert.ToInt32(lineToks[1]);

            // _negLimit
            line = textFile.ReadLine();
            if (line == null)
                return;
            lineToks = line.Split('=');
            _negLimit = Convert.ToInt32(lineToks[1]);

            // _currAngle
            line = textFile.ReadLine();
            if (line == null)
                return;
            lineToks = line.Split('=');
            _currAngle = Convert.ToInt32(lineToks[1]);
            textFile.Close();
        }

        private void WriteJointInfo(string fileName)
        {
            TextWriter file = new StreamWriter(fileName);
            file.WriteLine("rotateTime=" + _rotationTime);
            file.WriteLine("posLimit=" + _posLimit);
            file.WriteLine("negLimit=" + _negLimit);
            file.WriteLine("currAngle=" + _currAngle);
            file.Close();
        }
  

    }
}
