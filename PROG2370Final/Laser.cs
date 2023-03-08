/*Laser.ca
* Final Assignment
* Anya Scheinman
* Ava Schembri-Kress
* Taylor Szabo
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace PROG2370Final
{
    internal class Laser : DrawableGameComponent
    {
        //initializing variables
        public SpriteBatch laserSprite;
        private Texture2D laserTexture;
        private Vector2 laserPosition;
        private Vector2 laserSpeed;
        private Vector2 laserOrigin;
        private float laserScale = 1.5f;
        private float laserRotation = 0f;
        private Rectangle laserRectangle;
        ShipClass ship;
        private Laser laser;
        bool isFired = false;
        Song laserSound;

        //creating laser class
        public Laser(Game game, SpriteBatch laserSprite, Texture2D laserTexture, Vector2 laserPosition, Vector2 laserSpeed, ShipClass ship, Song laserSound) : base(game)
        {
            this.laserSprite = laserSprite;
            this.laserTexture = laserTexture;
            this.laserPosition = laserPosition;
            this.laserSpeed = laserSpeed;
            laserOrigin = new Vector2(laserTexture.Width / 2, laserTexture.Height / 2);
            laserRectangle = new Rectangle(0, 0, laserTexture.Width, laserTexture.Height);
            this.ship = ship;
            this.laserSound = laserSound;
        }

        public override void Draw(GameTime gameTime)
        {
            //drawing laser image
            laserSprite.Begin();

            //draw laser
            laserSprite.Draw(laserTexture, laserPosition, laserRectangle, Color.White, laserRotation, laserOrigin, laserScale, 
             SpriteEffects.None, 0);

            //end spritebatch
            laserSprite.End();
        }
        public override void Update(GameTime gameTime)
        {
            //if laser is not fired
            if (isFired == false)
            {
                //find ship position
                laserPosition = ship.getPosition();
            }

            //if laser is fired by spacebar
            else
            {
                //move laser across screen
                laserPosition.X = laserPosition.X + 5;
                if(laserPosition.X > 800)
                {
                    isFired= false;
                }
            }

            if (laserPosition.X > 800 - laserTexture.Width)
            {
                this.Visible = true;
            }

            //getting keyboard state
            KeyboardState keyState = Keyboard.GetState();
            {
                //shoot and trigger fire
                if (keyState.IsKeyDown(Keys.Space))
                {
                    isFired= true;
                    MediaPlayer.Play(laserSound);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// getBoundary method
        /// creates rectangle around laser sprite
        /// </summary>
        public Rectangle getBoundary()
        {
            return new Rectangle((int)laserPosition.X, (int)laserPosition.Y, laserTexture.Width, laserTexture.Height);
        }
    }
}
