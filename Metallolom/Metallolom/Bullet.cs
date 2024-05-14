using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metallolom
{
    public class Bullet
    {
        private List<Image> bulletImages = new List<Image>();
        public Image bulletImage;
        public Point bulletPosition;
        public Direction Direction;

        public Bullet(TankModel tankModel, Direction direction, Point bulletPosition)
        {
            GetBulletImages(tankModel);
            GetBulletDirection(direction);
            this.bulletPosition = bulletPosition;
            Direction = direction;
        }

        public void GetBulletImages(TankModel tank)
        {
            switch (tank) 
            {
                case(TankModel.T34):
                    bulletImages.Add(Properties.Resources.T34_b_forward);
                    bulletImages.Add(Properties.Resources.T34_b_right);
                    bulletImages.Add(Properties.Resources.T34_b_backward);
                    bulletImages.Add(Properties.Resources.T34_b_left);
                    break;
                case (TankModel.Maus):
                    bulletImages.Add(Properties.Resources.T34_b_forward);
                    bulletImages.Add(Properties.Resources.T34_b_right);
                    bulletImages.Add(Properties.Resources.T34_b_backward);
                    bulletImages.Add(Properties.Resources.T34_b_left);
                    break;
                default: break;
            }
        }

        public void GetBulletDirection(Direction direction)
        {
            switch(direction) 
            {
                case Direction.Forward:
                    bulletImage = bulletImages[0];
                    break;
                case Direction.Right:
                    bulletImage = bulletImages[1];
                    break;
                case Direction.Backward:
                    bulletImage = bulletImages[2];
                    break;
                case Direction.Left:
                    bulletImage = bulletImages[3];
                    break;
            }
        }
    }
}
