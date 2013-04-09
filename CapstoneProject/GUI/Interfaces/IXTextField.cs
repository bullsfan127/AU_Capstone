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
    /// Defines the Interface for a TextField in XNA.
    /// Which in this context is a string written on to the screen.
    /// </summary>
   public interface IXTextField : GComponent

    {
       /// <summary>
       /// String that you want to present
       /// </summary>
       string _Text { get; set; }

       /// <summary>
       /// Font to be drawn on the Screen.
       /// </summary>
        SpriteFont _Font { get; set; }

       
        
       /// <summary>
       /// Position on the Screen
       /// </summary>
        Vector2 _Position { get; set; }
       
        /// <summary>
       /// String Scale
       /// </summary>
        float _Scale { get; set; }

       
         
        /// <summary>
        /// String Color
        /// </summary>
        Color _TextColor { get; set; }
       /// <summary>
       /// TextField Visiblity
       /// </summary>
        bool _Enabled { get; set; }
       
        
       /// <summary>
       /// Draw Method
       /// </summary>
       /// <param name="gameTime"></param>
       /// <param name="spriteBatch"></param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
       

    }
}
