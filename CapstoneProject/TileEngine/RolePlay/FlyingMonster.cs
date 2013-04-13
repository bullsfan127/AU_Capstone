using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine
{
    public class FlyingMonster : Monster
    {

        /// <summary>
        /// FlyingMonster class - The monster that can fly
        /// </summary>
        public FlyingMonster()
        {
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            MaxDamage = 1;
            MaxHealth = 1;
        }


    }
}