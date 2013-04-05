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
        // Current movement speeds for player
        private Vector2 _movement;
        public Vector2 Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }

        // Current offset for centering map
        private Vector2 _offset;
        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        // Animation representing the player
        public Animation PlayerAnimation;

        // Does the player have armor
        private bool _armor;
        public bool Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        // Current health of the player
        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        // Maximum health of the player
        private int _maxHealth = 3;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        // Weapon object of the weapon the player is holding
        private Weapon _weapon;

        // The score for the current level
        private int _levelScore;
        public int LevelScore
        {
            get { return _levelScore; }
            set { _levelScore = value; }
        }

        // The score for the entire game
        private int _totalScore = 0;
        public int TotalScore
        {
            get { return _totalScore; }
            set { _totalScore = value; }
        }

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
            _health = this._maxHealth;

            // Set starting position of the player
            Position = position;
            //player Animation initialize
            PlayerAnimation.Initialize(spriteStrip, position, 64, 128, 2, 250, Color.White, 1.0f, true);
            // Set the player to be active
            PlayerAnimation.Active = true;
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
            _movement.X = 0;

            PlayerAnimation.state = Animation.Animate.IDLE;

            // Trying to move Left or Right
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PlayerAnimation.state = Animation.Animate.LMOVING;
                _movement.X = -5;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PlayerAnimation.state = Animation.Animate.RMOVING;
                _movement.X = 5;
            }

            //Keeping track of jumping/falling speed
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Position.Y == 372)
            {
                _movement.Y += -20;
            }

            _movement.Y += 1;

            //establish ceiling and floor
            if (Position.Y + _movement.Y < 0)//ceiling
            {
                Position = new Vector2(Position.X, 0);
            }
            else if (Position.Y + _movement.Y > 372)//floor
            {
                Position = new Vector2(Position.X, 372);
                _movement.Y = 0;
            }
            //establish left and right bound for "dead zone"
            if (Position.X + _movement.X > 500)
            {
                _offset.X += Position.X + _movement.X - 500;
                Position = new Vector2(500, Position.Y + _movement.Y);
            }
            else if (Position.X + _movement.X < 100 && _offset.X > 0)
            {
                _offset.X += Position.X + _movement.X - 100;
                Position = new Vector2(100, Position.Y + _movement.Y);
            }
            else
            {
                Position += _movement;
            }
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }

            PlayerAnimation.Update(gameTime);
            map.Update(gameTime, _offset);
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
        /// <param name="change">How much health to add</param>
        public void increaseHealth(int change)
        {
            if (this._health < this._maxHealth)
            {
                this._health += change;
            }
        }

        /// <summary>
        /// Decrease the health or armor of the player
        /// </summary>
        /// <param name="change">How much health to remove</param>
        public void decreaseHealth(int change)
        {
            if(hasArmor() && change == 1)
            {
                // Has armor and damaged 1
                this._armor = false;
            }
            else if (hasArmor() && change > 1)
            {
                // Has armor and damaged > 1
                this._armor = false;
                this._health -= (change - 1);
            }
            else if (this._health > 0)
            {
                // Has no armor
                this._health -= change;
            }
        }

        /// <summary>
        /// Adds armor to the player
        /// </summary>
        public void addArmor()
        {
            this._armor = true;
        }

        /// <summary>
        /// Increases the score
        /// </summary>
        /// <param name="amount"></param>
        public void increaseScore(int amount)
        {
            // Add the amount to the level score
            this._levelScore += amount;
        }

        /// <summary>
        /// Add level to total, then set level score to 0
        /// </summary>
        public void NextLevelScore()
        {
            this._totalScore += this._levelScore;
            this._levelScore = 0;
        }

        /// <summary>
        /// Get the current level score
        /// </summary>
        /// <returns>Current Level score</returns>
        public int getLevelScore()
        {
            return this._levelScore;
        }

        /// <summary>
        /// Get the Total Score
        /// </summary>
        /// <returns>Current Total score</returns>
        public int getTotalScore()
        {
            return this._totalScore;
        }

        /// <summary>
        /// Get the current health
        /// </summary>
        /// <returns>Current health</returns>
        public int getHealth()
        {
            return this._health;
        }

        /// <summary>
        /// Get the current weapon
        /// </summary>
        /// <returns>The weapon object</returns>
        public Weapon getWeapon()
        {
            return this._weapon;
        }

        /// <summary>
        /// Give the player a new weapon
        /// </summary>
        /// <param name="weaponType">1 for melee, 2 for ranged</param>
        public void setWeapon(int weaponType)
        {
            if (weaponType == 1)
            {
                _weapon = new Sword();
            }
            else if (weaponType == 2)
            {
                _weapon = new Boomerang();
            }
        }

        /// <summary>
        /// Get whether the player has armor or not
        /// </summary>
        /// <returns>True if yes</returns>
        public bool hasArmor()
        {
            return this._armor;
        }
    }
}