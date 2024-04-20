using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Tank tank;
        private Tank enemyTank;
        private bool isStart = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
            tank = new Tank(pictureBox2, 190, false);
            enemyTank = new Tank(pictureBox3, 500, true);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keyPressed = e.KeyChar;
            
            switch (keyPressed)
            {
                case (char)Keys.Escape:
                    Close();
                    break;
                case (char)Keys.A:
                    tank.Move(Direction.Left);
                    break;
                case (char)Keys.W:
                    tank.Move(Direction.Forward);
                    break;
                case (char)Keys.D:
                    tank.Move(Direction.Right);
                    break;
                case (char)Keys.S:
                    tank.Move(Direction.Backward);
                    break;
            }
        }

        private void UpdatePicture(object sender, EventArgs e)
        {
            if (isStart)
            {
                var direction = enemyTank.FindDirection(tank.tank);
                enemyTank.Move(direction);
            }
        }

        // Эти 5 методов пока просто существуют
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            isStart = true;
            ((Control)sender).Hide();
            Focus();
        }
    }
}