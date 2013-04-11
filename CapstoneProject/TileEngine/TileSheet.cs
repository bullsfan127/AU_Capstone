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
            int x = (int)Target.X;
            int y = (int) Target.Y;
            Rectangle sourceRect = Rectangle.Empty; 
            Rectangle[,] sourceRectangles = new Rectangle[(tileSheet.Width/_tileSize),(tileSheet.Height/_tileSize)];

            for (int i = 0; i < tileSheet.Width / _tileSize; i++)
                for (int q = 0; q < tileSheet.Height / _tileSize; q++)
                    sourceRectangles[i, q] = new Rectangle(i * _tileSize, q * _tileSize, _tileSize, _tileSize);

            Rectangle selection = new Rectangle(x, y, 1, 1);
            for (int i = 0; i < tileSheet.Width / _tileSize; i++)
                for (int q = 0; q < tileSheet.Height / _tileSize; q++)
                   {
                       if (selection.Intersects(sourceRectangles[i, q]))
                           sourceRect = sourceRectangles[i, q];
                      
                   }
           output = new Tile(sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
           output.setTexture(_tileSheet);
           output.Name = _name;
            return output;
        }


    }
}
