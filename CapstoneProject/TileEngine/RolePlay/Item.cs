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
        public Animation _itemAnimation;

        public bool iActive = true;
        public bool draw = true;

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

        // State of the monster
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private int _weapon = 0;

        public int Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        // How much score is increased from this item
        private int _score = 0;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        // How much health is increased from this item
        private int _health = 0;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        // Does this item give armor or not
        private bool _armor = false;

        public bool Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        private float _scale;

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        //position relative to player
        Vector2 relPosition = new Vector2(0, 0);

        //whether item is on screen;
        bool active = false;

        // The image of the item
        public Texture2D SpriteStrip;

        public Item() { }

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            _itemAnimation = new Animation();
            SpriteStrip = spriteStrip;
            // Set starting position of the monster
            Position = position;

            // TODO: Need to set correct image/location
            _itemAnimation.Initialize(spriteStrip, position, _spriteWidth, _spriteHeight, _spriteFrame, 250, Color.White, _scale, true, Animation.Animate.ITEM);

            // Set the monster to be active
            _itemAnimation.Active = true;
        }

        public virtual void Update(GameTime gameTime, Vector2 player, Vector2 offset)
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
                _itemAnimation.Position = relPosition;
                _itemAnimation.Update(gameTime);
            }
            X = Position.X;
            Y = Position.Y;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _itemAnimation.Draw(spriteBatch);
            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Get the amount of health this item restores
        /// </summary>
        /// <returns>Health increased</returns>
        public int getHealth()
        {
            return _health;
        }

        /// <summary>
        /// Get the amount of score this item increases
        /// </summary>
        /// <returns>Score increased</returns>
        public int getScore()
        {
            return _score;
        }

        /// <summary>
        /// Get whether this item is armor
        /// </summary>
        /// <returns>True for armor</returns>
        public bool getArmor()
        {
            return _armor;
        }
    }
}