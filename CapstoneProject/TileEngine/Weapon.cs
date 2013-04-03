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
    public class Weapon : Avatar
    {
        // Animation representing the weapon
        public Animation WeaponAnimation;

        public int maxDamage;
        public int spriteWidth;
        public int spriteHeight;
        public int spriteFrame;

        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            WeaponAnimation = new Animation();

            // Set starting position of the player
            Position = position;

            // TODO: Need to set correct image/location
            WeaponAnimation.Initialize(spriteStrip, position, spriteWidth, spriteHeight, spriteFrame, 250, Color.White, 1.0f, true);
        }

        public override void Update(GameTime gameTime)
        {
            WeaponAnimation.Position = Position;
            base.Update(gameTime);
            WeaponAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            WeaponAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        public int getDamage()
        {
            return maxDamage;
        }

    }
}