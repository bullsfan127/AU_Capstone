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
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            MaxDamage = 1;
        }
    }
}
