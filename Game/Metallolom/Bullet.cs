using Metallolom.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public class Bullet: ModelBox
    {
        public int speed { get; private set; }

        public Bullet(TankModel tankModel, Direction direction, Point initialPosition, Size bulletSize)
        {
            SetBulletBoxProperties(direction, initialPosition, bulletSize);
            var noneTankType = TankModel.None;
            Rotate(direction, noneTankType);
            speed = 5;
        }

        private void SetBulletBoxProperties(Direction direction, Point initialPosition, Size bulletSize)
        {
            Direction = direction;
            PictureBox = new PictureBox();
            PictureBox.Location = initialPosition;
            PictureBox.Size = bulletSize;
            PictureBox.BackColor = Color.Transparent;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SetBulletImage(direction);
        }

        private void SetBulletImage(Direction direction)
        {
            switch (direction) 
            {
                case Direction.Left:
                    PictureBox.Image = Resources.T34_b_left;
                    break;
                case Direction.Right:
                    PictureBox.Image = Resources.T34_b_right;
                    break;
                case Direction.Forward:
                    PictureBox.Image = Resources.T34_b_forward;
                    break;
                case Direction.Backward:
                    PictureBox.Image = Resources.T34_b_backward;
                    break;
            }
        }
    }
}