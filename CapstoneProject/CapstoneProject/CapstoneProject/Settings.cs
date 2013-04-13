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
    public class Settings
    {
        MoveButton rightButton;
        MoveButton leftButton;
        MoveButton upButton;
        GraphicsDeviceManager graphics;
        ContentManager content;
        Label rightLabel;

        public Settings(GraphicsDeviceManager _graphics, ContentManager _content)
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
            // TODO: Add your update code here
            rightButton.Update(gameTime);
            leftButton.Update(gameTime);
            upButton.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            rightButton.Draw(gameTime, spriteBatch);

            leftButton.Draw(gameTime, spriteBatch);

            upButton.Draw(gameTime, spriteBatch);

            rightLabel.Text = Controls.Right.ToString();
            rightLabel.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            Texture2D rightTexture = content.Load<Texture2D>("MoveRight");
            rightButton = new MoveButton(new Vector2(50, 10), rightTexture, 0);
            rightButton.ChangeColor = true;

            Texture2D leftTexture = content.Load<Texture2D>("MoveLeft");
            leftButton = new MoveButton(new Vector2(50, 100), leftTexture, 1);
            leftButton.ChangeColor = true;

            Texture2D upTexture = content.Load<Texture2D>("JumpUp");
            upButton = new MoveButton(new Vector2(50, 200), upTexture, 2);
            upButton.ChangeColor = true;

            SpriteFont font = content.Load<SpriteFont>("FPS");
            rightLabel = new Label(font, Controls.Right.ToString(), new Vector2(200, 10), Color.Blue, 3);
        }
    }
}