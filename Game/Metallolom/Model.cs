using System;
using System.Collections.Generic;
using System.Drawing;

namespace Metallolom
{
    public class Model
    {
        public Dictionary<int, Tank> tanksByNumbers;
        public Dictionary<int, Bomb> bombsByNumbers;

        private static Size tankSize = new Size(70, 70);
        private static Size bulletSize = new Size(50, 50);
        private static Size bombSize = new Size(60, 60);
        public Size TankSize => tankSize;
        public Size BulletSize => bulletSize;

        public HashSet<int> DeadEnemies;

        private Random randomNumber = new Random();
        public Model()
        {
            tanksByNumbers = new Dictionary<int, Tank>();
            bombsByNumbers = new Dictionary<int, Bomb>();
            tanksByNumbers[0] = new Tank(TankModel.Maus, new Point(1200, 500), tankSize, false);
            FillMapWithEnemies(enemiesCount: 5);
            FillMapWithBombs(bombsCount: 5);
            DeadEnemies = new HashSet<int>();
        }
        public bool LeftPressed { get; set; }
        public bool RightPressed { get; set; }
        public bool UpPressed { get; set; }
        public bool DownPressed { get; set; }
        public bool SpacebarPressed { get; set; }
        public bool IsQuitGame { get; set; }

        public DateTime LastShotTime { get; set; } = DateTime.MinValue;
        public TimeSpan ShootingInterval { get; set; } = TimeSpan.FromMilliseconds(500);

        public static double GetLength(Point firstPosition, Point secondPosition)
        {
            var dx = Math.Abs(firstPosition.X - secondPosition.X) * Math.Abs(firstPosition.X - secondPosition.X);
            var dy = Math.Abs(firstPosition.Y - secondPosition.Y) * Math.Abs(firstPosition.Y - secondPosition.X);
            return Math.Sqrt(dx + dy);
        }

        private void FillMapWithEnemies(int enemiesCount)
        {
            for (var i = 1; i < enemiesCount + 1; i++)
            {
                var x = randomNumber.Next(800);
                var y = randomNumber.Next(800);
                tanksByNumbers[i] = new Tank((TankModel)randomNumber.Next(3), new Point(x, y), tankSize, true);
            }
        }

        private void FillMapWithBombs(int bombsCount)
        {
            for (var i = 0; i < bombsCount; i++)
            {
                var x = randomNumber.Next(800);
                var y = randomNumber.Next(800);
                bombsByNumbers[i] = new Bomb(new Point(x, y), bombSize);
            }
        }
    }
}