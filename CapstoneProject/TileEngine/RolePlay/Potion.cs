using System;
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
    public class Potion : Item
    {
        /// <summary>
        /// Potion class - Increases health by 1
        /// </summary>
        public Potion()
        {
            SpriteWidth = 64;
            SpriteHeight = 64;
            SpriteFrame = 1;

            Health = 1;
            Scale = 0.6f;
        }
    }
}