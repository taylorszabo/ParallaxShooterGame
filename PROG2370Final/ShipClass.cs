/*ShipClass.ca
* Final Assignment
* Anya Scheinman
* Ava Schembri-Kress
* Taylor Szabo
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace PROG2370Final
{
    public class ShipClass : DrawableGameComponent
    {
        //initializing variables
        private SpriteBatch shipSprite;
        private Texture2D shipTexture;
        private Vector2 shipPosition;
        private Vector2 shipSpeed;
        private float shipScale = 0.1f;
        private float shipRotattion = 0f;
        private Vector2 shipOrigin;
        private Vector2 shipStage;
        private Rectangle screenRectangle;

        /// <summary>
        /// creating SpaceShip
        /// </summary
        public ShipClass (Game game, SpriteBatch shipSprite, Texture2D shipTexture ,Vector2 shipPosition, Vector2 shipSpeed, Vector2 shipStage) : base (game)
        {
            this.shipSprite = shipSprite;
            this.shipTexture = shipTexture;
            this.shipPosition = shipPosition;
            this.shipSpeed = shipSpeed;
            this.shipStage= shipStage;
            screenRectangle = new Rectangle(0, 0, shipTexture.Width, shipTexture.Height);
            shipOrigin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);
        }

        /// <summary>
        /// Draw 
        /// draws ship sprite
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //loading spriteBatch
            shipSprite.Begin();

            //drawing ship
            shipSprite.Draw(shipTexture, shipPosition, screenRectangle, Color.White, shipRotattion ,shipOrigin, shipScale,
               SpriteEffects.None, 0);

            //ending spriteBatch
            shipSprite.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// moving ship with keyboard
        /// </summary
        public override void Update(GameTime gameTime)
        {
            //getting keyboard state
            KeyboardState keyState = Keyboard.GetState();

            //moving ship up
            if(keyState.IsKeyDown(Keys.Up))
            {
                shipPosition.Y -= 5;
                //setting game boundary
                if (shipPosition.Y <= 20)
                {
                    shipPosition.Y = 20;
                }
            }

            //moving ship down
            if (keyState.IsKeyDown(Keys.Down))
            {
                shipPosition.Y += 5;
                //setting game boundary
                if (shipPosition.Y >= shipTexture.Height)
                {
                    shipPosition.Y = shipTexture.Height;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// getBoundary method
        /// creates rectangle around ship sprite
        /// </summary
        public Rectangle getBoundary()
        {
            return new Rectangle((int)shipPosition.X, (int)shipPosition.Y, shipTexture.Width / 7, shipTexture.Height / 7);
        }

        /// <summary>
        /// getPosition method
        /// return position of ship sprite
        /// </summary
        public Vector2 getPosition()
        {
            return shipPosition;
        }
    }
}