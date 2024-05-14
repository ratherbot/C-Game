using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Metallolom
{
    public class Tank
    {
        private readonly double healthPoints;
        private readonly double damage;
        private bool isDead = false;

        public TankModel Model;

        private List<Image> tankImages = new List<Image>();
        public Image tankImage;
        private Point position;

        private Direction direction;
        public Direction Direction => direction;

        public Tank(double health, double damage, TankModel tankModel, Point position)
        {
            healthPoints = health;
            this.damage = damage;
            GetTankModel(tankModel);
            this.position = position;
            Model = tankModel;
            direction = Direction.Forward;
        }

        public void RotateTank(Direction direction) 
        {
            switch (direction)
            {
                case (Direction.Forward):
                    tankImage = tankImages[0];
                    this.direction = direction;
                    break;
                case (Direction.Right):
                    tankImage = tankImages[1];
                    this.direction = direction;
                    break;
                case (Direction.Backward):
                    tankImage = tankImages[2];
                    this.direction = direction;
                    break;
                case (Direction.Left):
                    tankImage = tankImages[3];
                    this.direction = direction;
                    break;
            }
        }

        public void GetTankModel(TankModel tankModel)
        {
            switch (tankModel)
            {
                case (TankModel.Maus):
                    tankImages.Add(Metallolom.Properties.Resources.maus_forward);
                    tankImages.Add(Metallolom.Properties.Resources.maus_right);
                    tankImages.Add(Metallolom.Properties.Resources.maus_backward);
                    tankImages.Add(Metallolom.Properties.Resources.maus_left);
                    tankImage = tankImages[0];
                    break;
                case (TankModel.T34):
                    tankImages.Add(Metallolom.Properties.Resources.green_tank_forward);
                    tankImages.Add(Metallolom.Properties.Resources.green_tank_right);
                    tankImages.Add(Metallolom.Properties.Resources.green_tank_backward);
                    tankImages.Add(Metallolom.Properties.Resources.green_tank_left);
                    tankImage = tankImages[0];
                    break;
            }
        }

        public Image GetTankImage()
        {
            return tankImage;
        }

        public Point GetPosition()
        {
            return position;
        }
    }
}