/* Game1.cs
* Final Assignment
* Anya Scheinman
* Ava Schembri-Kress
* Taylor Szabo
*/
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace PROG2370Final
{
    public class Game1 : Game
    {
        //intialzing varibles
        private GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        SpriteFont textFont;

        Texture2D yellowEvilMonster;
        Song laserSound;
        Song explosionSound;

        private Laser laser;
        public ShipClass ship;
        private Monster monster;
        private int monsterNumber = 2;
        int score = 0;
        string level = "Level: 1";

        List<Monster> monsters = new List<Monster>();
        Random random = new Random();

        bool gameState = true;
        bool isPressed = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// loading sprites
        /// </summary
        protected override void LoadContent()
        {
             GraphicsDevice.Clear(Color.Black);
           
            //loading ship
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D shipTexture;
            Vector2 shipStage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 shipPosition = new Vector2(shipStage.X / 8, shipStage.Y / 2);
            Vector2 shipSpeed = new Vector2(1, 1);
            shipTexture = Content.Load<Texture2D>("spaceship");

            //loading background
            Texture2D ground = this.Content.Load<Texture2D>("background1");
            Texture2D space1 = this.Content.Load<Texture2D>("stars1");
            Texture2D space2 = this.Content.Load<Texture2D>("stars2");

            Rectangle spaceRect = new Rectangle(0,0,space1.Width,space1.Height);
            Vector2 backgroundPos= new Vector2(0,shipStage.Y-spaceRect.Height);

            Vector2 spaceSpeed1 = new Vector2(0.5f, 0);
            ScrollingBackground spaceBackground = new ScrollingBackground(this, _spriteBatch, space1, backgroundPos, spaceRect, spaceSpeed1);

            Vector2 spaceSpeed2 = new Vector2(0.25f, 0);
            ScrollingBackground spaceBackground2 = new ScrollingBackground(this, _spriteBatch, space2, backgroundPos, spaceRect, spaceSpeed2);

            Rectangle srcRect = new Rectangle(0, 785, ground.Width, ground.Height - 800); ;
            Vector2 pos = new Vector2(0, shipStage.Y - srcRect.Height);
            Vector2 speed = new Vector2(2, 0);
            ScrollingBackground sb = new ScrollingBackground(this, _spriteBatch, ground, pos, srcRect, speed);

            Vector2 pos2 = new Vector2(0, shipStage.Y - srcRect.Height - 50);
            Vector2 speed2 = new Vector2(1, 0);
            ScrollingBackground sb2 = new ScrollingBackground(this, _spriteBatch, ground, pos2, srcRect, speed2);

            this.Components.Add(spaceBackground);
            this.Components.Add(spaceBackground2);
            this.Components.Add(sb2);
            this.Components.Add(sb);

            //loading font
            textFont = Content.Load<SpriteFont>("galleryFont");

            //loading ship
            ship = new ShipClass(this, _spriteBatch, shipTexture, shipPosition, shipSpeed, shipStage);

            //loading laser image
            Texture2D laserText = Content.Load<Texture2D>("laser");
            Vector2 laserPosition = new Vector2(shipStage.X / 8, shipStage.Y / 2);
            Vector2 laserSpeed = new Vector2(1, 1);

            //loading sound effect
            Song laserSound = this.Content.Load<Song>("laser-gun-72558");

            laser = new Laser(this, _spriteBatch, laserText, laserPosition,laserSpeed, ship, laserSound);

            //adding laser and ship
            this.Components.Add(laser);
            this.Components.Add(ship);
        }
        
        float spawn =0;


        /// <summary>
        /// loading levels and enemies
        /// </summary
        protected override void Update(GameTime gameTime)
        {
            //reading escape key to exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach(Monster monster in monsters)
            {
                monster.Update(gameTime);
            }

            //load levels
            Levels();

            //load enemies
            LoadEnemies();
            base.Update(gameTime);
        }


        /// <summary>
        /// loading levels 
        /// </summary
        public void Levels()
        {
            //if score is 500 add a monster
            if(score == 500)
            {
                monsterNumber = 3;
                level = "Level: 2";
            }
            //if score is 1000 add a monster
            else if (score == 1000)
            {
                level = "Level: 3";
                monsterNumber = 4;
            }
            //if score is 1500 game over
            else if (score == 1500)
            {
                level = "Game over";
                monsterNumber = 0;
            }
        }

        /// <summary>
        /// loading enemies
        /// this method adds monsters to the game varying on the levels
        /// also detects when to add or remove score
        /// </summary
        public void LoadEnemies()
        {
            Random random2 = new Random();
            float yDiff = random2.Next(1, 300);//set boundry
            Texture2D monsterTexture = Content.Load<Texture2D>("yellowEvilMonster");
            Vector2 monsterPosition = new Vector2(650, yDiff);
            Vector2 monsterSpeed = new Vector2(1, 1);
            Vector2 shipStage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Song explosionSound = this.Content.Load<Song>("explosionSound");

            //adding monster
            if (spawn >= 1)
            {
                spawn = 0;
                if (monsters.Count < monsterNumber)
                {
                    monsters.Add(new Monster(this, _spriteBatch, monsterTexture, monsterPosition, monsterSpeed, shipStage, laser, ship, explosionSound));
                }
            }

            //remove monsters and add score
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Enabled)
                {
                    if (!monsters[i].Visible)
                    {
                        score = score + 100;
                        monsters.RemoveAt(i);
                        i--;
                    }
                }
                //remove monster and subtract score
                else
                {
                    score = score - 50;
                    monsters.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Draw
        /// this method starts game and draws sprites and text
        /// </summary
        protected override void Draw(GameTime gameTime)
        {
            //loading spritebatch
            GraphicsDevice.Clear(Color.Black);

            //getting input to start game 
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
            {
                isPressed = true;
            }

            //if enter is pressed draw monsters and text
            if(isPressed == true)
            {
                _spriteBatch.Begin();

                //loading textfont
                _spriteBatch.DrawString(textFont, level.ToString(), new Vector2(0,0), Color.Red);

                //drawing monsters
                if (laser.Enabled)
                    foreach (Monster monster in monsters)
                    {
                        monster.Draw(_spriteBatch);
                    }

                _spriteBatch.DrawString(textFont, "Score: " + score.ToString(), new Vector2(0,50), Color.Red);

                //ending spritebatch
                _spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }
    }
}