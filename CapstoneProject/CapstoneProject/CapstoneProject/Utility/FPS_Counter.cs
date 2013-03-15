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


namespace CapstoneProject
{
    /// <summary>
    /// Counts the frames per second
    /// </summary>
    public class FPS_Counter 
    {
        int lastTime;
        
        int frames = 0;
        int fps;
        SpriteFont font;
        GraphicsDeviceManager graphics;

        //bool to see if visible
        bool isVisible = false;

        /// <summary>
        /// Initializes the FPS counter
        /// </summary>
        /// <param name="graphic">Graphics device</param>
        public FPS_Counter(GraphicsDeviceManager graphic)
        {
            //Might not be needed because it might be accessible from spriteBatch
            graphics = graphic;
        }
        
        /// <summary>
        /// Updates the FPS counter
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
                int currentTime = gameTime.TotalGameTime.Milliseconds;
                if ((currentTime - lastTime) == 1000)
                {
                    frames = 0;
                    lastTime = 0;
                }
            lastTime += gameTime.TotalGameTime.Milliseconds;
            
            
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
            
           //add frame
           frames++;
            //calc FPS
           fps  =  lastTime /frames;
           if (isVisible)
           {
               spriteBatch.Begin();
               spriteBatch.DrawString(font, "FPS: " + fps, new Vector2(0, 0), Color.White);
               spriteBatch.End();
           }

        }

       
        /// <summary>
        /// Sets the Visibility
        /// </summary>
        /// <param name="Visibility">sets if its visible or not</param>
        public void setVisibility(bool Visibility)
        {
            isVisible = Visibility;
        }
    }
}
