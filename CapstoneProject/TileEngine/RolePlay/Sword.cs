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
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 1;

            MaxDamage = 1;
            Scale = 1.0f;
        }
    }
}
