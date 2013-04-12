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


namespace GUI.Interfaces
{
    /// <summary>
    /// Interface Defining Button
    /// </summary>
    public interface IXButton : GComponent
    {

        /// <summary>
        /// The Button's Texture
        /// </summary>
         Texture2D ButtonImage { get; set; }

        /// <summary>
        /// The Representation of the Button in the game world
        /// </summary>
         Rectangle ButtonRep { get; set; }
               
        /// <summary>
        /// Is the button Clicked
        /// </summary>
         bool Clicked { get; set; }

        /// <summary>
        /// Was the button recently Clicked.
        /// </summary>
         bool WasClicked { get; set; }
        
        /// <summary>
        /// Determines whether the Button is enabled
        /// </summary>
        bool Enabled { get; set; }
        
        /// <summary>
        /// Position of the Button on the Screen;
        /// </summary>
        //Vector2 Position { get; set; }
        //Moved to the parent
        
        /// <summary>
        /// Update Method for a button should check to see if the button was clicked and if it was
        /// then fire the event which is the actual action of the Button.
        /// </summary>
        /// <param name="gameTime">supplied for button cooldown.</param>
       new  void Update(GameTime gameTime);
        /// <summary>
        /// Draw Method for a Button
        /// </summary>
        /// <param name="gameTime">The Game Time</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        new  void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        
        /// <summary>
        /// Does the actual work.  Performs the action that the button is supposed to.
        /// </summary>
         void FireEvent();

    }
}
