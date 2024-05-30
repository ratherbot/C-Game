using Metallolom.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public class Tank: ModelBox
    {
        public int VisibleDistance { get; private set; }
        public TankModel Model { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int TankSpeed { get; private set; }

        public event EventHandler HealthChanged;
        public bool IsCriticalRotated = false;
        public bool IsFound = false;
        public bool IsEnemy { get; private set; }
        public Tank(TankModel model, Point initialPosition, Size size, bool isEnemy)
        {
            SetTankImage(initialPosition, size);
            GetTankProperties(model);
            IsEnemy = isEnemy;
        }

        private void SetTankImage(Point initialPosition, Size size)
        {
            PictureBox = new PictureBox();
            PictureBox.Location = initialPosition;
            PictureBox.BackColor = Color.Transparent;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.Size = size;
        }

        private void GetTankProperties(TankModel model)
        {
            switch (model) 
            {
                case TankModel.Maus:
                    Damage = 50;
                    Health = 250;
                    MaxHealth = Health;
                    TankSpeed = 5;
                    PictureBox.Image = Resources.maus_backward;
                    Model = model;
                    VisibleDistance = 500;
                    break;
                case TankModel.T34:
                    Damage = 20;
                    Health = 100;
                    MaxHealth = Health;
                    TankSpeed = 15;
                    PictureBox.Image = Resources.green_tank_backward;
                    Model = model;
                    VisibleDistance = 650;
                    break;
                case TankModel.Babah:
                    Damage = 250;
                    Health = 51;
                    MaxHealth = Health;
                    TankSpeed = 10;
                    PictureBox.Image = Resources.babah_backward;
                    Model = model;
                    VisibleDistance = 5000;
                    break;
            }
        }

        //public void RotateTank(Direction newDirection)
        //{
        //    if (Direction != newDirection)
        //    {
        //        switch (Model)
        //        {
        //            case TankModel.Maus:
        //                PictureBox.Image = Images[1];
        //                break;
        //            case TankModel.T34:
        //                PictureBox.Image = Images[1];
        //                break;
        //            case TankModel.Babah:
        //                PictureBox.Image = Images[1];
        //                break;
        //        }

        //        Direction = newDirection;
        //        switch (newDirection)
        //        {
        //            case Direction.Forward:
        //                PictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        //                break;
        //            case Direction.Backward:
        //                PictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        //                break;
        //            case Direction.Left:
        //                PictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        //                break;
        //            case Direction.Right:
        //                PictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
        //                break;
        //        }
        //    }
        //}

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(this, EventArgs.Empty);
            if (Health <= 0)
            {
                Health = 0;
            }
        }

        //public void Move(Direction direction, int speed)
        //{
        //    RotateTank(direction);

        //    var x = PictureBox.Location.X;
        //    var y = PictureBox.Location.Y;

        //    switch (direction)
        //    {
        //        case Direction.Forward:
        //            var pointUp = new Point(x, y - speed);
        //            PictureBox.Location = pointUp;
        //            break;
        //        case Direction.Backward:
        //            var pointDown = new Point(x, y + speed);
        //            PictureBox.Location = pointDown;
        //            break;
        //        case Direction.Left:
        //            var pointLeft = new Point(x - speed, y);
        //            PictureBox.Location = pointLeft;
        //            break;
        //        case Direction.Right:
        //            var pointRight = new Point(x + speed, y);
        //            PictureBox.Location= pointRight;
        //            break;
        //    }
        //}
    }
}