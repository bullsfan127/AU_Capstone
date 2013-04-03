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
    public class Player : Avatar
    {
        //Current movement speeds for player
        public Vector2 Movement;

        //current offset for centering map
        public Vector2 offset;

        // Animation representing the player
        public Animation PlayerAnimation;

        // State of the player
        public bool Active;

        // Does player have armor
        public bool armor;

        // Current health of the player
        public int health;

        // Maximum health of the player
        public int maxHealth = 3;

        // Weapon object of the weapon the player is holding
        public Weapon weapon;

        // The score for the current level
        public int levelScore;

        // The score for the entire game
        public int totalScore = 0;

        /// <summary>
        /// Default constructor for player
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Initializes the player
        /// </summary>
        /// <param name="spriteStrip"></param>
        /// <param name="position"></param>
        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            PlayerAnimation = new Animation();

            // Set the player's health to the maximum
            health = this.maxHealth;

            // Set starting position of the player
            Position = position;
            //player Animation initialize
            PlayerAnimation.Initialize(spriteStrip, position, 64, 128, 2, 250, Color.White, 1.0f, true);
            // Set the player to be active
            Active = true;
        }

        /// <summary>
        /// Update the player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="map"></param>
        public void Update(GameTime gameTime, Map map)
        {
            // Vector2 Position = Vector2.Zero;
            PlayerAnimation.Position = Position;

            //PlayerAnimation.Update(gameTime);

            base.Update(gameTime);

            //Reset movement to still
            Movement.X = 0;

            PlayerAnimation.state = Animation.Animate.IDLE;

            // Trying to move Left or Right
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PlayerAnimation.state = Animation.Animate.LMOVING;
                Movement.X = -5;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PlayerAnimation.state = Animation.Animate.RMOVING;
                Movement.X = 5;
            }

            //Keeping track of jumping/falling speed
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Position.Y == 372)
            {
                Movement.Y += -20;
            }

            Movement.Y += 1;

            //establish ceiling and floor
            if (Position.Y + Movement.Y < 0)//ceiling
            {
                Position = new Vector2(Position.X, 0);
            }
            else if (Position.Y + Movement.Y > 372)//floor
            {
                Position = new Vector2(Position.X, 372);
                Movement.Y = 0;
            }
            //establish left and right bound for "dead zone"
            if (Position.X + Movement.X > 500)
            {
                offset.X += Position.X + Movement.X - 500;
                Position = new Vector2(500, Position.Y + Movement.Y);
            }
            else if (Position.X + Movement.X < 100 && offset.X > 0)
            {
                offset.X += Position.X + Movement.X - 100;
                Position = new Vector2(100, Position.Y + Movement.Y);
            }
            else
            {
                Position += Movement;
            }
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }

            PlayerAnimation.Update(gameTime);
            map.Update(gameTime, offset);
        }

        /// <summary>
        /// Draw the player
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            PlayerAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Increase the health of the player
        /// </summary>
        /// <param name="change"></param>
        /// 

        public void increaseHealth(int change)
        {
            if (this.health < this.maxHealth)
            {
                this.health += change;
            }
        }

        /// <summary>
        /// Decrease the health or armor of the player
        /// </summary>
        /// <param name="change"></param>
        public void decreaseHealth(int change)
        {
            
            if(hasArmor() && change == 1)
            {
                // Has armor and damaged 1
                this.armor = false;
            }
            else if (hasArmor() && change > 1)
            {
                // Has armor and damaged > 1
                this.armor = false;
                this.health -= (change - 1);
            }
            else if (this.health > 0)
            {
                // Has no armor
                this.health -= change;
            }
        }

        public void addArmor()
        {
            this.armor = true;
        }

        /// <summary>
        /// Increases the score
        /// </summary>
        /// <param name="amount"></param>
        public void increaseScore(int amount)
        {
            // Add the amount to the level score
            this.levelScore += amount;
        }

        /// <summary>
        /// Add level to total, then set level score to 0
        /// </summary>
        public void NextLevelScore()
        {
            this.totalScore += this.levelScore;
            this.levelScore = 0;
        }

        /// <summary>
        /// Get the current level score
        /// </summary>
        /// <returns></returns>
        public int getLevelScore()
        {
            return this.levelScore;
        }

        /// <summary>
        /// Get the Total Score
        /// </summary>
        /// <returns></returns>
        public int getTotalScore()
        {
            return this.totalScore;
        }

        /// <summary>
        /// Get the current health
        /// </summary>
        /// <returns></returns>
        public int getHealth()
        {
            return this.health;
        }

        /// <summary>
        /// Get the  current weapon
        /// </summary>
        /// <returns></returns>
        public Weapon getWeapon()
        {
            return this.weapon;
        }

        public void setWeapon(int weaponType)
        {
            if (weaponType == 1)
            {
                weapon = new Sword();
            }
            else if (weaponType == 2)
            {
                weapon = new Boomerang();
            }
        }

        /// <summary>
        /// Get whether the player has armor or not
        /// </summary>
        /// <returns></returns>
        public bool hasArmor()
        {
            return this.armor;
        }
    }
}