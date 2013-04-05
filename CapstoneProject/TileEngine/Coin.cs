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
        /// Offset by +19,+19 to center it within a tile
        /// </summary>
        public Coin()
        {
            SpriteWidth = 64;
            SpriteHeight = 64;
            SpriteFrame = 2;

            Score = 1;

        }
    }
}
