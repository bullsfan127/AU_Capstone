﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TileEngine
{
    public class GroundMonster : Monster
    {

        /// <summary>
        /// Default constructor for player
        /// </summary>
        public GroundMonster()
        {
            spriteWidth = 64;
            spriteHeight = 128;
            spriteFrame = 2;

            maxDamage = 1;
            maxHealth = 1;

            Active = false;
        }
    }
}