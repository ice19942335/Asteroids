using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Game1
{
    class Program
    {
        static void Main(string[] args)
        {
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            Form form = new Form();
            form.Width = resolution.Width;
            form.Height = resolution.Height - 50;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.Name = "Aleksejs Birula";
            form.Text = "Asteroids";
            Game.Init(form);
            Game.AddControls(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
