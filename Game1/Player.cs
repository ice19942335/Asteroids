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
    class Player : BaseObj
    {
        private Image image = Resources.Player;
        private int x;
        private int y;
        /// <summary>
        /// Constructor
        /// </summary>
        public Player(Point pos, Point speed, Size size) : base(pos, speed, size)
        {
            x = Pos.X;
            y = Pos.Y;
        }

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
                //Image image = Image.FromFile("Player.png");
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
        {  }

        /// <summary>
        /// Updating Player sprite coordinates depends what button is pressed
        /// </summary>
        /// <param name="dir"></param>
        public override void UpdateSprite(short dir)
        {
            short speed = 15;

            switch (dir)
            {
                case 1:
                    if (Pos.Y > -30)
                        Pos.Y -= speed;
                    break;
                case 2:
                    if (Pos.Y < Game.Height - 180)
                        Pos.Y += speed;
                    break;
                case 3:
                    if (Pos.X < Game.Width / 2)
                        Pos.X += speed;
                    break;
                case 4:
                    if (Pos.X > 0)
                        Pos.X -= speed;
                    break;


                //Below not working YET!
                /*case 5: 
                      Pos.Y -= speed;
                      Pos.X += speed;
                      break;
                  case 6:
                      Pos.Y -= speed;
                      Pos.X -= speed;
                      break;
                  case 7:
                      Pos.Y += speed;
                      Pos.X += speed;
                      break;
                  case 8:
                      Pos.Y += speed;
                      Pos.X -= speed;
                      break;*/

                default:
                    break;
            }

            x = Pos.X;
            y = Pos.Y;

        }
    }
}
