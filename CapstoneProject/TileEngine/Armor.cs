using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Armor : Item
    {

        /// <summary>
        /// Armor class - Adds armor to the player (which increases health by 1)
        /// </summary>
        public Armor()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            armor = 1;

            Active = false;
        }
    }
}
