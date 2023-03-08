/* Game1.cs
* Final Assignment
* Revision History
*       Ava Schembri-Kress, 2022.11.27: Created + Added Ship
*       Taylor Szabo, 2022.11.27: Added Scrolling background
*/
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace PROG2370Final
{
    public class Game1 : Game
    {
        //intialzing varibles
        private GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        SpriteFont textFont;

        //Texture2D yellowEvilMonster;
        //Texture2D evilShip;
        //Texture2D greenMonster;
        
        private Laser laser;
        public ShipClass ship;
        private Monster monster;
        private Score scoreLabel;
        private double time;
        private float delay = 1;
        bool lasersOn = false;
        List<Monster> monsters = new List<Monster>();
        List<Laser> lasers = new List<Laser>();
        Random random = new Random();

        bool gameState = true;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //making game full screen
            //_graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// loading sprites
        /// </summary
        public void LoadEnemies()
        {
            Random random2 = new Random();
            float yDiff = random2.Next(1, 300);//set boundry
            Texture2D monsterTexture = Content.Load<Texture2D>("yellowEvilMonster");
            Vector2 monsterPosition = new Vector2(650, yDiff);
            Vector2 monsterSpeed = new Vector2(1, 1);
            Vector2 shipStage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            if (spawn >= 1)
            {
                spawn = 0;
                if (monsters.Count < 4)
                {
                    monsters.Add(new Monster(this, _spriteBatch, monsterTexture, monsterPosition
                        , monsterSpeed, shipStage, laser));
                }
            }

            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].Visible)
                {
                    monsters.RemoveAt(i);
                    i--;
                }
            }

        }

        public void LoadLaser(GameTime gameTime)
        {
            Vector2 shipStage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Texture2D laserText = Content.Load<Texture2D>("laser");
            Vector2 laserPosition = new Vector2(shipStage.X / 8, shipStage.Y / 2);
            Vector2 laserSpeed = new Vector2(1, 1);

            time += gameTime.ElapsedGameTime.TotalSeconds;

            if (time >= delay)
            {
                time = 0;
                lasersOn = true;
            }
            if (lasersOn == true)
            {
                lasers.Add(new Laser(this, _spriteBatch, laserText, laserPosition, laserSpeed, ship, monster));
                lasersOn = false;
            }

            for (int i = 0; i < lasers.Count; i++)
            {
                if (!lasers[i].Visible)
                {
                    lasers.RemoveAt(i);
                    i--;
                }
            }

        }

        //public void Collision()
        //{
        //    Rectangle laserRect = laser.getBoundary();
        //    Rectangle monsterRect = monster.getBoundary();

        //    if (laserRect.Intersects(monsterRect))
        //    {
        //        laser.Visible = false;
        //        monster.Visible = false;
        //    }
        //}

        protected override void LoadContent()
        {
            //loading ship
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D shipTexture;
            Vector2 shipStage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 shipPosition = new Vector2(shipStage.X / 8, shipStage.Y / 2);
            Vector2 shipSpeed = new Vector2(1, 1);
            shipTexture = Content.Load<Texture2D>("spaceship");

            //loading 1st scrollingBackground
            Texture2D groundTex = this.Content.Load<Texture2D>("background1");
            Texture2D spaceTex1 = Content.Load<Texture2D>("background_1");
            Texture2D spaceTex2 = Content.Load<Texture2D>("background_2");

            Rectangle screenRect = new Rectangle(0, 0, spaceTex1.Width, spaceTex1.Height); ;
            Vector2 backgroundPos = new Vector2(0, shipStage.Y - screenRect.Height);

            Vector2 moveSpeed = new Vector2(0.5f, 0);
            ScrollingBackground space = new ScrollingBackground(this, _spriteBatch, spaceTex1, backgroundPos, screenRect, moveSpeed);

            Vector2 moveSpeed2 = new Vector2(0.25f, 0);
            ScrollingBackground space2 = new ScrollingBackground(this, _spriteBatch, spaceTex2, backgroundPos, screenRect, moveSpeed2);

            Rectangle srcRect = new Rectangle(0, 785, groundTex.Width, groundTex.Height - 800); ;
            Vector2 pos = new Vector2(0, shipStage.Y - srcRect.Height);
            Vector2 speed = new Vector2(2, 0);
            ScrollingBackground sb = new ScrollingBackground(this, _spriteBatch, groundTex, pos, srcRect, speed);

            Vector2 pos2 = new Vector2(0, shipStage.Y - srcRect.Height - 50);
            Vector2 speed2 = new Vector2(1, 0);
            ScrollingBackground sb2 = new ScrollingBackground(this, _spriteBatch, groundTex, pos2, srcRect, speed2);

            this.Components.Add(space);
            this.Components.Add(space2);
            this.Components.Add(sb2);
            this.Components.Add(sb);

            //loading font
           textFont = Content.Load<SpriteFont>("galleryFont");


            //loading ship
            ship = new ShipClass(this, _spriteBatch, shipTexture, shipPosition, shipSpeed, shipStage);

            scoreLabel = new Score(this, _spriteBatch, textFont, new Vector2(10, 10), Color.Red, ship, laser, monster);

            this.Components.Add(ship);
            this.Components.Add(scoreLabel);

            //Collision();
        }
        
        float spawn =0;
        protected override void Update(GameTime gameTime)
        {

            //reading escape key to exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //crating recangles
            //Rectangle shipRectangle = ship.getBoundary();
            //Rectangle monsterRectangle = monster.getBoundary();
            //Rectangle laserRectangle = laser.getBoundary();


            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach(Monster monster in monsters)
            {

                monster.Update(gameTime);
            }

            //foreach (Laser laser in lasers)
            //{
            //    laser.Update(gameTime);
            //}

            LoadEnemies();
            LoadLaser(gameTime); 
            //Collision();


            base.Update(gameTime);
        }

      

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //   //start game here 
            //}

            //loading spritebatch
            _spriteBatch.Begin();

            //loadin textfont
            //_spriteBatch.DrawString(textFont, "Player: ", new Vector2(0, 0), Color.Black);
            //_spriteBatch.DrawString(textFont, "Score: " + score.ToString(), new Vector2(0, 0), Color.Black);

            foreach (Monster monster in monsters)
            {
                monster.Draw(_spriteBatch);
            }

            foreach (Laser laser in lasers)
            {
                laser.Draw(_spriteBatch);
            }

            //ending spritebatch
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}