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
        private Vector2 _Position;

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

        public virtual void Update(GameTime gameTime){}

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        
        
        }


    }
}
