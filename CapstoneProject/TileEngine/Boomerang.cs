using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Boomerang : Weapon
    {

        /// <summary>
        /// Boomerang class - Sets damage amount
        /// </summary>
        public Boomerang()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            maxDamage = 1;
        }
    }
}
