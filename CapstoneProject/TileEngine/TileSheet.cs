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
    public class TileSheet
    {
        private Texture2D _tileSheet;

        public Texture2D tileSheet
        {
            get { return _tileSheet; }
            set { _tileSheet = value; }
        }

        
        
        public TileSheet(Texture2D texture)
        { 
        
        }



    }
}
