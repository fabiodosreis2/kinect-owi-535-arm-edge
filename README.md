# kinect-owi-535-arm-edge
C# program that controls [owi-535 robot](http://www.owirobots.com/store/catalog/robotic-arm-kits-and-accessories/owi-535pc-robotic-arm-kit-with-usb-pc-interface-138.html) by [Kinect sensor]((http://www.xbox.com/en-US/xbox-360/accessories/kinect)) gestures. Warning: This was a scratch for a homework at university so take careful :)

# Getting Started

The solution is splitted in two projects: ArmEdgeRobotCom library that uses USB to comunicate with the OWI-535 robot and ArmEdgeRobotKinect that capture Kinect gestures and use the library to send commands to robot.

# Dependencies

LibUsbDotNet - [http://sourceforge.net/projects/libusbdotnet/](http://sourceforge.net/projects/libusbdotnet/)
Json.Net - [http://www.newtonsoft.com/json](http://www.newtonsoft.com/json)
Kinect for Windows SDK v1.0 - [http://www.microsoft.com/en-us/download/details.aspx?id=28782](http://www.microsoft.com/en-us/download/details.aspx?id=28782)

# ArmEdgeRobotCom usage
    RobotArmEdge robot = new RobotArmEdge();
    robot.TurnByDirection(RobotJoint.Shoulder, TurnDirection.Positive);
    Thread.Sleep(200);
    robot.Stop();
    robot.robot.OpenGripper();
    robot.TurnByOffset(RobotJoint.Shoulder, 90);
    
# ArmEdgeRobotKinect usage
    Click at start button.
    Send gestures to Kinect sensors.
    The gestures are:
        Right hand to move up,down, left and right.
        To close the gripper, join the hands.
        To open the gripper, separate the hands.
        To stop, elevate the hands at the same level near the chest.
    More details watch at [youtube](https://youtu.be/NEh17PsNnd0).
