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
    public class Item : Avatar
    {
        // Animation representing the monster
        public Animation ItemAnimation;

        // State of the monster
        public bool Active;

        public int spriteWidth;
        public int spriteHeight;
        public int spriteFrame;
        public int score = 0;
        public int health = 0;
        public int armor = 0;
        public int weapon = 0;

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            ItemAnimation = new Animation();

            // Set starting position of the player
            Position = position;

            // TODO: Need to set correct image/location
            ItemAnimation.Initialize(spriteStrip, position, spriteWidth, spriteHeight, spriteFrame, 250, Color.White, 1.0f, true);

            // Set the player to be active
            Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            ItemAnimation.Position = Position;
            base.Update(gameTime);
            ItemAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            ItemAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        public int getHealth()
        {
            return health;
        }

        public int getScore()
        {
            return score;
        }

        public int getArmor()
        {
            return armor;
        }
    }
}