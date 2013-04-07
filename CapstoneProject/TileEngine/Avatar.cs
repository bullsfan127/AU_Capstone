//Just an outline for now.
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
    public abstract class Avatar
    {
        //current offset for centering map
        public Vector2 _offset;

        public Microsoft.Xna.Framework.Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// Save offset values for serialization
        /// </summary>
        private float _oX;

        /// <summary>
        /// Save offset values for serialization
        /// </summary>
        private float _oY;

        public float OX
        {
            get { return _oX; }
            set { _oX = value; }
        }

        public float OY
        {
            get { return _oY; }
            set { _oY = value; }
        }

        // Animation representing the player
        private Animation _PlayerAnimation;

        public TileEngine.Animation PlayerAnimation
        {
            get { return _PlayerAnimation; }
            set { _PlayerAnimation = value; }
        }

        //Alows for the Postion
        private Vector2 _Position;

        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        /// <summary>
        /// save position values for serialization
        /// </summary>
        private float _x;

        /// <summary>
        /// save position values for serialization
        /// </summary>
        private float _y;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Avatar() { }

        public Avatar(Vector2 position, Texture2D Texture)
        {
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public virtual void Initialize(Texture2D spriteStrip, Vector2 position)
        {
        }
    }
}