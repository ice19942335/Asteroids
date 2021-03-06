﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game1.Properties;

namespace Game1
{
    /// <summary>
    /// Class - Obj "Planet"
    /// </summary>
    class AuthorLogo : BaseObj
    {
        private Image image = Resources.Logo;
        /// <summary>
        /// Constructor
        /// </summary>
        public AuthorLogo(Point pos, Point speed, Size size) : base(pos, speed, size)
        { }

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

        public override void Update()
        { }
    }
}