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
    public class LoadMap : XButton
    {
        public Map loadMap;
        Label buttonLable;
        ContentManager content;
        public bool newMap = false;

        public LoadMap(Vector2 Position, Texture2D ButtonTexture, Map map, SpriteFont font, ContentManager _content)
            : base(Position, ButtonTexture)
        {
            loadMap = map;
            buttonLable = new Label(font, "Load Map", new Vector2(Position.X + 20, Position.Y + 20), Color.Black, 1.0f);
            content = _content;
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
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
            System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) // Test result.
            {
                newMap = true;
                string file = openFileDialog1.FileName;

                loadMap = loadMap.LoadMap(file, content, false);
            }
            base.FireEvent();
        }
    }
}