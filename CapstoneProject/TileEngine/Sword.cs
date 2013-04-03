using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Sword : Weapon
    {

        /// <summary>
        /// Sword class - Sets damage amount
        /// </summary>
        public Sword()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            maxDamage = 1;
        }
    }
}
