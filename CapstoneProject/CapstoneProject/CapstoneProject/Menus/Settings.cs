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
        MainButton mainButton;
        SaveKeys saveKeys;
        GraphicsDeviceManager graphics;
        ContentManager content;
        Label rightLabel;
        Label leftLabel;
        Label upLabel;
        XPanel rPanel;
        XPanel lPanel;
        XPanel upPanel;

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
            // update buttons
            rightButton.Update(gameTime);
            leftButton.Update(gameTime);
            upButton.Update(gameTime);
            mainButton.Update(gameTime);
            saveKeys.Update(gameTime);

            //update panels
            rPanel.Update(gameTime);
            lPanel.Update(gameTime);
            upPanel.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw buttons
            rightButton.Draw(gameTime, spriteBatch);
            leftButton.Draw(gameTime, spriteBatch);
            upButton.Draw(gameTime, spriteBatch);
            mainButton.Draw(gameTime, spriteBatch);
            saveKeys.Draw(gameTime, spriteBatch);

            //Change Label text
            rightLabel.Text = Controls.Right.ToString();
            leftLabel.Text = Controls.Left.ToString();
            upLabel.Text = Controls.Up.ToString();

            //Draw Labels
            rightLabel.Draw(gameTime, spriteBatch);
            leftLabel.Draw(gameTime, spriteBatch);
            upLabel.Draw(gameTime, spriteBatch);

            //Draw panels
            rPanel.Draw(gameTime, spriteBatch);
            lPanel.Draw(gameTime, spriteBatch);
            upPanel.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            //Load button textures
            Texture2D mainTexture = content.Load<Texture2D>("menuButtons/MainMenu");
            mainButton = new MainButton(new Vector2(200, 400), mainTexture, 3);

            Texture2D saveTexture = content.Load<Texture2D>("menuButtons/SaveKeys");
            saveKeys = new SaveKeys(new Vector2(50, 400), saveTexture, 4);

            Texture2D rightTexture = content.Load<Texture2D>("menuButtons/MoveRight");
            rightButton = new MoveButton(new Vector2(50, 10), rightTexture, 0);
            //change color when button is clicked
            rightButton.ChangeColor = true;

            Texture2D leftTexture = content.Load<Texture2D>("menuButtons/MoveLeft");
            leftButton = new MoveButton(new Vector2(50, 100), leftTexture, 1);
            leftButton.ChangeColor = true;

            Texture2D upTexture = content.Load<Texture2D>("menuButtons/JumpUp");
            upButton = new MoveButton(new Vector2(50, 200), upTexture, 2);
            upButton.ChangeColor = true;

            //load Panel
            Texture2D panel = content.Load<Texture2D>("Panel540X540");
            //create panel
            rPanel = new XPanel(panel, new Vector2(200, 10), 100, 50);
            lPanel = new XPanel(panel, new Vector2(200, 100), 100, 50);
            upPanel = new XPanel(panel, new Vector2(200, 200), 100, 50);
            //Change panel color
            rPanel.Color = Color.Blue;
            lPanel.Color = Color.Blue;
            upPanel.Color = Color.Blue;

            //load font
            SpriteFont font = content.Load<SpriteFont>("FPS");

            //create labels
            rightLabel = new Label(font, Controls.Right.ToString(), new Vector2(200, 10), Color.White, 2);
            leftLabel = new Label(font, Controls.Left.ToString(), new Vector2(200, 10), Color.White, 2);
            upLabel = new Label(font, Controls.Up.ToString(), new Vector2(200, 10), Color.White, 2);
            //add labels to panels
            rPanel.AddChild(rightLabel, new Vector2(15, 15));
            lPanel.AddChild(leftLabel, new Vector2(15, 15));
            upPanel.AddChild(upLabel, new Vector2(15, 15));
        }
    }
}