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
    class DesAsteroid : BaseObj
    {
        private Image image = Resources.DesAsteroid_1;
        private DateTime t = new DateTime();
        /// <summary>
        /// Constructor
        /// </summary>
        public DesAsteroid(Point pos, Point speed, Size size) : base(pos, speed, size)
        {
            t = DateTime.Now;
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
            image = Resources.DesAsteroid_1;

            DateTime t2 = DateTime.Now;
            double tTotal = (t - t2).TotalMilliseconds;

            if (tTotal > 500)
                image = Resources.DesAsteroid_2;
        }
    }
}
