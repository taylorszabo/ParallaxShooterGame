﻿/* SpaceShip.cs
* Final Assignment
* Revision History
*       Taylor Szabo, 2022.11.27: Created
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
    internal class ScrollingBackground : DrawableGameComponent
    {
        //initializing varibles
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position1, position2;
        private Vector2 speed;

        /// <summary>
        /// creating scrolling background
        /// </summary
        public ScrollingBackground(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Rectangle srcRect,
            Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position1 = position;
            this.position2 = new Vector2(position.X + srcRect.Width, position.Y);
            this.speed = speed;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // v 4
            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// scrolling the background
        /// </summary
        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;
            if (position1.X < -srcRect.Width)
            {
                position1.X = position2.X + srcRect.Width;
            }

            if (position2.X < -srcRect.Width)
            {
                position2.X = position1.X + srcRect.Width;
            }

            base.Update(gameTime);
        }
    }
}
