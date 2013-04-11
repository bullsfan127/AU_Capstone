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
    public unsafe class SetFringeActive : XButton
    {
        MapWindow window;
        Label buttonLable;

        public SetFringeActive(Vector2 Position, Texture2D ButtonTexture, MapWindow window, SpriteFont font)
            : base(Position, ButtonTexture)
        {
            this.window = window;
            buttonLable = new Label(font, "", Position, Color.Black, 1.0f);

        }

        public override void Update(GameTime gameTime)
        {
            if (window._currentLayer != MapWindow.currentLayer.FRINGE)
                buttonLable.Text = "FRINGE Off";

            buttonLable._Position = this.Position;
            base.Update(gameTime);

        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            buttonLable.Draw(gameTime, spritebatch);
            base.Draw(gameTime, spritebatch);
        }

        public override void FireEvent()
        {

            if (window._currentLayer != MapWindow.currentLayer.FRINGE)
            {
                buttonLable.Text = "FRINGE On";
                window._currentLayer = MapWindow.currentLayer.FRINGE;
            }


            base.FireEvent();
        }
    }
}
