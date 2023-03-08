/*Laser.ca
* Final Assignment
* Revision History
*       Ava Schembri-Kress, 2022.12.01: Created      
*       Taylor Szabo, 2022.12.01: Debugged
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace PROG2370Final
{
    public class Laser : DrawableGameComponent
    {
        //initializing variables
        private SpriteBatch laserSprite;
        private Texture2D laserTexture;
        private Vector2 laserPosition;
        private Vector2 laserSpeed;
        private Vector2 laserOrigin;
        private float laserScale = 1;
        private float laserRotation = 0f;
        private Rectangle laserRectangle;
        private ShipClass ship;
        private Monster monster;
        bool isFired = false;

        //creating laser class
        public Laser(Game game, SpriteBatch laserSprite, Texture2D laserTexture, Vector2 laserPosition, Vector2 laserSpeed, ShipClass ship, Monster monster) : base(game)
        {
            this.laserSprite = laserSprite;
            this.laserTexture = laserTexture;
            this.laserPosition = laserPosition;
            this.laserSpeed = laserSpeed;
            laserOrigin = new Vector2(laserTexture.Width / 2, laserTexture.Height / 2);
            laserRectangle = new Rectangle(0, 0, laserTexture.Width, laserTexture.Height);
            this.ship = ship;
            this.monster = monster;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            laserSprite.Draw(laserTexture, laserPosition, laserRectangle, Color.White, laserRotation, laserOrigin, laserScale, 
             SpriteEffects.None, 0);

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

                if (laserPosition.X > 800)
                {
                    isFired = false;
                }
            }

            KeyboardState keyState = Keyboard.GetState();
            {
                //shoot and trigger fire
                if (keyState.IsKeyDown(Keys.Space))
                {
                    isFired= true;

                }

            }


            base.Update(gameTime);
        }
        public Rectangle getBoundary()
        {
            return new Rectangle((int)laserPosition.X, (int)laserPosition.Y, laserTexture.Width, laserTexture.Height);
        }
    }
}
