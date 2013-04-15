using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI.Controls;
using GUI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

namespace MapEditor.GUI
{
    public class SaveButton : XButton
    {
        public Map saveMap;
        Label buttonLable;

        public SaveButton(Vector2 Position, Texture2D ButtonTexture, Map map, SpriteFont font)
            : base(Position, ButtonTexture)
        {
            saveMap = map;
            buttonLable = new Label(font, "Save Map", new Vector2(Position.X + 20, Position.Y + 20), Color.Black, 1.0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void UpdateM(GameTime gameTime, Map map)
        {
            saveMap = map;
            this.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            buttonLable.Draw(gameTime, spritebatch);
            base.Draw(gameTime, spritebatch);
        }

        public override void FireEvent()
        {
            Random random = new Random(42);
            saveMap.MapID = random.Next(999999);
            saveMap.saveMap("../../../SavedMaps/" + saveMap.MapID + ".xml");

            base.FireEvent();
        }
    }
}