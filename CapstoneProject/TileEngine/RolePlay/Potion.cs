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
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            Health = 1;
            Scale = 0.6f;
        }
    }
}