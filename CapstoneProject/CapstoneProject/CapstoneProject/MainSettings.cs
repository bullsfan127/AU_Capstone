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
    public class MainSettings 
    {

     MoveButton rightButton;
        MoveButton leftButton;
        MoveButton upButton;
        MainButton mainButton;
        MainSaveKeys msaveKeys;
        GraphicsDeviceManager graphics;
        ContentManager content;
        Label rightLabel;
        Label leftLabel;
        Label upLabel;
        Label wordLabel;
        Label wordLabel2;
        Label wordLabel3;
        XPanel rPanel;
        XPanel lPanel;
        XPanel upPanel;
        XPanel wordPanel;
        String text = "In order to change the controls of the game:";
        String text2 = "Click on the Button that you want to change - Example(MoveRight)";
        String text3 = "Then press the key on the keyboard that you want your new control to be - Example(D).";

       public MainSettings(GraphicsDeviceManager _graphics, ContentManager _content)
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
            msaveKeys.Update(gameTime);

            //update panels
            rPanel.Update(gameTime);
            lPanel.Update(gameTime);
            upPanel.Update(gameTime);
            wordPanel.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw buttons
            rightButton.Draw(gameTime, spriteBatch);
            leftButton.Draw(gameTime, spriteBatch);
            upButton.Draw(gameTime, spriteBatch);
            mainButton.Draw(gameTime, spriteBatch);
            msaveKeys.Draw(gameTime, spriteBatch);

            //Change Label text
            rightLabel.Text = Controls.Right.ToString();
            leftLabel.Text = Controls.Left.ToString();
            upLabel.Text = Controls.Up.ToString();
            wordLabel.Text = text;
            wordLabel2.Text = text2;
            wordLabel3.Text = text3;
            

            //Draw Labels
            rightLabel.Draw(gameTime, spriteBatch);
            leftLabel.Draw(gameTime, spriteBatch);
            upLabel.Draw(gameTime, spriteBatch);
            wordLabel.Draw(gameTime, spriteBatch);
            wordLabel2.Draw(gameTime, spriteBatch);
            wordLabel3.Draw(gameTime, spriteBatch);

            //Draw panels
            rPanel.Draw(gameTime, spriteBatch);
            lPanel.Draw(gameTime, spriteBatch);
            upPanel.Draw(gameTime, spriteBatch);
            wordPanel.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            //Load button textures
            Texture2D mainTexture = content.Load<Texture2D>("menuButtons/MainMenu");
            mainButton = new MainButton(new Vector2(300, 500), mainTexture, 3);

            Texture2D saveTexture = content.Load<Texture2D>("menuButtons/SaveKeys");
            msaveKeys = new MainSaveKeys(new Vector2(150, 500), saveTexture, 4);

            Texture2D rightTexture = content.Load<Texture2D>("menuButtons/MoveRight");
            rightButton = new MoveButton(new Vector2(150, 100), rightTexture, 0);
            //change color when button is clicked
            rightButton.ChangeColor = true;

            Texture2D leftTexture = content.Load<Texture2D>("menuButtons/MoveLeft");
            leftButton = new MoveButton(new Vector2(150, 200), leftTexture, 1);
            leftButton.ChangeColor = true;

            Texture2D upTexture = content.Load<Texture2D>("menuButtons/JumpUp");
            upButton = new MoveButton(new Vector2(150, 300), upTexture, 2);
            upButton.ChangeColor = true;

            //load Panel
            Texture2D panel = content.Load<Texture2D>("Panel540X540");
            //create panel
            rPanel = new XPanel(panel, new Vector2(300, 100), 100, 50);
            lPanel = new XPanel(panel, new Vector2(300, 200), 100, 50);
            upPanel = new XPanel(panel, new Vector2(300, 300), 100, 50);
            wordPanel = new XPanel(panel, new Vector2(50, 0), 100, 50);

            //Change panel color
            rPanel.Color = Color.Blue;
            lPanel.Color = Color.Blue;
            upPanel.Color = Color.Blue;
            wordPanel.Color = Color.Black;

            //load font
            SpriteFont font = content.Load<SpriteFont>("FPS");

            //create labels
            rightLabel = new Label(font, Controls.Right.ToString(), new Vector2(350, 10), Color.White, 2);
            leftLabel = new Label(font, Controls.Left.ToString(), new Vector2(350, 10), Color.White, 2);
            upLabel = new Label(font, Controls.Up.ToString(), new Vector2(350, 10), Color.White, 2);
            wordLabel = new Label(font, text, new Vector2(50, 10), Color.White, 1);
            wordLabel2 = new Label(font, text, new Vector2(50, 10), Color.White, 1);
            wordLabel3 = new Label(font, text, new Vector2(50, 10), Color.White, 1);
            //add labels to panels
            rPanel.AddChild(rightLabel, new Vector2(15, 15));
            lPanel.AddChild(leftLabel, new Vector2(15, 15));
            upPanel.AddChild(upLabel, new Vector2(15, 15));
            wordPanel.AddChild(wordLabel, new Vector2(15, 15));
            wordPanel.AddChild(wordLabel2, new Vector2(15, 35));
            wordPanel.AddChild(wordLabel3, new Vector2(15, 55));
        }
    }
}