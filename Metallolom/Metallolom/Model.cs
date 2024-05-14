using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metallolom
{
    public class Model
    {
        public Tank playerTank = new Tank(100, 20, TankModel.Maus, new Point(0, 0));
        private Size tankSize = new Size(70, 70);
        public Size TankSize => tankSize;
        

        public Model()
        {
            Position = new Point(0, 0);
        }

        public Direction Direction { get; set; }
        public Point Position { get; set; }
        public bool LeftPressed { get; set; }
        public bool RightPressed { get; set; }
        public bool UpPressed { get; set; }
        public bool DownPressed { get; set; }
        public bool SpacebarPressed { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsQuitGame { get; set; }

        public void MoveEntity(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }


        public void GameOver()
        {
        }
    }
}