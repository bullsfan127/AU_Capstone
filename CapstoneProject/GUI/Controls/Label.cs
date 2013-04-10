
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
    public class Label : IXTextField
    {
        public string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        public SpriteFont _Font;
        public SpriteFont Font
        {
            get
            {
                return (_Font);
            }
            set
            {
                _Font = value;
            }
        }

        public Vector2 _Position;
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

        public float _Scale;
        public float Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
            }
        }

        public Color _TextColor;
        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
            }
        }

        public bool _Enabled;
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
                spriteBatch.DrawString(_Font, _Text, _Position, _TextColor, 0.0f, Vector2.Zero, _Scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
