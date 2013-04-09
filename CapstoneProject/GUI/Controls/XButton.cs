using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GUI.Controls
{
   public class XButton : IXButton
    {
        private Texture2D _ButtonImage;
        public Texture2D ButtonImage
        {
            get
            {
              return  _ButtonImage;
            }
            set
            {
                _ButtonImage = value;
            }
        }

        private Rectangle _ButtonRep;
        public Rectangle ButtonRep
        {
            get
            {
                return _ButtonRep;
            }
            set
            {
                _ButtonRep = value;
            }
        }

        private bool _Clicked;
        public bool Clicked
        {
            get
            {
                return _Clicked;
            }
            set
            {
                _Clicked = value;
            }
        }

        private bool _WasClicked;
        public bool WasClicked
        {
            get
            {
                return _WasClicked;
            }
            set
            {
                _WasClicked = value;
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }

        private Vector2 _Position;
        public Vector2 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
            }
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void FireEvent()
        {
            throw new NotImplementedException();
        }
    }
}
