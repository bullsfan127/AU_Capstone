using System;
using System.Collections.Generic;
using System.Linq;
using CustomSerialization;
using GUI.Controls;
using MapEditor.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

namespace MapEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MapEditorMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static public ContentManager content;

        /// <summary>
        /// The window the map is contained in.
        /// </summary>
        MapWindow window;

        //~~~~~~~~~~Tile Selector~~~~~~~~~~~~~
        /// <summary>
        /// Tile Selector panel
        /// </summary>
        XPanel TileSelectorPanel;

        /// <summary>
        /// Object that handles all the tile management
        /// </summary>
        TileSelector tileSelector;

        /// <summary>
        /// Tilesheet the tiles are pulled off of.
        /// </summary>
        TileSheet sheets;

        /// <summary>
        /// Layer selection
        /// </summary>
        XPanel LayerSelectionPanel;

        Label ViewportPosition;
        SaveButton saveButton;
        ResetButton resetButton;
        LoadMap loadButton;

        public MapEditorMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //I might add the ability to scale the window based on your needs
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
            content = this.Content;
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
        ///

        protected override void LoadContent()
        {
            Texture2D panelTexture = this.Content.Load<Texture2D>("Panel540X540");
            Texture2D buttonTexture = this.Content.Load<Texture2D>("Tiles//Node");
            SpriteFont Text = this.Content.Load<SpriteFont>("FPS");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sheets = new TileSheet(this.Content.Load<Texture2D>("Tiles//FinalGrid"), 64, "Tiles//FinalGrid");
            //~~~~~~~~~Tile Selector~~~~~~~~~~~~~~
            tileSelector = new TileSelector(spriteBatch, sheets, graphics.GraphicsDevice, panelTexture);
            tileSelector._renderTarget = new Rectangle(graphics.GraphicsDevice.Viewport.Width - 320, 0, 320, 320);
            TileSelectorPanel = new XPanel(panelTexture, new Vector2(1280 - 360, 0), 360, 360);
            TileSelectorPanel.AddChild(tileSelector, new Vector2(20, 20));
            //~~~~~~~~~~window code~~~~~~~~~~~~~~~
            window = new MapWindow(spriteBatch, graphics, Content, this.Content.Load<SpriteFont>("FPS"), tileSelector, panelTexture);
            window.Position = new Vector2(200, 50);
            //~~~~~~~~~Layer selection Panel~~~~~~~~~~~~
            LayerSelectionPanel = new XPanel(panelTexture, new Vector2(1280 - 360, 360), 360, 360);
            //Button adds
            LayerSelectionPanel.AddChild(new SetGoundActiveButton(Vector2.Zero, buttonTexture,
                window,Text), new Vector2(30, 30));
            LayerSelectionPanel.AddChild(new SetMaskActive(Vector2.Zero, buttonTexture,
                    window, this.Content.Load<SpriteFont>("FPS")), new Vector2(30, 30 + buttonTexture.Height));
            LayerSelectionPanel.AddChild(new SetFringeActive(Vector2.Zero, buttonTexture,
                        window, this.Content.Load<SpriteFont>("FPS")), new Vector2(30, 30 + (buttonTexture.Height) * 2));
            LayerSelectionPanel.AddChild(window.layerLabel, new Vector2(buttonTexture.Width + 30, 30));

            saveButton = new SaveButton(Vector2.Zero, buttonTexture, window.Map, this.Content.Load<SpriteFont>("FPS"));
            loadButton = new LoadMap(new Vector2(0, 300), buttonTexture, window.Map, this.Content.Load<SpriteFont>("FPS"), this.Content);
            resetButton = new ResetButton(new Vector2(0, 100), buttonTexture, this.Content.Load<SpriteFont>("FPS"), graphics, Content, window);

            ViewportPosition = new Label(Text, "The Position of the Viewport is:  " + window.Center.Position.ToString(), new Vector2(200, 0), Color.Red, 1.5f);
            
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

            TileSelectorPanel.Update(gameTime);
            LayerSelectionPanel.Update(gameTime);
            saveButton.UpdateM(gameTime, window.Map);
            loadButton.Update(gameTime);
            if (loadButton.newMap)
            {
                window.Map = null;
                window.Map = loadButton.loadMap;
                loadButton.newMap = false;
            }
            resetButton.Update(gameTime);
            ViewportPosition._Text = "The Position of the Viewport is:  " + window.Center.Position.ToString();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RoyalBlue);

       
            window.Draw(gameTime, spriteBatch);

            TileSelectorPanel.Draw(gameTime, spriteBatch);
            LayerSelectionPanel.Draw(gameTime, spriteBatch);
            saveButton.Draw(gameTime, spriteBatch);
            resetButton.Draw(gameTime, spriteBatch);
            loadButton.Draw(gameTime, spriteBatch);
            ViewportPosition.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}