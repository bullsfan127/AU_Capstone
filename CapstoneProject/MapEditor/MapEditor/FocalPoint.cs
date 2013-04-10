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
    public class FocalPoint : Avatar
    {
        /// <summary>
        /// viewport width
        /// </summary>
        int width;
        /// <summary>
        /// viewport height
        /// </summary>
        int height;

        int MaxXTiles;
        int MaxYTiles;
        public FocalPoint(Vector2 position, int tileWide, int tileHigh, int MaxX, int MaxY)
        {
            Position = position;
            width = tileWide;
            height = tileHigh;
            MaxXTiles = MaxX;
            MaxYTiles = MaxY;
        }



        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Up))
            {
                if (Position.X > height / 2)
                    Position = new Vector2(Position.X -1, Position.Y);
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (Position.X > MaxYTiles - height / 2)
                    Position = new Vector2(Position.X + 1, Position.Y);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (Position.Y > height / 2)
                    Position = new Vector2(Position.X, Position.Y -1);
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if (Position.X > MaxYTiles - width / 2)
                    Position = new Vector2(Position.X, Position.Y +1);
            }
            
            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }




    }
}