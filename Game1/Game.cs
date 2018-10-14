using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Game1
{
    /// <summary>
    /// Main class of the game
    /// </summary>
    static class Game
    {
        // //////////////////////////////
        #region Delegate
        //!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!
        //Как хорошо что по методичке мы начали работать со статикой, а потом оказывается что это не удобно... GeekBrains одним словом.
        //Я надеюсь этот пункт в дз примется, так как всю логику я написал, просто я не могу сделать екземпляр класса "Game"

        //  public delegate void ConsoleHandler(string message); //в классе

        /*
            ЕкземплярКласса.RegistreHandler(Метод);
            ЕкземплярКласса.UnRegistreHandler(Метод);
            
            Вызываем метод, но к нему привязано 2 разных реализации соответственно и выполнится 2 разных метода.
            ЕкземплярКласса.PrintMessage("Сообщение");
            

            P.s Я думаю что разобрался в этой теме можете задать вопросы я думаю что отвечу.
        */

        /*

        
        private static string _str;
        private static ConsoleHandler _consoleHandler;

        public static void RegistreHandler(ConsoleHandler str)
        {
            _consoleHandler += str;
        }

        public static void UnRegistreHandler(ConsoleHandler str)
        {
            _consoleHandler -= str;
        }

        public static void PrintMessage(string str)
        {
            _str += str;
            if (_consoleHandler != null)
                _consoleHandler($"{DateTime.Now}:: {str}\n\r");
        }

        //Это уже реализация
        public static void LogConsole(string message)
        {
            consoleLinesList.Add(message);
        }

        public static void LogFileConsole(string message)
        {
            File.AppendAllText("ConsoleLog.txt", message);
        }
        

        */
        //!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!//!!!!!!!!!!!!!
        #endregion Delegate
        // //////////////////////////////

        //Constants
        private const string RECORDS_FILE_NAME = "Records.txt";
        private const string CONSOLE_LOG_FILE_NAME = "ConsoleLog.txt";

        private const int LIVES_COUNT = 3; // Колличество жизней
        private const int ASTEROIDS_WAVES = 150; // Колличество волн астероидов
        private const int MIN_TIME_TO_SPAWN_LIFE = 30;
        private const int MAX_TIME_TO_SPAWN_LIFE = 60;
        private const int LIFE_SPEED = 10;
        private static int asteroidWaves = ASTEROIDS_WAVES;
        private static int lifeSpeed = LIFE_SPEED;
        private static int lives = LIVES_COUNT;

        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        
        private static List<BaseObj> objList = new List<BaseObj>();
        private static List<BaseObj> bulletList = new List<BaseObj>();
        private static List<BaseObj> asteroidList = new List<BaseObj>();
        private static List<BaseObj> destroyedAsteroidList = new List<BaseObj>();
        private static Stack<string> logList = new Stack<string>();
        private static List<PlusLife> plusLifeList = new List<PlusLife>();
        private static Random rnd = new Random();
        private static int timeToSpawnLife = rnd.Next(MIN_TIME_TO_SPAWN_LIFE, MAX_TIME_TO_SPAWN_LIFE);
        private static bool gameStarted = false;
        private static int destroyedAsterods = 0;
        private static DateTime _t = DateTime.Now;
        private static DateTime _t1 = DateTime.Now;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Game() { }


        /////////////////////////////////////////////////////////////////////////////////////////////
        #region AddControls
        //Buttons objects and eventHandlers
        private static Label startBtn = new Label();
        private static Label recordsBtn = new Label();
        private static Label exitBtn = new Label();

        private static Label consoleLabel = new Label();

        /// <summary>
        /// Method adding controls
        /// </summary>
        /// <param name="form"></param>
        public static void AddControls(Form form)
        {
            //Using labels as buttons =) --------------------------------------------[]
            startBtn.Location = new Point(Width - 350, 10);
            startBtn.Size = new Size(350, 125);
            startBtn.TabIndex = 0;
            startBtn.Text = "Start";
            startBtn.BackColor = Color.DarkOrange;
            startBtn.TextAlign = ContentAlignment.MiddleCenter;
            startBtn.Font = new Font("Serif", 24, FontStyle.Bold);
            startBtn.Click += StartBtn_Click;
            form.Controls.Add(startBtn);

            recordsBtn.Location = new Point(Width - 350, 145);
            recordsBtn.Size = new Size(350, 125);
            recordsBtn.TabIndex = 0;
            recordsBtn.Text = "Record";
            recordsBtn.BackColor = Color.DarkOrange;
            recordsBtn.TextAlign = ContentAlignment.MiddleCenter;
            recordsBtn.Font = new Font("Serif", 24, FontStyle.Bold);
            recordsBtn.Click += RecordsBtn_Click;
            form.Controls.Add(recordsBtn);

            exitBtn.Location = new Point(Width - 350, 280);
            exitBtn.Size = new Size(350, 125);
            exitBtn.TabIndex = 0;
            exitBtn.Text = "Exit";
            exitBtn.BackColor = Color.DarkOrange;
            exitBtn.TextAlign = ContentAlignment.MiddleCenter;
            exitBtn.Font = new Font("Serif", 24, FontStyle.Bold);
            exitBtn.Click += ExitBtn_Click;
            form.Controls.Add(exitBtn);
            //Using labels as buttons =) --------------------------------------------[]


            //Console ---------------------------------------------------------------[]
            consoleLabel.Location = new Point(Width - 350, 500);
            consoleLabel.Size = new Size(350, 600);
            consoleLabel.TabIndex = 0;
            consoleLabel.Text = "Console";
            consoleLabel.ForeColor = Color.White;
            consoleLabel.BackColor = Color.Black;
            consoleLabel.TextAlign = ContentAlignment.TopLeft;
            consoleLabel.Font = new Font("Serif", 8, FontStyle.Bold);
            form.Controls.Add(consoleLabel);
            //Console ---------------------------------------------------------------[]
        }
        //Click handlers below
        private static void StartBtn_Click(object sender, EventArgs e)
        {
            if (startBtn.Text == "Start")
            {
                Load();
                startBtn.Text = "Stop";
                destroyedAsterods = 0;
                lives = LIVES_COUNT;
                if (plusLifeList.Count == 0)
                    plusLifeList.Add(new PlusLife(new Point(rnd.Next(1, 1500), rnd.Next(1, 1000)), new Point(lifeSpeed, lifeSpeed), new Size(0, 0)));

                gameStarted = true;
                LogConsole("Game STARTED");
            }
            else if (startBtn.Text == "Stop")
            {
                Records();
                startBtn.Text = "Start";
                gameStarted = false;
                LogConsole("You stopt the game.");
            }
        }
        private static void RecordsBtn_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;
            string record = File.ReadAllText($"{path}\\Records.txt");
            MessageBox.Show($"Record: {record}");
        }
        private static void ExitBtn_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop) Application.Exit(); else Environment.Exit(1);
        }
        #endregion AddControls

        /// <summary>
        /// Load method, loading objects in list
        /// </summary>
        public static void Load()
        {
            objList.Clear();
            bulletList.Clear();
            asteroidList.Clear();
            destroyedAsteroidList.Clear();
            logList.Clear();
            plusLifeList.Clear();

            //Порядок важен! это не фича а нехватка времени.
            #region BackGround
            objList.Add(new BackGround(new Point(rnd.Next(0, 0), rnd.Next(0, 0)), new Point(0, 0), new Size(0, 0)));
            #endregion BackGround

            #region Stars
            //Adding start in list
            for (int i = 0; i < 350; i++)
            {
                //int starSize = rnd.Next(3, 20);
                //int starSpeed = 2; //This variable means starSpeed, "Direction is not correct describtion"
                int starSize = 0;
                int starSpeed = 0;

                if (i < 250)
                {
                    starSpeed = rnd.Next(1, 2);
                    starSize = rnd.Next(1, 3);
                }
                else if (i >= 250 && i < 290)
                {
                    starSpeed = rnd.Next(2, 3);
                    starSize = rnd.Next(2, 4);
                }
                else if (i >= 290)
                {
                    starSpeed = rnd.Next(3, 4);
                    starSize = rnd.Next(4, 7);
                }

                objList.Add(new Star(new Point(rnd.Next(0, 1800), rnd.Next(0, 1000)), new Point(starSpeed, starSpeed), new Size(starSize, starSize)));
            }
            #endregion Stars

            #region PlanetPic
            //Adding planets in list
            int planetSize = 75;
            int planetSpeed = 1;
            for (int i = 1; i < 4; i++)
            {
                objList.Add(new PlanetPic(new Point(rnd.Next(0, 1800), rnd.Next(0, 1000)), new Point(planetSpeed, planetSpeed), new Size(planetSize, planetSize), i.ToString()));
                planetSize += 75;
                planetSpeed++;
            }
            #endregion PlanetPic

            #region Author
            objList.Add(new AuthorLogo(new Point(Width - 625, 10), new Point(0, 0), new Size(0, 0)));
            #endregion Author

            #region Asteroid

            int speed = rnd.Next(3, 8);
            for (int i = 1; i <= 30; i++)
                asteroidList.Add(new Asteroid(new Point(rnd.Next(1000, 1800), rnd.Next(0, 1000)), new Point(speed, speed), new Size(0, 0)));
            #endregion Asteroids

            #region PlayerSprite
            int playerSpeed = 1;
            objList.Add(new Player(new Point(5, Height / 3 + 70), new Point(playerSpeed, playerSpeed), new Size(0, 0)));
            #endregion PlayerSprite  

            #region PlusLife
            plusLifeList.Add(new PlusLife(new Point(rnd.Next(2000, 3000), rnd.Next(1, 1500)), new Point(lifeSpeed, lifeSpeed), new Size(0, 0)));
            #endregion
        }

        /// <summary>
        /// Init method. Initializing
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width - 350, Height));
            form.BackColor = Color.Black;
            Load();
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            if (form.Width > resolution.Width || form.Height > resolution.Height)
                throw new ArgumentOutOfRangeException($"Form size MAX Width: {resolution.Width} MAX Height: {resolution.Height}");

            form.ActiveControl = null;
            LogConsole("Feel free to CLOSE me or MOVE =)");

            Timer timer = new Timer { Interval = 24 };
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
        }

        /// <summary>
        /// KeyPressed event method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            short dir;
            //Enum сделаю потом когда буду допиливать и наводить красоту в коде
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                dir = 1;
                UpdateSprite(dir);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                dir = 2;
                UpdateSprite(dir);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                dir = 3;
                UpdateSprite(dir);
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                dir = 4;
                UpdateSprite(dir);
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (bulletList.Count <= 10)
                    Shot();

            }

            #region NotWorkingYes
            //Below not working YET!
            //Key Arrows
            //if (e.KeyCode == Keys.Up && e.KeyCode == Keys.Right)
            //{
            //    dir = 5;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.Up && e.KeyCode == Keys.Left)
            //{
            //    dir = 6;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.Down && e.KeyCode == Keys.Right)
            //{
            //    dir = 7;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.Down && e.KeyCode == Keys.Left)
            //{
            //    dir = 8;
            //    UpdateSprite(dir);
            //}

            ////Key Letters
            //if (e.KeyCode == Keys.W && e.KeyCode == Keys.D)
            //{
            //    dir = 5;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.W && e.KeyCode == Keys.A)
            //{
            //    dir = 6;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.S && e.KeyCode == Keys.D)
            //{
            //    dir = 7;
            //    UpdateSprite(dir);
            //}
            //else if (e.KeyCode == Keys.S && e.KeyCode == Keys.A)
            //{
            //    dir = 8;
            //    UpdateSprite(dir);
            //}
            #endregion NotWorkingYes
        }

        /// <summary>
        /// Generating bullets on screen
        /// </summary>
        private static void Shot()
        {
            Player player = (Player)objList.ElementAt(objList.Count() - 1);
            bulletList.Add(new Bullet(new Point(player.X + 150, player.Y + 47), new Point(0, 0), new Size(0, 0)));
            bulletList.Add(new Bullet(new Point(player.X + 150, player.Y + 115), new Point(0, 0), new Size(0, 0)));
        }

        /// <summary>
        /// Timer method that call methods on "Tick"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Draw method, calling Draw method from object
        /// </summary>
        public static void Draw()
        {
            foreach (var obj in objList)
                obj.Draw();

            foreach (var bullet in bulletList)
                bullet.Draw();

            foreach (var asteroid in asteroidList)
                asteroid.Draw();

            foreach (var asteroid in destroyedAsteroidList)
                asteroid.Draw();

            foreach (var obj in plusLifeList)
                obj.Draw();

            DrawHards();

            Buffer.Render();
        }

        /// <summary>
        /// Runing all UPDATING methods
        /// </summary>
        private static void Update()
        {
            CheckAsteroidsQTY();
            CheckPlusLifeQTY();
            UpdateObjects();
            UpdateBullets();
            IsBulletHitAsteroid();
            IsPlayertHitAsteroid();
            IsPlayertHitLife();
            UpdateDestroyedAsteroids();
        }

        /// <summary>
        /// Drawing hards-lives
        /// </summary>
        private static void DrawHards()
        {
            int x = Width / 2 - 200;
            for (int i = 0; i < lives; i++, x += 50)
                new Hard(new Point(x, 30), new Point(0, 0), new Size(0, 0)).Draw();
        }

        /// <summary>
        /// if is less than 3 asteroids, generating new, no more than 'ASTEROIDS_WAVES' = 5 asteroids
        /// </summary>
        private static void CheckAsteroidsQTY()
        {
            if (gameStarted)
            {
                if (asteroidList.Count < 20 && asteroidWaves > 0)
                {
                    for (int i = 1; i <= 5; i++)
                        asteroidList.Add(new Asteroid(new Point(rnd.Next(2000, 2200), rnd.Next(0, 1000)), new Point(rnd.Next(2, 5), rnd.Next(2, 5)), new Size(0, 0)));
                    asteroidWaves--;
                }
                else
                {
                    if (asteroidWaves == 0 && asteroidList.Count == 0)
                    {
                        asteroidWaves = ASTEROIDS_WAVES;
                        startBtn.Text = "Start";
                        gameStarted = false;
                        MessageBox.Show("YOU WIN!! Maximum asteroids was destroyed!", "YOU WIN!", MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        /// Checking for spawn or not new life
        /// </summary>
        private static void CheckPlusLifeQTY()
        {
            if (gameStarted)
            {
                _t1 = DateTime.Now;
                if ((_t1 - _t).TotalSeconds > timeToSpawnLife && plusLifeList.Count == 0)
                {
                    plusLifeList.Add(new PlusLife(new Point(rnd.Next(2000, 3000), rnd.Next(1, 1500)), new Point(lifeSpeed, lifeSpeed), new Size(0, 0)));
                    _t = _t1;
                }
            }

        }

        /// <summary>
        /// Checking for asteroid hit Player
        /// </summary>
        private static void IsPlayertHitAsteroid()
        {
            bool status = false;
            Player player = (Player)objList.ElementAt(objList.Count - 1);

            if (gameStarted)
            {
                int i = 0;
                foreach (Asteroid asteroid in asteroidList)
                {
                    i++;
                    if (
                            player.X + 150 >= asteroid.X &&
                            player.X <= asteroid.X + 70 &&
                            player.Y + 150 > asteroid.Y &&
                            player.Y <= asteroid.Y + 70
                       )
                    {
                        if (lives > 0)
                        {
                            lives -= 1;
                            LogConsole($"Lives left: {lives}");
                            Beep();
                        }
                        else
                        {

                            gameStarted = false;
                            startBtn.Text = "Start";
                            MessageBox.Show($"GAME OVER! destroyed asteroids: {destroyedAsterods}. Press Start for new game");
                            LogConsole($"GAME OVER! destroyed asteroids: {destroyedAsterods}. Press Start for new game");
                            Records();
                            destroyedAsterods = 0;
                            lives = LIVES_COUNT;
                        }
                        status = true;
                        break;
                    }
                }

                if (status)
                {
                    asteroidList.RemoveAt(i - 1);
                }
            }
        }

        /// <summary>
        /// Method check rekords and writing in file.
        /// </summary>
        private static void Records()
        {
            int asteroidDestroyedFromFile = -1;
            string path = Environment.CurrentDirectory;
            asteroidDestroyedFromFile = Convert.ToInt32(File.ReadAllText($"{path}\\{RECORDS_FILE_NAME}"));
            if (destroyedAsterods > asteroidDestroyedFromFile && asteroidDestroyedFromFile != -1)
                File.WriteAllText(RECORDS_FILE_NAME, destroyedAsterods.ToString());
        }

        /// <summary>
        /// Check Is player hit life or not, if yes then life++ if lives >= 3 than delete life
        /// </summary>
        private static void IsPlayertHitLife()
        {
            if (gameStarted)
            {
                bool status = false;
                Player player = (Player)objList.ElementAt(objList.Count - 1);

                int i = 0;
                foreach (PlusLife life in plusLifeList)
                {
                    i++;
                    if (
                            player.X + 150 >= life.X &&
                            player.X <= life.X + 50 &&
                            player.Y + 150 > life.Y &&
                            player.Y <= life.Y + 50
                       )
                    {
                        if (lives >= 0 && lives < 3)
                        {
                            lives++;
                            LogConsole($"You get one life!");
                            LogConsole($"Lives left: {lives}");
                            Beep();
                        }
                        else
                        {
                            LogConsole($"You can get life only if yo have less than 3 lifes!");
                            LogConsole($"Life destroyed, it should be like this =)");
                        }
                        status = true;
                        break;
                    }
                }

                if (status)
                {
                    plusLifeList.RemoveAt(i - 1);
                }
            }
        }

        /// <summary>
        /// Checking for bullet meet asteroid
        /// </summary>
        private static void IsBulletHitAsteroid()
        {
            if (gameStarted)
            {
                int i = 0, j = 0;
                bool del = false;

                foreach (Bullet bullet in bulletList)
                {
                    i++;
                    j = 0;
                    foreach (Asteroid aster in asteroidList)
                    {
                        j++;
                        if (
                                bullet.X > aster.X &&
                                bullet.X < aster.X + 70 &&
                                bullet.Y > aster.Y &&
                                bullet.Y < aster.Y + 70
                            )
                        {
                            del = true;
                            destroyedAsterods += 1;
                            Beep();
                            break;
                        }
                    }
                    if (del)
                        break;
                }

                if (del)
                {
                    string message = $"{DateTime.Now} Asteroid destroyed. Total: {destroyedAsterods}\r";
                    try
                    {
                        Asteroid asteroid = (Asteroid)asteroidList.ElementAt(i - 1);
                        //DrawDestroyedAsteroid(asteroid.X, asteroid.Y);
                        bulletList.RemoveAt(i - 1);
                        asteroidList.RemoveAt(j - 1);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error in asteroid vollection!");
                    }

                    LogConsole(message);
                    LogFileConsole(message);
                }
            }
        }

        /// <summary>
        /// Print message in console window
        /// </summary>
        /// <param name="message"></param>
        public static void LogConsole(string message)
        {
            logList.Push(message);
            string longString = String.Empty;
            foreach (string s in logList)
                longString += "\n" + s;
            consoleLabel.Text = longString;
        }

        /// <summary>
        /// Save message in LogFileConsole in root
        /// </summary>
        /// <param name="message"></param>
        public static void LogFileConsole(string message)
        {
            File.AppendAllText(CONSOLE_LOG_FILE_NAME, message);
        }

        /// <summary>
        /// Save message in Records file in root
        /// </summary>
        /// <param name="message"></param>
        public static void LogRecords(string message)
        {
            File.AppendAllText(RECORDS_FILE_NAME, message);
        }

        /// <summary>
        /// Drawing destroyed asteroid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void DrawDestroyedAsteroid(int x, int y)
        {
            destroyedAsteroidList.Add(new DesAsteroid(new Point(x, y), new Point(0, 0), new Size(0, 0)));
        }

        //Пока в разработке
        private static void UpdateDestroyedAsteroids()
        {
            foreach (var desAsteroid in asteroidList)
                desAsteroid.Update();
        }

        /// <summary>
        /// Updating list of baseObjects
        /// </summary>
        private static void UpdateObjects()
        {
            //Objects
            foreach (var obj in objList)
                obj.Update();

            foreach (var asteroid in asteroidList)
                asteroid.Update();

            foreach (var life in plusLifeList)
                life.Update();
        }

        /// <summary>
        /// Updating and deleting bullets from list
        /// </summary>
        private static void UpdateBullets()
        {
            //Bullets
            int i = 0;
            bool del = false;

            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
                if (bullet.X > Width)
                    del = true;
                i++;
            }

            //Deleting bullets from screen if X is bigger then Game.Width
            //And check how many bullets is in first row do delet. If there is one bullet than delet only one ELSE ExceptionOutOfRange
            if (bulletList.Count > 0)
            {
                if (del)
                {
                    if (bulletList.Count % 2 == 0)
                        for (i = 0; i < 2; i++)
                            bulletList.RemoveAt(0);
                    else
                        bulletList.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Updating Player sprite
        /// </summary>
        /// <param name="dir"></param>
        private static void UpdateSprite(short dir)
        {
            objList.ElementAt(objList.Count() - 1).UpdateSprite(dir);
        }

        private static void Beep()
        {
            //Console.Beep();
        }
    }
}
