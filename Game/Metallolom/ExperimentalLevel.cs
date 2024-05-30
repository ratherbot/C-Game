using System;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public partial class View : Form
    {
        private Controller gameController;
    
        private Label playerHealthLabel;
        private Label enemiesCountLabel;

        public View()
        {
            InitializeComponent();
            gameController = new Controller(this);
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.BackColor = Color.Black;
            InitializeGame();
            
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
            {
                gameController.HandleKeyDown(e);
                e.Handled = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            gameController.HandleKeyUp(e);
        }

        private void InitializeGame()
        {
            InitializeTanks();
            InitializeBombs();
            CreateLabels();
        }

        public void InitializeBombs()
        {
            foreach (var bomb in gameController.model.bombsByNumbers.Values)
            {
                Controls.Add(bomb.PictureBox);
                bomb.PictureBox.Visible = true;
            }
        }

        private void InitializeTanks()
        {
            Controls.Add(gameController.model.tanksByNumbers[0].PictureBox);
            gameController.model.tanksByNumbers[0].PictureBox.Visible = true;

            foreach (var enemy in gameController.model.tanksByNumbers.Values)
            {
                if (enemy != gameController.model.tanksByNumbers[0])
                {
                    Controls.Add(enemy.PictureBox);
                    enemy.PictureBox.Visible = true;
                }
            }
        }

        private void CreateLabels()
        {
            playerHealthLabel = new Label();
            Controls.Add(playerHealthLabel);
            playerHealthLabel.ForeColor = Color.White;
            playerHealthLabel.Location = new Point(10, Height - 40);
            playerHealthLabel.AutoSize = true;

            enemiesCountLabel = new Label();
            Controls.Add(enemiesCountLabel);
            enemiesCountLabel.ForeColor = Color.White;
            enemiesCountLabel.Location = new Point(Width - 150, Height - 40);
            enemiesCountLabel.AutoSize = true;

            gameController.model.tanksByNumbers[0].HealthChanged += (sender, e) => UpdateHealthLabel();
            UpdateHealthLabel();
            var healthLabelTimer = new Timer();
            healthLabelTimer.Interval = 100;
            healthLabelTimer.Tick += (sender, e) => UpdateHealthLabel();
        }

        private void UpdateHealthLabel()
        {
            playerHealthLabel.Text = $"Player Health: {gameController.model.tanksByNumbers[0].Health} / {gameController.model.tanksByNumbers[0].MaxHealth}";
            enemiesCountLabel.Text = $"Dead Enemies: {gameController.model.DeadEnemies.Count} / {gameController.model.tanksByNumbers.Count - 1}";
        }

        public Bullet DrawBullet(Tank tank)
        {
            var bullet = new Bullet(tank.Model, 
                tank.Direction, 
                tank.PictureBox.Location, 
                gameController.model.BulletSize);

            var bulletBox = bullet.PictureBox;
            Controls.Add(bulletBox);
            bulletBox.Visible = true;

            Timer bulletTimer = new Timer();
            bulletTimer.Interval = 10;
            bulletTimer.Tick += (sender, e) =>
            {
                if (gameController.CanGo(bullet.Direction, bulletBox.Location))
                {
                    bullet.Move(bullet.Direction, bullet.speed, TankModel.None);

                    for (var i = 0; i < gameController.model.tanksByNumbers.Count; i++)
                    { 
                        if (tank != gameController.model.tanksByNumbers[i] && bullet.CheckCollision(gameController.model.tanksByNumbers[i].PictureBox) && gameController.model.tanksByNumbers[i].IsEnemy != tank.IsEnemy)
                        {
                            gameController.model.tanksByNumbers[i].TakeDamage(tank.Damage);
                            bulletTimer.Stop();
                            bulletTimer.Dispose();
                            Controls.Remove(bulletBox);
                            bulletBox.Dispose();
                            break;
                        }
                    }
                }
                else
                {
                    bulletTimer.Stop();
                    bulletTimer.Dispose();
                    Controls.Remove(bulletBox);
                    bulletBox.Dispose();
                }
            };
            bulletTimer.Start();

            return bullet;
        }
    }
}