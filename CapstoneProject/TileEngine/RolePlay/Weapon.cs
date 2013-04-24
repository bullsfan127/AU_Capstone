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
        //private Animation _weaponAnimation;

        // Total damage the weapon can do
        private int _maxDamage;

        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }

        public Rectangle wepRect= Rectangle.Empty;
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

        private float _scale;

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        // The image of the item
        private Texture2D SpriteStrip;

        private int _cropStart = 64;
        private int _cropPos = 50;

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            //_weaponAnimation = new Animation();

            // Set starting position of the weapon
            Position = position;
            SpriteStrip = spriteStrip;

            // TODO: Need to set correct image/location
            // _weaponAnimation.Initialize(spriteStrip, position, _spriteWidth, _spriteHeight, _spriteFrame, 250, Color.White, 1.0f, true);
        }

        public void Update(GameTime gameTime, Vector2 Position)
        {
            float x = Position.X;
            float y = Position.Y;
            Vector2 weaponPosition = new Vector2(x + _cropPos, y + 25);
            this.Position = weaponPosition;
            //_weaponAnimation.Position = weaponPosition;
            base.Update(gameTime);
            //_weaponAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            //_weaponAnimation.Draw(spriteBatch);

            //base.Draw(spriteBatch, gameTime);

            spriteBatch.Begin();
            // SpriteStip is image
            // Position is x,y vector of image location
            // 0.4f is scaling of image
            //spriteBatch.Draw(SpriteStrip, Position, null, Color.White, 0.0f, Vector2.Zero, _scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(SpriteStrip, Position, new Rectangle(_cropStart, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, _scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }

        /// <summary>
        /// Get the damage of the weapon
        /// </summary>
        /// <returns>The damage the weapon can do</returns>
        public int getDamage()
        {
            return _maxDamage;
        }

        public void setDirection(int d)
        {
            if (d == 1)
            {
                _cropStart = 64;
                _cropPos = 47;
            }
            else
            {
                _cropStart = 0;
                _cropPos = -47;
            }
        }
    }
}