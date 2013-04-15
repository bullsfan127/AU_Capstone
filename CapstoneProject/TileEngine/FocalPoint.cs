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
        int lastmove = 0;

        public FocalPoint(Vector2 position, int tileWide, int tileHigh, int MaxX, int MaxY)
        {
            Position = position;
            width = tileWide;
            height = tileHigh;
            MaxXTiles = MaxX;
            MaxYTiles = MaxY;
        }

        public FocalPoint()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (lastmove - gameTime.TotalGameTime.Milliseconds > 300)
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    if (Position.X > 0)
                        Position = new Vector2(Position.X, Position.Y - 10);
                    lastmove = gameTime.TotalGameTime.Milliseconds;
                }
                else if (keyboard.IsKeyDown(Keys.Down))
                {
                    if (Position.Y < MaxYTiles - 10)
                        Position = new Vector2(Position.X, Position.Y + 10);
                    lastmove = gameTime.TotalGameTime.Milliseconds;
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    if (Position.X > 0)
                        Position = new Vector2(Position.X - 10, Position.Y);
                    lastmove = gameTime.TotalGameTime.Milliseconds;
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    if (Position.X < MaxXTiles - 10)
                        Position = new Vector2(Position.X + 10, Position.Y);
                    lastmove = gameTime.TotalGameTime.Milliseconds;
                }

            if (lastmove == 0 || (lastmove - gameTime.TotalGameTime.Milliseconds < 0))
                lastmove = gameTime.TotalGameTime.Milliseconds;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}