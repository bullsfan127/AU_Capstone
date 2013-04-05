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
        private Texture2D _Texture;

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get { return _Texture; }
            set { _Texture = value; }
        }

        private Vector2 _Position;

        // Animation representing the player
        private Animation _PlayerAnimation;

        public TileEngine.Animation PlayerAnimation
        {
            get { return _PlayerAnimation; }
            set { _PlayerAnimation = value; }
        }

        private float _x;
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

        //Alows for the Postion
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        // private Texture2D _Sprite;

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