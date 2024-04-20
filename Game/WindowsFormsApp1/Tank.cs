using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Tank
    {
        public PictureBox tank;
        private double healthPoints;
        //private Image image = Image.FromFile(@"C:\Users\User\Downloads\second-tank.png");
        private bool isDead = false;
        private bool canRotate;

        public Tank(PictureBox tank, double healthPoints, bool canRotate)
        {
            this.tank = tank;
            this.healthPoints = healthPoints;
            this.canRotate = canRotate;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Forward):
                    if (CanMove(direction))
                    {
                        Rotate(direction);
                        tank.Top -= 10;
                    }
                    break;

                case (Direction.Backward):
                    if (CanMove(direction))
                    {
                        Rotate(direction);
                        tank.Top += 10;
                    }
                    break;

                case (Direction.Left):
                    if (CanMove(direction))
                    {
                        Rotate(direction);
                        tank.Left -= 10;
                    }
                    break;

                case (Direction.Right):
                    if (CanMove(direction))
                        tank.Left += 10;
                    break;
            }
        }

        private bool CanMove(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Right):
                    return tank.Location.X < 1500;
                case (Direction.Left):
                    return tank.Location.X >= 0;
                case (Direction.Forward):
                    return tank.Location.Y >= 0;
                case (Direction.Backward):
                    return tank.Location.Y <= 1000;
            }
            return false;
        }

        private void Rotate(Direction direction)
        {
            if (canRotate)
            {
                switch (direction)
                {
                    case (Direction.Backward):
                        tank.Image = new Bitmap(WindowsFormsApp1.Properties.Resources.green_tank_backward);
                        break;
                    case (Direction.Forward):
                        tank.Image = new Bitmap(WindowsFormsApp1.Properties.Resources.green_tank_forward);
                        break;
                    case (Direction.Left):
                        tank.Image = new Bitmap(WindowsFormsApp1.Properties.Resources.green_tank_left);
                        break;
                    case (Direction.Right):
                        tank.Image = new Bitmap(WindowsFormsApp1.Properties.Resources.green_tank_right);
                        break;
                }
            }
        }

        public Direction FindDirection(PictureBox picture)
        {
            if (picture.Location.X < tank.Location.X && Math.Abs(picture.Location.X - tank.Location.X) > 10)
                return Direction.Left;
            if (picture.Location.X > tank.Location.X && Math.Abs(picture.Location.X - tank.Location.X) <= 10 || picture.Location.X > tank.Location.X)
                return Direction.Right;
            if (picture.Location.Y < tank.Location.Y)
                return Direction.Forward;
            //if (picture.Location.Y > tank.Location.Y)
            return Direction.Backward;
            //throw new NotImplementedException();
        }


        //public void ChangePicture()
        //{
        //    tank.Image = image;
        //}
    }
}