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
    public class GroundMonster : Monster
    {
        private int _maxHealth = 3;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }
        private int _maxDamage = 1;
        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }

        /// <summary>
        /// Default constructor for player
        /// </summary>
        public GroundMonster()
        {
        }

        public void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            MonsterAnimation = new Animation();

            Health = _maxHealth;

            // Set starting position of the player
            Position = position;

            // TODO: Need to set correct image/location
            MonsterAnimation.Initialize(spriteStrip, position, 64, 128, 2, 250, Color.White, 1.0f, true);

            // Set the player to be active
            Active = true;
        }

        /// <summary>
        /// TODO: Not sure how the collision is working... but generally this
        /// all that is needed to make the damage happen.
        /// </summary>
        public void doDamageToPlayer()
        {
            // TODO
            //player.changeHealth(this._maxDamage);
        }


    }
}