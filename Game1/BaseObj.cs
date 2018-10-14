using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game1
{
    /// <summary>
    /// Class base object
    /// </summary>
    abstract class BaseObj
    {
        /// <summary>
        /// class fields
        /// </summary>
        protected Point Pos;
        protected Point Speed;
        protected Size Size;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <param name="size"></param>
        public BaseObj(Point pos, Point speed, Size size)
        {
            Pos = pos;
            Speed = speed;
            Size = size;
        }

        /// <summary>
        /// Virtual method "Draw"
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Virtual method "Update"
        /// </summary>
        public abstract void Update();

        public virtual void UpdateSprite(short dir)
        { }
    }
}
//
