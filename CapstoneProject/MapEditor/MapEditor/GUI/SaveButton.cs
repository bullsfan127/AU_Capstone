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
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.InitialDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
            saveFileDialog1.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";

            System.Windows.Forms.DialogResult result = saveFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) // Test result.
            {
                string file = saveFileDialog1.FileName;
                saveMap.saveMap(file, false);
            }

            base.FireEvent();
        }
    }
}