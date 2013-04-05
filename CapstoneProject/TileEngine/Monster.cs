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
    public class Monster : Avatar
    {
        // Animation representing the monster
        public Animation MonsterAnimation;

        // State of the monster
        public bool Active;

        // Current health of the monster
        public int health;

        public int maxHealth;
        public int maxDamage;
        public int spriteWidth;
        public int spriteHeight;
        public int spriteFrame;

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            MonsterAnimation = new Animation();

            // Set starting position of the player
            Position = position;

            // TODO: Need to set correct image/location
            MonsterAnimation.Initialize(spriteStrip, position, spriteWidth, spriteHeight, spriteFrame, 250, Color.White, 1.0f, true);

            // Set the player to be active
            Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            MonsterAnimation.Position = Position;
            base.Update(gameTime);
            MonsterAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            MonsterAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        public void changeHealth(int change)
        {
            // Add/subtract the change to the current health
            this.health += change;

            if (this.health < 0)
            {
                this.health = 0;
            }
        }

        public int getHealth()
        {
            return this.health;
        }
    }
}