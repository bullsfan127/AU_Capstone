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
    /// <summary>
    /// Counts the frames per second
    /// </summary>
    public class ScoreDisplay
    {
        SpriteFont font;
        GraphicsDeviceManager graphics;
        private int _score;

        /// <summary>
        /// Initializes the FPS counter
        /// </summary>
        /// <param name="graphic">Graphics device</param>
        public ScoreDisplay(GraphicsDeviceManager graphic)
        {
            //Might not be needed because it might be accessible from spriteBatch
            graphics = graphic;
        }

        /// <summary>
        /// Updates the FPS counter
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(int newScore)
        {
            _score = newScore;
        }

        /// <summary>
        /// loads the spriteFont
        /// </summary>
        /// <param name="a">SpriteFont</param>
        public void loadFont(SpriteFont a)
        {
            font = a;
        }

        /// <summary>
        /// Draws the FPS
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Score: " + _score, new Vector2(400, 50), Color.White);
                spriteBatch.End();
        }
    }
}
