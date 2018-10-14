using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game1.Properties;
using System.Resources;

namespace Game1
{
    /// <summary>
    /// Class - Obj "Planet"
    /// </summary>
    class PlanetPic : BaseObj
    {
        private Image image;
        /// <summary>
        /// Constructor
        /// </summary>
        public PlanetPic(Point pos, Point speed, Size size, string imageName) : base(pos, speed, size)
        {
            ResourceManager rm = Resources.ResourceManager;
            image = (Bitmap)rm.GetObject("_" + imageName);
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
            if (Pos.X < 0 - Size.Width - 30)
            {
                Pos.X = Game.Width + Size.Width + rnd.Next(200, 500);
                Pos.Y = rnd.Next(0, Game.Height - Size.Height - 40);
            }
        }
    }
}
