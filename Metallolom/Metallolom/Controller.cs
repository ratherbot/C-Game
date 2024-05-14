using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Metallolom
{
    public class Controller
    {
        public Model model;
        private View view;
        public static Timer MTimer;
        public Controller(View view)
        {
            this.view = view;
            this.model = new Model();
            MTimer = new Timer();
            MTimer.Interval = 10;
            MTimer.Tick += (sender, e) => MoveEntity();
            MTimer.Tick += (sender, e) => CheckQuitGameButton();
            MTimer.Tick += (sender, e) => Shoot();
        }

        private void CheckQuitGameButton()
        {
            if (model.IsQuitGame)
            {
                view.Close();
            }
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: 
                    model.LeftPressed = true;
                    //model.Direction = Direction.Left;
                    break;
                case Keys.D: 
                    model.RightPressed = true; 
                    //model.Direction = Direction.Right;
                    break;
                case Keys.S: 
                    model.DownPressed = true;
                    //model.Direction = Direction.Backward;
                    break;
                case Keys.W: 
                    model.UpPressed = true;
                    //model.Direction = Direction.Forward;
                    break;
                case Keys.Space: 
                    model.SpacebarPressed = true; 
                    break;
                case Keys.Escape: 
                    model.IsQuitGame = true; 
                    break;
            }
            MTimer.Start();
        }

        public void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: model.LeftPressed = false; break;
                case Keys.D: model.RightPressed = false; break;
                case Keys.W: model.UpPressed = false; break;
                case Keys.S: model.DownPressed = false; break;
                case Keys.Space: model.SpacebarPressed = false; break;
            }

            if (!model.LeftPressed && !model.RightPressed && !model.UpPressed && !model.DownPressed && !model.SpacebarPressed)
                MTimer.Stop();
        }

        private void MoveEntity()
        {
            var tankPosition = model.Position;
            if (model.LeftPressed && CanGo(Direction.Left, tankPosition))
            {
                model.MoveEntity(-10, 0);
                model.playerTank.RotateTank(Direction.Left);
                view.playerTankBox.Image = model.playerTank.tankImage;
                view.playerTankBox.Location = model.Position;
            }

            if (model.DownPressed && CanGo(Direction.Backward, tankPosition))
            {
                model.MoveEntity(0, 10);
                model.playerTank.RotateTank(Direction.Backward);
                view.playerTankBox.Image = model.playerTank.tankImage;
                view.playerTankBox.Location = model.Position;
            }

            if (model.RightPressed && CanGo(Direction.Right, tankPosition))
            {
                model.MoveEntity(10, 0);
                model.playerTank.RotateTank(Direction.Right);
                view.playerTankBox.Image = model.playerTank.tankImage;
                view.playerTankBox.Location = model.Position;
            }

            if (model.UpPressed && CanGo(Direction.Forward, tankPosition))
            {
                model.MoveEntity(0, -10);
                model.playerTank.RotateTank(Direction.Forward);
                view.playerTankBox.Image = model.playerTank.tankImage;
                view.playerTankBox.Location = model.Position;
            }
        }

        private bool CanGo(Direction direction, Point point)
        {
            switch (direction)
            {
                case Direction.Left: return (point.X) > 0;
                case Direction.Right: return (point.X + model.TankSize.Width) < view.Width;
                case Direction.Forward: return (point.Y) > 0;
                case Direction.Backward: return (point.Y + model.TankSize.Height) < view.Height;
                default: return true;
            }
        }

        private void Shoot()
        {
            var isShooted = false;
            PictureBox bulletBox = null;
            Bullet bullet = null;

            if (model.SpacebarPressed)
            {
                var tuple = view.DrawBullet(view.playerTankBox);
                bulletBox = tuple.Item2;
                bullet = tuple.Item1;
                isShooted = true;
            }

            if (isShooted) 
            {
                while (CanGo(bullet.Direction, bulletBox.Location))
                {
                    switch (bullet.Direction)
                    {
                        case Direction.Right:
                            bulletBox.Location = new System.Drawing.Point(bulletBox.Location.X + 10, bulletBox.Location.Y);
                            break;
                        case Direction.Left:
                            bulletBox.Location = new System.Drawing.Point(bulletBox.Location.X - 10, bulletBox.Location.Y);
                            break;
                        case Direction.Forward:
                            bulletBox.Location = new System.Drawing.Point(bulletBox.Location.X, bulletBox.Location.Y - 10);
                            break;
                        case Direction.Backward:
                            bulletBox.Location = new System.Drawing.Point(bulletBox.Location.X, bulletBox.Location.Y + 10);
                            break;
                    }
                }
            }          
        }
    }
}