using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game1
{
    /// <summary>
    /// Class - Obj "Star"
    /// </summary>
    class Star : BaseObj
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <param name="size"></param>
        public Star(Point pos, Point speed, Size size) : base(pos, speed, size)
        {

        }

        /// <summary>
        /// Draw method overrided
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width,
            Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X,
            Pos.Y + Size.Height);        }

        /// <summary>
        /// Update method overrided, updating obj position in window
        /// </summary>
        public override void Update()
        {
            Random rnd = new Random();
            Pos.X = Pos.X - Speed.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + Size.Width + rnd.Next(50, 150);
                Pos.Y = rnd.Next(0, Game.Height - Size.Height - 40);
            }
        }
    }
}
