using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PROG2370Final
{
    public class Score : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected SpriteFont font;
        protected String message;
        protected Vector2 position;
        protected Color color;
        private int scoreInt = 0;

        private ShipClass ship;
        private Laser laser;
        private Monster monster;

        public Score(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 postion, Color color, ShipClass ship, Laser laser, Monster monster) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.color = color;
            this.ship = ship;
            this.laser = laser;
            this.monster = monster;
        }

        public override void Update(GameTime gameTime)
        {
            //if (Enabled)
            //{
            //    Rectangle laserRect = laser.getBoundary();
            //    Rectangle monsterRect = monster.getBoundary();
            //    Rectangle shipRect = ship.getBoundary();

            //    if (laserRect.Intersects(monsterRect))
            //    {
            //        laser.Visible = false;
            //        monster.Visible = false;
            //        scoreInt = scoreInt + 100;
            //    }

            //    if (shipRect.Intersects(monsterRect))
            //    {
            //        monster.Visible = false;
            //        ship.Visible = false;
            //        scoreInt = scoreInt - 100;
            //    }
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "SCORE: " + scoreInt.ToString(), position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
