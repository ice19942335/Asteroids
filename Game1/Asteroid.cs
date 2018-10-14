using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game1.Properties;
using System.Windows.Forms;

namespace Game1
{
    /// <summary>
    /// Class - Obj "Asteroid"
    /// </summary>
    class Asteroid : BaseObj
    {
        private Image image = Resources.Asteroid;
        private int x;
        private int y;
        /// <summary>
        /// Constructor
        /// </summary>
        public Asteroid(Point pos, Point speed, Size size) : base(pos, speed, size)
        { }

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Draw method overrided
        /// </summary>
        public override void Draw()
        {
            try
            {
                Point ulCorner = Pos;
                Game.Buffer.Graphics.DrawImage(image, ulCorner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Update method overrided, updating obj position in window
        /// </summary>
        public override void Update()
        {
            Random rnd = new Random();
            Pos.X = Pos.X - Speed.X;
            if (Pos.X + 150 < 0)
            {
                Pos.X = Game.Width + Size.Width + rnd.Next(50, 150);
                Pos.Y = rnd.Next(0, Game.Height - Size.Height - 40);
            }

            x = Pos.X;
            y = Pos.Y;
        }
    }
}
