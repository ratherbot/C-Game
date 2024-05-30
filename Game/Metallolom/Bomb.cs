using Metallolom.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Metallolom
{
    public class Bomb: ModelBox
    {
        public int Damage { get; private set; }
        public bool isActivated { get; private set; } = false;
        public Bomb(Point position, Size size) 
        {
            Damage = 100;
            PictureBox = new PictureBox();
            PictureBox.Size = size;
            PictureBox.Location = position;
            PictureBox.Image = Resources.bomb;
            PictureBox.BackColor = Color.Transparent;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void CheckActivating(Dictionary<int, Tank> tanksByNumbers)
        {
            foreach (var tank in tanksByNumbers.Values)
            {
                if (Model.GetLength(tank.PictureBox.Location, PictureBox.Location) < 500)
                {
                    isActivated = true;
                    PictureBox.Image = Resources.bomb_ready;
                    break;
                }
            }
        }

        public void BlowUp(Dictionary<int, Tank> tanksByNumbers)
        {
            if (isActivated)
                foreach (var tank in tanksByNumbers.Values)
                {
                    tank.TakeDamage((int)(Damage / (Model.GetLength(PictureBox.Location, tank.PictureBox.Location) / 8)));
                }
                PictureBox.Location = new Point(-100, -100);
                PictureBox.Dispose();
        }
    }
}
