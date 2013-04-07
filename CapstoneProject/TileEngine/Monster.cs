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
        private Animation _monsterAnimation;

        // Current health
        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        // Maximum health
        private int _maxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        // Maximum damage
        private int _maxDamage;
        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }

        // Total width of the image
        private int _spriteWidth;
        public int SpriteWidth
        {
            get { return _spriteWidth; }
            set { _spriteWidth = value; }
        }

        // Total height of the image
        private int _spriteHeight;
        public int SpriteHeight
        {
            get { return _spriteHeight; }
            set { _spriteHeight = value; }
        }

        // Number of frames of the image
        private int _spriteFrame;
        public int SpriteFrame
        {
            get { return _spriteFrame; }
            set { _spriteFrame = value; }
        }

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            _monsterAnimation = new Animation();

            // Set starting position of the monster
            Position = position;

            // TODO: Need to set correct image/location
            _monsterAnimation.Initialize(spriteStrip, position, _spriteWidth, _spriteHeight, _spriteFrame, 250, Color.White, 1.0f, true);

            // Set the monster to be active
            _monsterAnimation.Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            _monsterAnimation.Position = Position;
            _monsterAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _monsterAnimation.Draw(spriteBatch);
            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Decrease the health of the monster
        /// </summary>
        /// <param name="change">The number of health to remove</param>
        public void decreaseHealth(int change)
        {
            this._health -= change;
        }

        /// <summary>
        /// Get the health of the monster
        /// </summary>
        /// <returns>The current health of the monster</returns>
        public int getHealth()
        {
            return this._health;
        }
    }
}