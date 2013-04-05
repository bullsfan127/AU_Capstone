using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HealthBar
    {
        GraphicsDeviceManager graphics;
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        ContentManager Content;

        public HealthBar(GraphicsDeviceManager _graphics, ContentManager content)  
        {
            Content = content;
            graphics = _graphics;
        }

     

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here

            
        }
        public void LoadContent()
        {
            texture = Content.Load<Texture2D>("AdvHealth");
            position = new Vector2(600, 30);
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                rectangle.Width -= 1;

            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, rectangle, Color.White);
            spriteBatch.End();

            
        }
    }
}
