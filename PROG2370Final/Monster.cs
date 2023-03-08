/*Monster.ca
* Final Assignment
* Anya Scheinman
* Ava Schembri-Kress
* Taylor Szabo
*/
using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2370Final
{
    internal class Monster : DrawableGameComponent
    {
        //initializing variables
        private SpriteBatch monsterSprite;
        private Texture2D monsterTexture;
        private Vector2 monsterPosition;
        private Vector2 monsterSpeed;
        private float monsterScale = 1.5f;
        private float monsterRotation = 0f;
        private Vector2 monsterOrigin;
        private Vector2 monsterStage;
        private Rectangle screenRectangle;
        Song explosionSound;

        float yPosition;

        private Laser laser;
        private ShipClass ship;

        /// <summary>
        /// creating Monsters
        /// </summary
        public Monster(Game game, SpriteBatch monsterSprite, Texture2D monsterTexture, Vector2 monsterPosition, Vector2 monsterSpeed, Vector2 monsterStage, Laser laser, ShipClass ship, Song explosionSound) : base(game)
        {
            this.monsterSprite = monsterSprite;
            this.monsterTexture = monsterTexture;
            this.monsterPosition = monsterPosition;
            this.monsterSpeed = monsterSpeed;
            this.monsterStage = monsterStage;
            screenRectangle = new Rectangle(0, 0, monsterTexture.Width, monsterTexture.Height);
            monsterOrigin = new Vector2(monsterTexture.Width / 2, monsterTexture.Height / 2);
            this.laser = laser;
            this.ship = ship;
            this.explosionSound = explosionSound;
        }

        /// <summary>
        /// Draw
        /// drawing monster sprite
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            monsterSprite.Draw(monsterTexture, monsterPosition, screenRectangle, Color.White, monsterRotation, monsterOrigin, monsterScale,
            SpriteEffects.None, 0);
        }

        /// <summary>
        /// creating the shooting aspect of the game
        /// </summary
        public override void Update(GameTime gameTime)
        {
            //find position of monster, ship and laser
            monsterPosition.X = monsterPosition.X - 1;
            Rectangle laserRect = laser.getBoundary();
            Rectangle shipRect = ship.getBoundary();
            Rectangle monsterRect = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, monsterTexture.Width, monsterTexture.Height);

            if (monsterPosition.X < 0 - monsterTexture.Width)
            {
                this.Enabled = false;
            }

            //if laser hits monster, they both dissapear
            if (laserRect.Intersects(monsterRect))
            {
                laser.Visible = false;
                this.Visible = false;

                //make noise
                MediaPlayer.Play(explosionSound);
            }

            //if ship hits monster, monster dissapears
            if (shipRect.Intersects(monsterRect))
            {
                this.Enabled = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// getBoundary method
        /// creates rectangle around ship sprite
        /// </summary
        public Rectangle getBoundary()
        {
            return new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, monsterTexture.Width, monsterTexture.Height);
        }
    }
}
