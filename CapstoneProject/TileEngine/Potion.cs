using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Potion : Item
    {

        /// <summary>
        /// Potion class - Increases health by 1
        /// </summary>
        public Potion()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            health = 1;

            Active = false;
    }
}
