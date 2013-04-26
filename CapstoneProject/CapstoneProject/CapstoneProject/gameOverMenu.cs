using System;
using System.Collections.Generic;
using System.Linq;
using GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

namespace CapstoneProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class gameOverMenu
    {
        MainButton mainButton;

        GraphicsDeviceManager graphics;
        ContentManager content;

        public gameOverMenu(GraphicsDeviceManager _graphics, ContentManager _content)
        {
            graphics = _graphics;
            content = _content;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            mainButton.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D background, Rectangle mainFrame)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.End();
            mainButton.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            //Load button textures
            Texture2D mainTexture = content.Load<Texture2D>("menuButtons/MainMenu");
            mainButton = new MainButton(new Vector2(250, 500), mainTexture, 3);
        }
    }
}