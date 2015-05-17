using System;
using System.Runtime.InteropServices;
using System.Threading;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace ArmEdgeRobot
{
    public enum TurnDirection
    {
        None,
        Positive,
        Negative
    }

    public class ArmEdgeComunicator
    {
        private const short VendorId = 0x1267;
        private const short ProductId = 0x0;
        // the IDs were obtained by looking at the robot arm using USBDeview

        private const int GripperPeriod = 1500; //ms time to open/close
        private int _lastGripperMove = -1;

        private byte[] _commands = new byte[3];
        private IntPtr _commandsPtr;
        private UsbSetupPacket _setupPacket;
        private UsbDeviceFinder _usbDevFinder = new UsbDeviceFinder(VendorId, ProductId);
        private UsbDevice _usbDevice; // used to communicate with the USB device

        // start state for the light
        private bool _isLightOn;

        private static ArmEdgeComunicator _comunicatorInst;

        private ArmEdgeComunicator()
        {
            _usbDevice = UsbDevice.OpenUsbDevice(_usbDevFinder);
            _setupPacket = new UsbSetupPacket((byte) UsbRequestType.TypeVendor, 0x06, 0x100, 0, (short)_commands.Length);
            _commandsPtr = Marshal.AllocHGlobal(_commands.Length);
        }

        public static ArmEdgeComunicator Instance()
        {
            if (_comunicatorInst == null)
            {
                _comunicatorInst = new ArmEdgeComunicator();
            }
            return _comunicatorInst;
        }

        public void Close()
        {
            if (_usbDevice != null)
            {
                _usbDevice.Close();
            }
        }

        // ------------------------------ command ops --------------------------
        /*
        First byte:
        Gripper close == 0x01     Gripper open == 0x02
        */

        public void OpenGripper(bool isOpen)
        {
            if (isOpen)
            {
                if (_lastGripperMove != 1)
                {
                    SendCommand(0x02, 0x00, GripperPeriod);
                    _lastGripperMove = 1;
                }
            }
            else
            {
                if (_lastGripperMove != 0)
                {
                    SendCommand(0x01, 0x00, GripperPeriod);
                    _lastGripperMove = 0;
                }
            }
        }


        public void SetLight(bool turnOn)
        {
            SendControl(0x00, 0x00, GetLightVal(turnOn));
            _isLightOn = turnOn;
        }

        private static byte GetLightVal(bool lightStatus)
        // light on/off
        {
            return (byte) ((lightStatus) ? 0x01 : 0x00);
        }

        public void Turn(RobotJoint jid, TurnDirection dir)
        {
            Turn(jid, dir, -1);
        }

        public void Turn(RobotJoint jid, TurnDirection dir, int period)
        /* First byte:
        Wrist forwards == 0x04          Wrist backwards == 0x08
        Elbow forwards == 0x10          Elbow backwards == 0x20
        Shoulder forwards == 0x40       Shoulder backwards == 0x80
   
        Second byte:
        Base rotate right == 0x01  Base rotate left == 0x02
        */
        {
            byte opCode1 = 0x00;
            byte opCode2 = 0x00;

            if (jid == RobotJoint.Base)
                opCode2 = (byte)((dir == TurnDirection.Positive) ? 0x01 : 0x02);
            else if (jid == RobotJoint.Shoulder)
                opCode1 = (byte)((dir == TurnDirection.Positive) ? 0x80 : 0x40);
            else if (jid == RobotJoint.Elbow)
                opCode1 = (byte)((dir == TurnDirection.Positive) ? 0x20 : 0x10);
            else if (jid == RobotJoint.Wrist)
                opCode1 = (byte)((dir == TurnDirection.Positive) ? 0x08 : 0x04);

            if (period < 0)
            {
                period = 0;
            }
            SendCommand(opCode1, opCode2, period);
        }


        private void SendCommand(byte opCode1, byte opCode2, int period)
        // execute the operation for period millisecs
        {
            byte opCode3 = GetLightVal(_isLightOn); // third byte == light on/off
            if (_usbDevice == null) return;
            SendControl(opCode1, opCode2, opCode3);
            if (period > 0)
            {
                Wait(period);
                SendControl(0, 0, opCode3); // stop arm
            }
        }

        public void Stop()
        {
            byte opCode3 = GetLightVal(_isLightOn);
            SendControl(0, 0, opCode3); // stop arm
        }

        // end of sendCommand()


        private void SendControl(byte opCode1, byte opCode2, byte opCode3)
        // send a USB control transfer
        {
            _commands.SetValue(opCode1, 0);
            _commands.SetValue(opCode2, 1);
            _commands.SetValue(opCode3, 2);
            try
            {                
                Marshal.Copy(_commands, 0, _commandsPtr, _commands.Length);
                int lenghtTransfered;
                _usbDevice.ControlTransfer(ref _setupPacket, _commandsPtr, _commands.Length,
                                                      out lenghtTransfered);
            }
            catch (UsbException)
            {
            }
        }


        public static void Wait(int ms)
        // sleep for the specified no. of millisecs
        {
            Thread.Sleep(ms);
        }
    }
}