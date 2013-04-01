using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class Coin : Item
    {
        /// <summary>
        /// Coin class - Increases score by 1
        /// </summary>
        public Coin()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            score = 1;

            Active = false;
        }
    }
}