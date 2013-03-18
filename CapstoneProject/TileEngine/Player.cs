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
        public Animation WalkAnimation;

        // State of the player
        public bool Active;

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

        // ID of the weapon the player is holding
        private int _weapon;

        public int Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

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

        public Player()
        {
        }

        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            PlayerAnimation = new Animation();
            WalkAnimation = new Animation();

            // Set the player's health to the maximum
            _health = this._maxHealth;

            // Set starting position of the player
            Position = position;
            //player Animation initialize
            PlayerAnimation.Initialize(spriteStrip, position, 64, 128, 2, 250, Color.White, 1.0f, true);
            WalkAnimation.Initialize(spriteStrip, position, 64, 128, 3, 200, Color.White, 1.0f, true);
            // Set the player to be active
            Active = true;
        }

        public  void Update(GameTime gameTime, Map map)
        {
            
            // Vector2 Position = Vector2.Zero;
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
            base.Update(gameTime);
           
            //Reset movement to still
            Movement.X = 0;
            
            // Trying to move Left or Right
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Movement.X = -5;
            }else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PlayerAnimation.sourceRect = new Rectangle(2 * PlayerAnimation.FrameWidth, 0, PlayerAnimation.FrameWidth, PlayerAnimation.FrameHeight);
                //PlayerAnimation.currentFrame = 2;
                //for (int i = 0; i < 2; i++)
                 //PlayerAnimation.currentFrame++;
               
                Movement.X = 5;
            }
            //Keeping track of jumping/falling speed
            if (Keyboard.GetState().IsKeyDown(Keys.Up)&&Position.Y==372)
            {
                Movement.Y += -20;
            }
            Movement.Y += 1;

            //establish ceiling and floor
            if (Position.Y + Movement.Y < 0)//ceiling
            {
                Position = new Vector2(Position.X, 0);
            }else if (Position.Y + Movement.Y > 372)//floor
            {
                Position = new Vector2(Position.X, 372);
                Movement.Y = 0;
            }
            //establish left and right bound for "dead zone"
            if (Position.X + Movement.X > 500)
            {
                offset.X += Position.X + Movement.X - 500;
                Position = new Vector2(500, Position.Y+Movement.Y);

            }else if(Position.X+Movement.X<100&&offset.X>0)
            {
                offset.X += Position.X + Movement.X - 100;
                Position = new Vector2(100, Position.Y + Movement.Y);
            }else
            {
                Position += Movement;
            }
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }
            map.Update(gameTime,offset);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the
            PlayerAnimation.Draw(spriteBatch);

            base.Draw(spriteBatch, gameTime);
        }

        public void changeHealth(int change)
        {
            // Add/subtract the change to the current health
            this._health += change;

            // More than max? Set to max
            if (this._health > this._maxHealth)
            {
                this._health = this._maxHealth;
            } // Less than 0? Set to 0.
            else if (this._health < 0)
            {
                this._health = 0;
            }
        }

        public void changeWeapon(int weapon)
        {
            // TODO - Do we need to update the graphic here too
            // or is that done at the next "update" command?
            this._weapon = weapon;
        }

        public void increaseScore(int amount)
        {
            // Add the amount to the level score
            this._levelScore += amount;
        }

        public void NextLevelScore()
        {
            // Add level to total, then set level to 0
            this._totalScore += this._levelScore;
            this._levelScore = 0;
        }

        public int getLevelScore()
        {
            return this._levelScore;
        }

        public int getTotalScore()
        {
            return this._totalScore;
        }

        public int getHealth()
        {
            return this._health;
        }

        public int getWeapon()
        {
            return this._weapon;
        }
    }
}