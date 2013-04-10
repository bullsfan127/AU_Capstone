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
        private int _tileSize;
        private string _name;
        public int TileSize
        {
            get { return _tileSize; }
            set { _tileSize = value; }
        }

        public Texture2D tileSheet
        {
            get { return _tileSheet; }
            set { _tileSheet = value; }
        }

        
        
        public TileSheet(Texture2D texture, int TileSize, string name)
        {
            tileSheet = texture;
            _tileSize = TileSize;
            _name = name;
        }

        public Tile getTileAt(Vector2 Target)
        { 
            Tile output;
           Rectangle sourceRect = new Rectangle((int)(Target.X /_tileSize), (int)(Target.Y / _tileSize), TileSize, TileSize);

           output = new Tile(sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
           output.setTexture(_tileSheet);
           output.Name = _name;
            return output;
        }


    }
}
