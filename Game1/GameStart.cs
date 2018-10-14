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
    /// Main class of the game
    /// </summary>
    class GameStart
    {
        /// <summary>
        /// Class fields
        /// </summary>
        private BufferedGraphicsContext context;
        public BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static List<BaseObj> objList = new List<BaseObj>();
        private static Random rnd = new Random();
        static GameStart() { }

        private static  Form formGame;
        /// <summary>
        /// Load method, loading objects in list
        /// </summary>
        public static void Load()
        {
            #region BackGround
            objList.Add(new BackGround(new Point(rnd.Next(0, 0), rnd.Next(0, 0)), new Point(0, 0), new Size(0, 0)));
            #endregion BackGround

            #region Stars
            //Adding start in list
            for (int i = 0; i < 30; i++)
            {
                //int starSize = rnd.Next(3, 20);
                //int starSpeed = 2; //This variable means starSpeed, "Direction is not correct describtion"
                int starSize = 0;
                int starSpeed = 0;

                if (i >= 0 && i < 11)
                {
                    starSpeed = rnd.Next(1, 2);
                    starSize = rnd.Next(1, 3);
                }
                else if (i > 9 && i < 21)
                {
                    starSpeed = rnd.Next(2, 3);
                    starSize = rnd.Next(2, 4);
                }
                else if (i > 19 && i < 31)
                {
                    starSpeed = rnd.Next(3, 4);
                    starSize = rnd.Next(4, 7);
                }

                objList.Add(new Star(new Point(rnd.Next(0, 500), rnd.Next(0, 800)), new Point(starSpeed, starSpeed), new Size(starSize, starSize)));
            }
            #endregion Stars

            #region PlanetPic
            //Adding planets in list
            int planetSize = 75;
            int planetSpeed = 1;
            for (int i = 1; i < 6; i++)
            {
                objList.Add(new PlanetPic(new Point(rnd.Next(0, 500), rnd.Next(0, 800)), new Point(planetSpeed, planetSpeed), new Size(planetSize, planetSize), i.ToString()));
                planetSize += 75;
                planetSpeed++;
            }
            #endregion PlanetPic

            #region PlayerSprite
            int playerSpeed = 1;
            objList.Add(new Player(new Point(5, Height / 3 - 20), new Point(playerSpeed, playerSpeed), new Size(0, 0)));

            #endregion PlayerSprite

            #region Author
            objList.Add(new AuthorLogo(new Point(Width - 283, 10), new Point(0, 0), new Size(0, 0)));
            #endregion Author
        }

        /// <summary>
        /// Init method. Initializing
        /// </summary>
        /// <param name="form"></param>
        public void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            formGame = form;
            if (form.Width > 1500 || form.Height > 1000)
                throw new WrongFormSize("Max Width: 1500 Max Height: 600");
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();



            Timer timer = new Timer { Interval = 60 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Timer method that call methods on "Tick"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Draw method, calling Draw method from object
        /// </summary>
        public void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var obj in objList)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Update method, calling Update method from object
        /// </summary>
        public static void Update()
        {
            foreach (var obj in objList)
                obj.Update();
        }
    }
}
