/*Monster.ca
* Final Assignment
* Revision History
*       Ava Schembri-Kress, 2022.11.28: Created      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2370Final
{
    public class Monster : DrawableGameComponent
    {
        //initializing variables
        private SpriteBatch monsterSprite;
        //private SpriteBatch EvilShip;
        //private SpriteBatch greenMonster;
        private Texture2D monsterTexture;
        private Vector2 monsterPosition;
        private Vector2 monsterSpeed;
        private float monsterScale = 1.5f;
        private float monsterRotation = 0f;
        private Vector2 monsterOrigin;
        private Vector2 monsterStage;
        private Rectangle screenRectangle;
        private Laser laser;




        /// <summary>
        /// creating Monsters
        /// </summary
        public Monster(Game game, SpriteBatch monsterSprite, Texture2D monsterTexture, Vector2 monsterPosition, Vector2 monsterSpeed, Vector2 monsterStage, Laser laser) : base(game)
        {
            this.monsterSprite = monsterSprite;
            this.monsterTexture = monsterTexture;
            this.monsterPosition = monsterPosition;
            this.monsterSpeed = monsterSpeed;
            this.monsterStage = monsterStage;
            this.laser = laser;
            screenRectangle = new Rectangle(0, 0, monsterTexture.Width, monsterTexture.Height);
            monsterOrigin = new Vector2(monsterTexture.Width / 2, monsterTexture.Height / 2);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
                //drawing ship
                monsterSprite.Draw(monsterTexture, monsterPosition, screenRectangle, Color.White, monsterRotation, monsterOrigin, monsterScale,
                   SpriteEffects.None, 0);
            
        }
        public override void Update(GameTime gameTime)
        {
            monsterPosition.X = monsterPosition.X - 1;

            //Rectangle laserRect = laser.getBoundary();
            //Rectangle monsterRect = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, monsterTexture.Width / 2, monsterTexture.Height / 2);
            if (monsterPosition.X < 0 - monsterTexture.Width)
            {
                this.Visible = false;
            }

            //if (laserRect.Intersects(monsterRect))
            //{
            //    this.Visible = false;
            //    laser.Visible = false;
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// getBoundary method
        /// </summary
        public Rectangle getBoundary()
        {
            return new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, monsterTexture.Width / 2, monsterTexture.Height / 2);
        }

    }
}
