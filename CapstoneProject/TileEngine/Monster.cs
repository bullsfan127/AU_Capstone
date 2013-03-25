﻿using System;
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
    internal class Monster : Avatar
    {
        // Animation representing the monster
        public Animation MonsterAnimation;

        // State of the monster
        public bool Active;

        // Current health of the monster
        private int _health;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
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
            this._health += change;

            if (this._health < 0)
            {
                this._health = 0;
            }
        }

        public int getHealth()
        {
            return this._health;
        }
    }
}