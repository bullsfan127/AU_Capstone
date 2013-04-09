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
       string _Text { get; set; }

       /// <summary>
       /// String that you want to present
       /// </summary>
        string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        SpriteFont _Font { get; set; }

       /// <summary>
       /// Font to be drawn on the Screen.
       /// </summary>
         SpriteFont Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        Vector2 _Position { get; set; }
       /// <summary>
       /// Position on the Screen
       /// </summary>
         Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        float _Scale { get; set; }

       /// <summary>
       /// String Scale
       /// </summary>
         float Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }

        Color _TextColor { get; set; }
       /// <summary>
       /// String Color
       /// </summary>
         Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }


        bool _Enabled { get; set; }
       /// <summary>
       /// TextField Visiblity
       /// </summary>
         bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
       

    }
}
