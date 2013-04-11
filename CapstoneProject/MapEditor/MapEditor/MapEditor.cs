using System;
using System.Collections.Generic;
using System.Linq;
using CustomSerialization;
using MapEditor.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;
using GUI.Controls;

namespace MapEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MapEditorMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapWindow window;
        TileSelector tileSelector;
        TileSheet sheets;
        XPanel TileSelectorPanel;

        public MapEditorMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //I might add the ability to scale the window based on your needs
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            window = new MapWindow(spriteBatch, graphics, Content);
            sheets = new TileSheet(this.Content.Load<Texture2D>("Tiles//FinalGrid"), 48, "Tiles//FinalGrid");
            tileSelector = new TileSelector(spriteBatch, sheets, graphics.GraphicsDevice, this.Content.Load<Texture2D>("Tiles//Node"));
            tileSelector._renderTarget = new Rectangle(graphics.GraphicsDevice.Viewport.Width - 240, 0, 240, 240);
            TileSelectorPanel = new XPanel(this.Content.Load<Texture2D>("Tiles//Node"), new Vector2(1280 - 300, 0), 300, 300);
            TileSelectorPanel.AddChild(tileSelector, new Vector2(30, 30));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            window.Update(gameTime);
            tileSelector.Update(gameTime);
            TileSelectorPanel.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            window.Draw(gameTime);
            tileSelector.Draw(gameTime);
            TileSelectorPanel.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}