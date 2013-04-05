using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Armor : Item
    {

        /// <summary>
        /// Armor class - Adds armor to the player (which increases health by 1 indirectly)
        /// </summary>
        public Armor()
        {
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            Armor = true;

        }
    }
}
