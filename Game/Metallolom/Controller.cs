using System;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public class Controller
    {
        public Model model;
        private View view;
        public static Timer MTimer;
        public static Timer enemyMovementTimer;
        public static Timer bombsTimer;

        public Controller(View view)
        {
            this.view = view;
            this.model = new Model();
            MTimer = new Timer();
            MTimer.Interval = 10;
            MTimer.Tick += (sender, e) => MovePlayerTank(model.tanksByNumbers[0].PictureBox.Location);
            MTimer.Tick += (sender, e) => CheckGameEnding();
            MTimer.Tick += (sender, e) => CheckShoots();
            MTimer.Tick += (sender, e) => CheckEnemiesCount();
            MTimer.Tick += (sender, e) => CheckBombsActivating();
            MTimer.Start();

            enemyMovementTimer = new Timer();
            enemyMovementTimer.Interval = 100;

            foreach (var tank in model.tanksByNumbers.Values) 
            {
                if (tank != model.tanksByNumbers[0])
                    enemyMovementTimer.Tick += (sender, e) => MoveEnemyTank(model.tanksByNumbers[0].PictureBox.Location, tank);
            }
            enemyMovementTimer.Start();

            bombsTimer = new Timer();
            bombsTimer.Interval = 3000;
            bombsTimer.Tick += (sender, e) => ActivateBombs();
            bombsTimer.Start();
        }

        private void CheckBombsActivating()
        {
            
            foreach (var bomb in model.bombsByNumbers.Values)
            {
                bomb.CheckActivating(model.tanksByNumbers);
            }
        }

        private void ActivateBombs()
        {
            foreach(var bomb in model.bombsByNumbers.Values)
            {
                bomb.BlowUp(model.tanksByNumbers);
            }
        }

        private void CheckEnemiesCount()
        {
            var keys = model.tanksByNumbers.Keys;
            foreach (var enemy in keys)
            {
                if (model.tanksByNumbers[enemy].Health <= 0) 
                { 
                    model.tanksByNumbers[enemy].PictureBox.Location = new Point(-100, -100);
                    model.tanksByNumbers[enemy].PictureBox.Dispose();
                    model.DeadEnemies.Add(enemy);
                }
            }
        }

        private void CheckGameEnding()
        {
            if (model.IsQuitGame)
            {
                view.Close();
            }

            if (model.tanksByNumbers[0].Health <= 0)
            {
                MTimer.Stop();
                MessageBox.Show("Game Over!");
                view.Close();
            }

            if (model.DeadEnemies.Count >= model.tanksByNumbers.Count - 1)
            {
                MTimer.Stop();
                MessageBox.Show("You Win!");
                view.Close();
            }
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    model.LeftPressed = true;
                    break;
                case Keys.D:
                    model.RightPressed = true;
                    break;
                case Keys.S:
                    model.DownPressed = true;
                    break;
                case Keys.W:
                    model.UpPressed = true;
                    break;
                case Keys.Space:
                    model.SpacebarPressed = true;
                    break;
                case Keys.Escape:
                    model.IsQuitGame = true;
                    break;
                case Keys.P:
                    MTimer.Stop();
                    StopEnemies();
                    MessageBox.Show("Нажмите P, чтобы продолжить");
                    MTimer.Start();
                    MoveEnemies();
                    break;
            }
        }

        public void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    model.LeftPressed = false;
                    break;
                case Keys.D:
                    model.RightPressed = false;
                    break;
                case Keys.W:
                    model.UpPressed = false;
                    break;
                case Keys.S:
                    model.DownPressed = false;
                    break;
                case Keys.Space:
                    model.SpacebarPressed = false;
                    break;
            }
        }

        private void StopEnemies()
        {
            enemyMovementTimer.Stop();
        }

        private void MoveEnemies()
        {
            enemyMovementTimer.Start();
        }

        private void MovePlayerTank(Point tankPosition)
        {
            var playerTank = model.tanksByNumbers[0];
            if (model.LeftPressed && CanGo(Direction.Left, tankPosition))
            {
                playerTank.Move(Direction.Left, model.tanksByNumbers[0].TankSpeed, playerTank.Model);
            }

            if (model.DownPressed && CanGo(Direction.Backward, tankPosition))
            {
                playerTank.Move(Direction.Backward, model.tanksByNumbers[0].TankSpeed, playerTank.Model);
            }

            if (model.RightPressed && CanGo(Direction.Right, tankPosition))
            {
                playerTank.Move(Direction.Right, model.tanksByNumbers[0].TankSpeed, playerTank.Model);
            }

            if (model.UpPressed && CanGo(Direction.Forward, tankPosition))
            {
                playerTank.Move(Direction.Forward, model.tanksByNumbers[0].TankSpeed, playerTank.Model);
            }
        }

        public bool CanGo(Direction direction, Point point)
        {
            switch (direction)
            {
                case Direction.Left: return point.X > 0;
                case Direction.Right: return point.X + model.TankSize.Width < view.Width;
                case Direction.Forward: return point.Y > 0;
                case Direction.Backward: return point.Y + model.TankSize.Height < view.Height;
                default: return true;
            }
        }

        private void CheckShoots()
        {
            if (model.SpacebarPressed)
            {
                Shoot(model.tanksByNumbers[0]);
            }
            for (var i = 1; i < model.tanksByNumbers.Count; i++)
            {
                if (model.tanksByNumbers[i].IsFound)
                    Shoot(model.tanksByNumbers[i]);
            }
        }

        private void Shoot(Tank tank)
        {
            if ((DateTime.Now - model.LastShotTime) >= model.ShootingInterval)
            {
                model.LastShotTime = DateTime.Now;
                var bullet = view.DrawBullet(tank);
                var bulletBox = bullet.PictureBox;

                Timer bulletTimer = new Timer();
                bulletTimer.Interval = 10;
                bulletTimer.Tick += (sender, e) =>
                {
                    if (CanGo(bullet.Direction, bulletBox.Location))
                    {
                        bullet.Move(bullet.Direction, bullet.speed, TankModel.None);
                    }
                    else
                    {
                        bulletTimer.Stop();
                        bulletTimer.Dispose();
                        view.Controls.Remove(bulletBox);
                        bulletBox.Dispose();
                    }
                };
                bulletTimer.Start();
            }
        }

        private void MoveEnemyTank(Point playerPosition, Tank enemyTank)
        {
            var d = 0;
            if (Model.GetLength(playerPosition, enemyTank.PictureBox.Location) < enemyTank.VisibleDistance)
            {
                if (Math.Abs(playerPosition.X - enemyTank.PictureBox.Location.X) > (Math.Abs(playerPosition.Y - enemyTank.PictureBox.Location.Y)))
                {
                    if (playerPosition.Y - enemyTank.PictureBox.Location.Y > enemyTank.TankSpeed)
                    {
                        enemyTank.Move(Direction.Backward, enemyTank.TankSpeed, enemyTank.Model);
                        enemyTank.IsCriticalRotated = false;
                        enemyTank.IsFound = false;
                    }
                    else if (enemyTank.PictureBox.Location.Y - playerPosition.Y > enemyTank.TankSpeed)
                    {
                        enemyTank.Move(Direction.Forward, enemyTank.TankSpeed, enemyTank.Model);
                        enemyTank.IsCriticalRotated = false;
                        enemyTank.IsFound = false;
                    }
                    else
                    {
                        if (!enemyTank.IsCriticalRotated && enemyTank.PictureBox.Location.X > playerPosition.X && !enemyTank.IsFound)
                        {
                            enemyTank.Move(Direction.Left, enemyTank.TankSpeed, enemyTank.Model);
                            enemyTank.IsCriticalRotated = true;
                            enemyTank.IsFound = true;

                        }
                        else if (!enemyTank.IsCriticalRotated && !enemyTank.IsFound)
                        {
                            enemyTank.Move(Direction.Right, enemyTank.TankSpeed, enemyTank.Model);
                            enemyTank.IsCriticalRotated = true;
                            enemyTank.IsFound = true;

                        }
                    }
                }
                else
                {
                    if (playerPosition.X - enemyTank.PictureBox.Location.X > enemyTank.TankSpeed)
                    {
                        enemyTank.Move(Direction.Right, enemyTank.TankSpeed, enemyTank.Model);
                        enemyTank.IsCriticalRotated = false;
                        enemyTank.IsFound = false;
                    }
                    else if (enemyTank.PictureBox.Location.X - playerPosition.X > enemyTank.TankSpeed)
                    {
                        enemyTank.Move(Direction.Left, enemyTank.TankSpeed, enemyTank.Model);
                        enemyTank.IsCriticalRotated = false;
                        enemyTank.IsFound = false;
                    }
                    else
                    {
                        if (!enemyTank.IsCriticalRotated && enemyTank.PictureBox.Location.Y > playerPosition.Y)
                        {
                            enemyTank.Move(Direction.Forward, enemyTank.TankSpeed, enemyTank.Model);
                            enemyTank.IsCriticalRotated = true;
                            enemyTank.IsFound = true;
                        }
                        else if (!enemyTank.IsCriticalRotated)
                        {
                            enemyTank.Move(Direction.Backward, enemyTank.TankSpeed, enemyTank.Model);
                            enemyTank.IsCriticalRotated = true;
                            enemyTank.IsFound = true;
                        }
                    }
                }
            }
            else
            {
                var random = new Random();
                var number = random.Next(4);
                if (CanGo((Direction)number, enemyTank.PictureBox.Location))
                    enemyTank.Move((Direction)number, enemyTank.TankSpeed, enemyTank.Model);
            }
        }
    }
}