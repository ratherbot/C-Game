using Metallolom.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public class ModelBox
    {
        public PictureBox PictureBox;
        public Direction Direction;
        public bool isRotatable {  get; set; }
        public bool CheckCollision(PictureBox anotherBox)
        {
            Rectangle modelRect = new Rectangle(PictureBox.Location, PictureBox.Size);
            Rectangle anotherModelRect = new Rectangle(anotherBox.Location, anotherBox.Size);
            return modelRect.IntersectsWith(anotherModelRect);
        }

        public void Move(Direction direction, int speed, TankModel model)
        {
            Rotate(direction, model);
            var x = PictureBox.Location.X;
            var y = PictureBox.Location.Y;
            switch (direction)
            {
                case Direction.Forward:
                    PictureBox.Location = new Point(x, y - speed);
                    break;
                case Direction.Backward:
                    PictureBox.Location = new Point(x, y + speed);
                    break;
                case Direction.Left:
                    PictureBox.Location = new Point(x - speed, y);
                    break;
                case Direction.Right:
                    PictureBox.Location = new Point(x + speed, y);
                    break;
            }
        }

        public void Rotate(Direction newDirection, TankModel model)
        {
            if (Direction != newDirection)
            {
                IfTank(model);

                Direction = newDirection;
                switch (newDirection)
                {
                    case Direction.Forward:
                        PictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case Direction.Backward:
                        PictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case Direction.Left:
                        PictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case Direction.Right:
                        PictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                        break;
                }
            }
        }

        private void IfTank(TankModel model)
        {
            if (model != TankModel.None)
            {
                switch (model) 
                {
                    case (TankModel.Maus):
                        PictureBox.Image = Resources.maus_right;
                        break;
                    case (TankModel.T34):
                        PictureBox.Image = Resources.green_tank_right;
                        break;
                    case (TankModel.Babah):
                        PictureBox.Image = Resources.babah_right;
                        break;
                }
            }
        }
    }
}