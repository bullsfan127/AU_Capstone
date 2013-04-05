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
        private Animation _weaponAnimation;

        // Total damage the weapon can do
        private int _maxDamage;
        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }

        // Width of the full image
        private int _spriteWidth;
        public int SpriteWidth
        {
            get { return _spriteWidth; }
            set { _spriteWidth = value; }
        }

        // Height of the full image
        private int _spriteHeight;
        public int SpriteHeight
        {
            get { return _spriteHeight; }
            set { _spriteHeight = value; }
        }

        // Total number of frames of the image
        private int _spriteFrame;
        public int SpriteFrame
        {
            get { return _spriteFrame; }
            set { _spriteFrame = value; }
        }


        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            _weaponAnimation = new Animation();

            // Set starting position of the weapon
            Position = position;

            // TODO: Need to set correct image/location
            _weaponAnimation.Initialize(spriteStrip, position, _spriteWidth, _spriteHeight, _spriteFrame, 250, Color.White, 1.0f, true);
        }

        public override void Update(GameTime gameTime)
        {
            _weaponAnimation.Position = Position;
            base.Update(gameTime);
            _weaponAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            _weaponAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Get the damage of the weapon
        /// </summary>
        /// <returns>The damage the weapon can do</returns>
        public int getDamage()
        {
            return _maxDamage;
        }

    }
}