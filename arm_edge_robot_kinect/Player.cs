using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ArmEdgeRobot;
using Newtonsoft.Json;

namespace leak
{
    class Player
    {
        private List<Movement> rewindList = new List<Movement>();
        private List<Movement> playList;
        private IRobotControler robotControler;

        public Player(IRobotControler controler, string serialized)
        {
            playList = JsonConvert.DeserializeObject<List<Movement>>(serialized);
            robotControler = controler;
        }

        private void PlayMovementList(List<Movement> movementList)
        {
            foreach (Movement movement in movementList)
            {
                if (movement.stop)
                {
                    robotControler.Stop();
                    Thread.Sleep(TimeSpan.FromMilliseconds(movement.time));
                    continue;
                }

                if (movement.gripperaction == GripperAction.Open)
                {
                    robotControler.OpenGripper();
                    continue;
                }
                
                if (movement.gripperaction == GripperAction.Close)
                {
                    robotControler.CloseGripper();
                    continue;
                }
                
                robotControler.Move(movement.direction.joint, movement.direction.dir);
                Thread.Sleep(TimeSpan.FromMilliseconds(movement.time));
                robotControler.Stop();
            }
        }

        public void Play()
        {
            PlayMovementList(playList);
        }

        public void Rewind()
        {
            rewindList.Clear();
            List<Movement> reversePlayList = playList;
            reversePlayList.Reverse();
            foreach (Movement movement in reversePlayList)
            {
                Movement newMovement = movement;
                if (newMovement.gripperaction == GripperAction.Open)
                {
                    newMovement.gripperaction = GripperAction.Close;
                }
                else if (newMovement.gripperaction == GripperAction.Close)
                {
                    newMovement.gripperaction = GripperAction.Open;
                }

                if (newMovement.direction.dir == TurnDirection.Positive)
                {
                    newMovement.direction.dir = TurnDirection.Negative;
                }
                else if (newMovement.direction.dir == TurnDirection.Negative)
                {
                    newMovement.direction.dir = TurnDirection.Positive;
                }
                rewindList.Add(newMovement);
            }
            PlayMovementList(rewindList);
        }
    }
}
