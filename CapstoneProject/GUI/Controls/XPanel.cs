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
    public class XPanel : IXPanel
    {
        private Color _color = Color.White;

        public Microsoft.Xna.Framework.Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private Texture2D _BackGroundImage;

        public Texture2D BackGroundImage
        {
            get
            {
                return _BackGroundImage;
            }
            set
            {
                _BackGroundImage = value;
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

        private List<GComponent> _ChildComponents;

        public List<GComponent> ChildComponents
        {
            get
            {
                return _ChildComponents;
            }
            set
            {
                _ChildComponents = value;
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

        private int _PanelWidth;

        public int PanelWidth
        {
            get { return _PanelWidth; }
            set { _PanelWidth = value; }
        }

        private int _PanelHeight;

        public int PanelHeight
        {
            get { return _PanelHeight; }
            set { _PanelHeight = value; }
        }

        public XPanel(Texture2D background, Vector2 Position, int width, int height)
        {
            _BackGroundImage = background;
            this.Position = Position;
            _PanelWidth = width;
            _PanelHeight = height;
            _ChildComponents = new List<GComponent>();
        }

        /// <summary>
        /// Add A child to the Panel
        /// </summary>
        /// <param name="childComponent">The Component you wish to add the Panel</param>
        /// <param name="Position">The Position in relatation to the top-Left of the Panel</param>
        public void AddChild(GComponent childComponent, Vector2 Position)
        {
            childComponent.Position = new Vector2(Position.X + this.Position.X, Position.Y + this.Position.Y);
            _ChildComponents.Add(childComponent);
        }

        public void Update(GameTime gameTime)
        {
            foreach (GComponent a in _ChildComponents)
            {
                a.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_BackGroundImage, new Rectangle((int)_Position.X, (int)Position.Y, PanelWidth, PanelHeight), _color);
            spriteBatch.End();

            foreach (GComponent a in _ChildComponents)
            {
                if (a != null)
                    a.Draw(gameTime, spriteBatch);
            }
        }
    }
}