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
    /// Class - Obj "Planet"
    /// </summary>
    class Bullet : BaseObj
    {
        private Image image = Resources.Bullet;
        private int x;
        private int y;
        /// <summary>
        /// Constructor
        /// </summary>
        public Bullet(Point pos, Point speed, Size size) : base(pos, speed, size)
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
            Pos.X += 50;
            x = Pos.X;
            y = Pos.Y;
        }

    }
}

