using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TileEngine
{
    public class Potion : Item
    {

        //position relative to player
        Vector2 relPosition = new Vector2(0, 0);

        //whether item is on screen;
        bool active = false;

        /// <summary>
        /// Potion class - Increases health by 1
        /// </summary>
        public Potion()
        {
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            Health = 1;
            Scale = 0.6f;
        }

        public override void Update(GameTime gameTime, Vector2 player, Vector2 offset)
        {
            //is item on screen check
            if (Position.X - offset.X < 600 && Position.X - offset.X > -70 && Position.Y - offset.Y < 600 && Position.Y - offset.Y > 0)
            {
                active = true;
            }
            else
                active = false;
            //if item on screen
            if (active)
            {
                //define relative position
                relPosition = Position - offset;
            }
            X = Position.X;
            Y = Position.Y;
            base.Update(gameTime);
        }
    }
}