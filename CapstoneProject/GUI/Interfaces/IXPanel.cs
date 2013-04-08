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
    /// An Interface for Outlining the Behavior for a Panel in XNA
    /// 
    /// A Panel Tethers the Position of the child Components to itself so that all components move together.
    /// It also updates its child components.
    /// </summary>
    public interface IXPanel : GComponent, IUpdateable
    {
       
        Texture2D _BackGroundImage;
        /// <summary>
        /// Texture Drawn below the Child Components;
        /// </summary>
        public Texture2D BackGroundImage
        {
            get { return _BackGroundImage; }
            set { _BackGroundImage = value; }
        }
        Vector2 _Position;
        /// <summary>
        /// Position on the ViewPort
        /// </summary>
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        List<GComponent> _ChildComponents;
        bool _Enabled;
        /// <summary>
        /// Is the Component Enabled?
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        /// <summary>
        /// Add A child to the Panel
        /// </summary>
        /// <param name="childComponent">The Component you wish to add the Panel</param>
        /// <param name="Position">The Position in relatation to the top-Left of the Panel</param>
        public void AddChild(GComponent childComponent, Vector2 Position);
        
        /// <summary>
        /// Updates all Child Components and its self.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime);

        /// <summary>
        /// Draws all its Children
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        /// <param name="spriteBatch">SpriteBatch for Drawing.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        
    }
}
