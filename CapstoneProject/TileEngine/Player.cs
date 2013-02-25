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
    class Player : Avatar
    {
        // Animation representing the player
        public Animation PlayerAnimation;
        
        // Position of the Player
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Current health of the player
        protected int health;

        // Maximum health of the player
        protected int maxHealth = 3;

        // ID of the weapon the player is holding
        protected int weapon;

        // The score for the current level
        protected int levelScore;

        // The score for the entire game
        protected int totalScore = 0;


        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;

            // Set the player's health to the maximum
            health = this.maxHealth;

            // Set starting position of the player
            Position = position;

            // Set the player to be active
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            PlayerAnimation.Draw(spriteBatch);
        }

        public void changeHealth(int change)
        {
            // Add/subtract the change to the current health
            this.health += change;

            // More than max? Set to max
            if (this.health > this.maxHealth)
            {
                this.health = this.maxHealth;
            } // Less than 0? Set to 0.
            else if (this.health < 0)
            {
                this.health = 0;
            }
        }

        public void changeWeapon(int weapon)
        {
            // TODO - Do we need to update the graphic here too
            // or is that done at the next "update" command?
            this.weapon = weapon;
        }

        public void increaseScore(int amount)
        {
            // Add the amount to the level score
            this.levelScore += amount;
        }

        public void NextLevelScore()
        {
            // Add level to total, then set level to 0
            this.totalScore += this.levelScore;
            this.levelScore = 0;
        }

        public int getLevelScore()
        {
            return this.levelScore;
        }

        public int getTotalScore()
        {
            return this.totalScore;
        }

        public int getHealth()
        {
            return this.health;
        }

        public int getWeapon()
        {
            return this.weapon;
        }

    }
}
