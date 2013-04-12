using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;
using GUI.Interfaces;

namespace MapEditor.GUI
{
    public class ResetButton : XButton
    {
        GraphicsDeviceManager _graphic;
        ContentManager _content;
        MapWindow _mapWindow;
        Label buttonLable;

        public ResetButton(Vector2 Position, Texture2D ButtonTexture, SpriteFont font, GraphicsDeviceManager graphics, ContentManager manager, MapWindow mapWindow)
            : base(Position, ButtonTexture)
        {
            _graphic = graphics;
            _content = manager;
            _mapWindow = mapWindow;
            buttonLable = new Label(font, "Reset Map", new Vector2(Position.X + 20, Position.Y + 20), Color.Black, 1.0f);

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            buttonLable.Draw(gameTime, spritebatch);
            base.Draw(gameTime, spritebatch);
        }

        public override void FireEvent()
        {

            _mapWindow.reset(_graphic, _content);

            base.FireEvent();
        }
    }
}

