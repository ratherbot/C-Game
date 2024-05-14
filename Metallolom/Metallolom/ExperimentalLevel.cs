using Metallolom.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metallolom
{
    public partial class View : Form
    {
        private Controller gameController;
        public PictureBox playerTankBox;
        
        public View()
        {
            InitializeComponent();
            gameController = new Controller(this);
            this.KeyDown += (OnKeyDown);
            this.KeyUp += (OnKeyUp);
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
            playerTankBox = new PictureBox();
            this.Controls.Add(playerTankBox);
            playerTankBox.Size = gameController.model.TankSize;
            playerTankBox.SizeMode = PictureBoxSizeMode.StretchImage;
            playerTankBox.Location = gameController.model.Position;
            playerTankBox.Image = gameController.model.playerTank.tankImage;
            playerTankBox.Visible = true;
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}

        public Tuple<Bullet,PictureBox> DrawBullet(PictureBox tankBox)
        {
            var bulletBox = new PictureBox();
            this.Controls.Add(bulletBox);
            bulletBox.Size = gameController.model.TankSize;
            bulletBox.SizeMode = PictureBoxSizeMode.StretchImage;
            bulletBox.Location = tankBox.Location;
            var bullet = new Bullet(gameController.model.playerTank.Model, gameController.model.playerTank.Direction, bulletBox.Location);
            bulletBox.Image = bullet.bulletImage;
            bulletBox.Visible = true;
            return Tuple.Create(bullet, bulletBox);
        }

        private void View_Load(object sender, EventArgs e)
        {

        }
    }
}