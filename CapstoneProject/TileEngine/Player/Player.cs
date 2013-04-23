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
        //unknown
        private ContentManager Content;

        //Current movement speeds for player
        private Vector2 _movement;

        //Players collision Rectangle
        private Rectangle PlayerRect;

        //Players position on the viewPort
        private Vector2 viewPortPostion;

        public Vector2 ViewPortPostion
        {
            get { return viewPortPostion; }
            set { viewPortPostion = value; }
        }

        //Player is on the ground and can jump
        private bool Jump;

        // State of the player
        private bool _active;

        // Does player have armor
        private bool _armor;

        // Current health of the player
        private int _health;

        // Maximum health of the player
        private int _maxHealth = 3;

        // the weapon the player is holding
        private Weapon _weapon;

        // The score for the current level
        private int _levelScore;

        public Texture2D _swordTexture;
        public Texture2D _rangedTexture;

        private int _justAttacked = 0;

        public int JustAttacked
        {
            get { return _justAttacked; }
            set { _justAttacked = value; }
        }

        private bool _attackReleased = true;

        public bool AttackReleased
        {
            get { return _attackReleased; }
            set { _attackReleased = value; }
        }

        private int _weaponDirection = 1;

        public int WeaponDirection
        {
            get { return _weaponDirection; }
            set { _weaponDirection = value; }
        }

        // The score for the entire game
        private int _totalScore = 0;

        public Microsoft.Xna.Framework.Vector2 Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        public int LevelScore
        {
            get { return _levelScore; }
            set { _levelScore = value; }
        }

        public int TotalScore
        {
            get { return _totalScore; }
            set { _totalScore = value; }
        }

        /// <summary>
        /// Default constructor for player
        /// </summary>
        public Player(ContentManager Content)
        {
            _swordTexture = Content.Load<Texture2D>("Items/Sword");
            _rangedTexture = Content.Load<Texture2D>("Items/Boomerang");
            _weapon = new Sword();
            this.Content = Content;
        }

        public Player()
        {
        }

        /// <summary>
        /// Initializes the player
        /// </summary>
        /// <param name="spriteStrip"></param>
        /// <param name="position"></param>
        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            PlayerAnimation = new Animation();

            // Set the player's health to the maximum
            _health = this._maxHealth;

            // Set starting position of the player
            viewPortPostion = position;
            //make player rectangle
            PlayerRect = new Rectangle((int)viewPortPostion.X, (int)viewPortPostion.Y, 64, 128);
            //player Animation initialize
            PlayerAnimation.Initialize(spriteStrip, viewPortPostion, 64, 128, 2, 250, Color.White, 1.0f, true);
            _weapon.Initialize(_swordTexture, viewPortPostion);
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
            PlayerAnimation.Position = viewPortPostion;
           
            if (_justAttacked <= 20)
            {
                _weapon.Update(gameTime, new Vector2(-500, -500));
                if (_justAttacked >= 0)
                {
                    _justAttacked--;
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Space))
                {
                    _attackReleased = true;
                }
            }
            else
            {
                _weapon.setDirection(_weaponDirection);
                _weapon.Update(gameTime, Position);
                _justAttacked--;
                _attackReleased = false;
            }
            //PlayerAnimation.Update(gameTime);

            base.Update(gameTime);

            //Reset movement to still

            _movement.X = 0;

            //Reset movement to still
            if (_justAttacked <= 0)
            {
                if (_weaponDirection == 1)
                {
                    PlayerAnimation.state = Animation.Animate.RIDLE;
                }
                else
                {
                    PlayerAnimation.state = Animation.Animate.LIDLE;
                }
            }

            // Trying to move Left or Right
            if (Keyboard.GetState().IsKeyDown(Controls.Left))
            {
                PlayerAnimation.state = Animation.Animate.LMOVING;

                _movement.X = -5;
                _weaponDirection = -1;
            }

            else if (Keyboard.GetState().IsKeyDown(Controls.Right))
            {
                PlayerAnimation.state = Animation.Animate.RMOVING;

                _movement.X = 5;
                _weaponDirection = 1;
            }

            //Keeping track of jumping/falling speed
            if (Keyboard.GetState().IsKeyDown(Controls.Up) && Jump)
            {
                _movement.Y += -20;
                Jump = false;
            }

            // Attack
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)) && _attackReleased)
            {
                _justAttacked = 40;
                if (_weaponDirection == 1)
                {
                    PlayerAnimation.state = Animation.Animate.RATTACK;
                }
                else
                {
                    PlayerAnimation.state = Animation.Animate.LATTACK;
                }
            }

            _movement.Y += 1;

            //establish ceiling and floor
            if ((viewPortPostion.Y + _movement.Y < 0) && (Position.X > 0))//ceiling
            {
                
                _offset.Y -= 5;

                if( _offset.Y % -60 == 0)
                Position = new Vector2(Position.X, Position.Y - 1);
            }
            else if (viewPortPostion.Y + _movement.Y > 400)//floor
            {
               // viewPortPostion = new Vector2(viewPortPostion.X, 372);
                _movement.Y = 0;
                Jump = true;
            }

            _movement.Y++;
             
            //establish left and right bound for "dead zone"
            //Right
            if (viewPortPostion.X + _movement.X > 500)
            {
                
                viewPortPostion = new Vector2(500, viewPortPostion.Y + _movement.Y);

                //if the Position less than the max scroll range and the offset is on a whole tile
                if ((Position.X < map.Ground.MapWidth) && _offset.X >= 60)
                {
                    Position = new Vector2(Position.X + 1, Position.Y);
                    _offset.X = 0;
                }
                else
                _offset.X += _movement.X;
            }
                //Left
            else if (viewPortPostion.X + _movement.X < 100 )
            {
                
                viewPortPostion = new Vector2(100, viewPortPostion.Y + _movement.Y);

                if ((Position.X > 0) && ( _offset.X <= -60 ))
                {
                    Position = new Vector2(Position.X - 1, Position.Y);
                    _offset.X = 0;   
                }
                else
                _offset.X += _movement.X;
            }
            else
                viewPortPostion = new Vector2(viewPortPostion.X + _movement.X, viewPortPostion.Y);

            //establish upper and lower bound for dead zone
            if (viewPortPostion.Y + _movement.Y < 100 && _offset.Y > 0)
            {
                _offset.Y += viewPortPostion.Y + _movement.Y - 100;
                viewPortPostion = new Vector2(viewPortPostion.X, 100);
                
            }
                //down
            else if (viewPortPostion.Y + _movement.Y > 400)
            {
                
                viewPortPostion = new Vector2(viewPortPostion.X, 400);

                if ((Position.Y < map.Ground.MapHeight - 10) && (_offset.Y >= 60))
                {
                    Position = new Vector2(Position.X, Position.Y + 1);
                    _offset.Y = 0;
                }
                else
                    _offset.Y += _movement.Y;
            }
            else
                viewPortPostion = new Vector2(viewPortPostion.X, viewPortPostion.Y + _movement.Y);

            //update rectangle position based on player position
            PlayerRect.X = (int)viewPortPostion.X;
            PlayerRect.Y = (int)viewPortPostion.Y;

            //prevent the player from moving off the side
            if (viewPortPostion.X < 0)
            {
                viewPortPostion = new Vector2(0, viewPortPostion.Y);
            }

            if (Keyboard.GetState().IsKeyDown(Controls.Attack))
            {
            }
            //save position values
            X = viewPortPostion.X;
            Y = viewPortPostion.Y;
            PlayerAnimation.Update(gameTime);

            //Save offset values
            OX = _offset.X;
            OY = _offset.Y;

            //if ((_offset.X % 60 == 0) || ( _offset.X % -60 == 0))
            //    _offset.X = 0;

            //if (( _offset.Y % 60 == 0) || ( _offset.X % -60 == 0))
            //    _offset.Y = 0;

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
            _weapon.Draw(spriteBatch, gameTime);
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
            if (hasArmor() && change == 1)
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

            // TODO - Do we need to update the graphic here too
            // or is that done at the next "update" command?
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