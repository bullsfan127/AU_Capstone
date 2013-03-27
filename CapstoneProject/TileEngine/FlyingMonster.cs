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
    public class FlyingMonster : Monster
    {

        /// <summary>
        /// Default constructor for player
        /// </summary>
        public FlyingMonster()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            maxDamage = 1;
            maxHealth = 1;

            Active = false;
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