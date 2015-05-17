using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmEdgeRobot;
using Newtonsoft.Json;

namespace leak
{
    public enum GripperAction
    {
        None,
        Open,
        Close
    }

    public struct Movement
    {
        public RobotDirection direction;
        public double time;
        public string description;
        public bool stop;
        public bool slow;
        public GripperAction gripperaction;
    }

    class Recorder
    {
        private List<Movement> movementlist = new List<Movement>();
        private bool isRecording = false;

        public void Record()
        {
            isRecording = true;
            movementlist.Clear();
        }

        public void Stop()
        {
            isRecording = false;
        }

        public void Save(string filepath)
        {
           string json = JsonConvert.SerializeObject(movementlist, Formatting.Indented);
           System.IO.File.WriteAllText(filepath, json);
        }
 
        public void RegisterMove(RobotDirection direction, bool slow)
        {
            if (!isRecording) return;

            double currTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
            Movement currentmovement = new Movement();
            currentmovement.direction = direction;
            currentmovement.time = currTime;
            currentmovement.slow = slow;
            currentmovement.description = BuildDescription(currentmovement);

            UpdateLastMove();
            movementlist.Add(currentmovement);
        }

        private void UpdateLastMove()
        {
            double currTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
            if (movementlist.Count == 0)
            {
                return;
            }

            int lastIndex = movementlist.Count - 1;
            Movement lastMovement = movementlist.ElementAt(lastIndex);

            // stop movements and openning movs do not need update
            if (lastMovement.stop || lastMovement.gripperaction != GripperAction.None )
            {
                return;
            }

            lastMovement.time = currTime - lastMovement.time;
            movementlist.RemoveAt(lastIndex);
            movementlist.Add(lastMovement);
        }

        public void RegisterGripper(bool isOpen)
        {
            if (!isRecording) return;

            Movement movement = new Movement();
            if (isOpen)
            {
                movement.gripperaction = GripperAction.Open;
            }
            else
            {
                movement.gripperaction = GripperAction.Close;
            }
            movement.description = BuildDescription(movement);

            UpdateLastMove();

            movementlist.Add(movement);
        }

        public void RegisterStop()
        {
            if (!isRecording) return;

            Movement movement = new Movement();
            movement.stop = true;
            movement.description = BuildDescription(movement);

            UpdateLastMove();

            movementlist.Add(movement);
        }

        private string BuildDescription(Movement movement)
        {
            string description = "";
            if (movement.stop)
            {
                description = "stop";
                return description;
            }

            if (movement.gripperaction == GripperAction.Open)
            {
                description = "open gripper";
                return description;
            } 
            else if (movement.gripperaction == GripperAction.Close)
            {
                description = "close gripper";
                return description;
            }

            if (movement.slow)
            {
                description = "slow ";
            }

            if (movement.direction.joint == ArmEdgeRobot.RobotJoint.Base)
            {
                description += "move base ";
                if (movement.direction.dir == TurnDirection.Positive)
                {
                    description += "right";
                }
                else
                {
                    description += "left";
                }
            }
            else if (movement.direction.joint == ArmEdgeRobot.RobotJoint.Elbow)
            {
                description += "move elbow ";
                if (movement.direction.dir == TurnDirection.Positive)
                {
                    description += "down";
                }
                else
                {
                    description += "up";
                }
            }
            return description;
        }
    }
}
