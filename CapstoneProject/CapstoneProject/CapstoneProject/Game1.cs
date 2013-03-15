using System;
using System.Collections.Generic;
using System.Linq;
using CustomSerialization;
using DebugTerminal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

/**************************************************
 * Added an XNA debugger, it can debug in real time,
 * see http://www.protohacks.net/xna_debug_terminal/HowTo%20v2.1.php5 for usuage.
 * Press the tab key to invole it
 **************************************************************/

namespace CapstoneProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tile a;
        Tile b;
        Tile c;
        DrawableLayer<Tile> currentLayer;
        DrawableLayer<Tile> currentLayerA;
        DrawableLayer<Tile> currentLayerB;

        // Represents the player
        Player player;

        Serialize<Map> serializer = new Serialize<Map>();
        Map gameMap;

        //#FPS_COUNTER
        private FPS_Counter counter;
        /// <summary>
        /// Constructor for game, initilaizes the graphics device
        /// Sets root directory, bufer height and width
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentLayer = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerA = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerB = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);

            player = new Player();

            gameMap = new Map();
            gameMap.Player = player;

            //#FPS_COUNTER
            counter = new FPS_Counter(graphics);
            a = new Tile(new Rectangle(0, 0, 64, 64), Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            b = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            c = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
#if DEBUG
            counter.setVisibility(true);
#endif
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    currentLayer.setItemAt(new Vector2(x, y), a);
                    if (x % 3 == 0)
                        currentLayerA.setItemAt(new Vector2(x, y), b);

                    if (y % 3 == 0)
                        currentLayerB.setItemAt(new Vector2(x, y), c);
                }
            }

            gameMap.SwapMaskLayer(currentLayerA);
            gameMap.SwapGoundLayer(currentLayer);
            gameMap.SwapFringeLayer(currentLayerB);
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
            //#FPS_COUNTER
            counter.loadFont(this.Content.Load<SpriteFont>("FPS"));
            Terminal.Init(this, spriteBatch, this.Content.Load<SpriteFont>("FPS"), graphics.GraphicsDevice);
            Terminal.SetSkin(TerminalThemeType.FIRE);
            a.setTexture(this.Content.Load<Texture2D>("Tiles//tile"));
            b.setTexture(this.Content.Load<Texture2D>("Tiles//tileM"));
            c.setTexture(this.Content.Load<Texture2D>("Tiles//tileF"));

            Texture2D playerTexture = Content.Load<Texture2D>("shitty2.0test");

            player.Initialize(playerTexture, new Vector2(0, 0));
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
            Terminal.CheckOpen(Keys.Tab, Keyboard.GetState());
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Tab))
            {
                //  graphics.ToggleFullScreen();
                gameMap.saveMap();

                gameMap.LoadMap("Savegame.xml");
            }

            player.Update(gameTime);
            // TODO: Add your update logic here
            //#FPS_COUNTER
            counter.Update(gameTime);
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
            //#FPS_COUNTER
            counter.Draw(spriteBatch, gameTime);

            //  currentLayer.Draw(spriteBatch, gameTime, Vector2.Zero);
            // gameMap.Player = new Avatar();
            // gameMap.Player.Position = Vector2.Zero;
            gameMap.Draw(spriteBatch, gameTime);
            base.Draw(gameTime);
            Terminal.CheckDraw(false);
        }
    }
}