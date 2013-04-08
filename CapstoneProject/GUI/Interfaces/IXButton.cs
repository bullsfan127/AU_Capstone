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
    public interface IXButton : GComponent, IUpdateable
    {
        /// <summary>
        /// The Button's Texture
        /// </summary>
        private Texture2D _ButtonImage;

        public Texture2D ButtonImage
        {
            get { return _ButtonImage; }
            set { _ButtonImage = value; }
        }
        private Rectangle _ButtonRep;

        /// <summary>
        /// The Representation of the Button in the game world
        /// </summary>
        public Rectangle ButtonRep
        {
            get { return _ButtonRep; }
            set { _ButtonRep = value; }
        }
        private bool _Clicked;
        private bool _WasClicked;
        private bool _Enabled;

        Vector2 _Position;
        
        /// <summary>
        /// Position of the Button on the Screen;
        /// </summary>
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        /// <summary>
        /// Determines whether the Button is enabled
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        /// <summary>
        /// Update Method for a button should check to see if the button was clicked and if it was
        /// then fire the event which is the actual action of the Button.
        /// </summary>
        /// <param name="gameTime">supplied for button cooldown.</param>
        public void Update(GameTime gameTime);
        /// <summary>
        /// Draw Method for a Button
        /// </summary>
        /// <param name="gameTime">The Game Time</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        
        /// <summary>
        /// Does the actual work.  Performs the action that the button is supposed to.
        /// </summary>
        public void FireEvent();

    }
}
