//#define LOAD_FROM_FILE

using System;
using System.Collections.Generic;
using System.Linq;
using CustomSerialization;
using DebugTerminal;
using MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PauseMenu;

using PauseMenu;

using TileEngine;

/**************************************************
 * Added an XNA debugger, it can debug in real time,
 * see http://www.protohacks.net/xna_debug_terminal/HowTo%20v2.1.php5 for usuage.
 * Press the tab key to invoke it
 **************************************************************/

namespace CapstoneProject
{
    public enum GAMESTATE { MAINMENU = 0, PLAY = 1, PAUSE = 2, EXIT = 3 };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Keeps track of overall gamestate
        /// Sets initital sate, if you want to skip the main menu for testing, just set the state to GAMESTATE.PLAY instead
        /// </summary>
        public static GAMESTATE gameState = GAMESTATE.MAINMENU;

        MainMenu.MainMenu menu;
        PauseMenu.PauseMenu pauseMenu;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Coin coin = new Coin();

#if !LOAD_FROM_FILE
        Tile a;
        Tile b;
        Tile c;

        DrawableLayer<Tile> currentLayer;
        DrawableLayer<Tile> currentLayerA;
        DrawableLayer<Tile> currentLayerB;
#endif

        // Represents the player
        Player player;

        //Health Bar
        HealthBar healthBar;

        Serialize<Map> serializer = new Serialize<Map>();
        Map gameMap;

#if DEBUG

        //#FPS_COUNTER
        private FPS_Counter counter;

#endif

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
            menu = new MainMenu.MainMenu(graphics, this.Content);
            pauseMenu = new PauseMenu.PauseMenu(graphics, this.Content);
            healthBar = new HealthBar(graphics, this.Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            menu.Initialize(this.Window);
            pauseMenu.Initialize(this.Window);

            IsMouseVisible = true;

            player = new Player();
            gameMap = new Map();
#if !LOAD_FROM_FILE
            currentLayer = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerA = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerB = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
#else
            gameMap = gameMap.LoadMap("Savegame.xml");
            gameMap.loadTiles(this.Content);
            player = (Player)gameMap.Player;
#endif

#if !LOAD_FROM_FILE
            gameMap.Player = player;

            a = new Tile(new Rectangle(0, 0, 64, 64), Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            b = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            c = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

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
#endif
#if DEBUG
            //#FPS_COUNTER
            counter = new FPS_Counter(graphics);
            counter.setVisibility(true);
#endif
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            menu.LoadContent();
            pauseMenu.LoadContent();
            healthBar.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
#if DEBUG
            //#FPS_COUNTER
            counter.loadFont(this.Content.Load<SpriteFont>("FPS"));
            Terminal.Init(this, spriteBatch, this.Content.Load<SpriteFont>("FPS"), graphics.GraphicsDevice);
            Terminal.SetSkin(TerminalThemeType.FIRE);
#endif
#if !LOAD_FROM_FILE
            a.setTexture(this.Content.Load<Texture2D>("Tiles//tile"));
            a.Name = "Tiles//tile";
            b.setTexture(this.Content.Load<Texture2D>("Tiles//tileM"));
            b.Name = "Tiles//tileM";
            c.setTexture(this.Content.Load<Texture2D>("Tiles//tileF"));
            c.Name = "Tiles//tileF";

            Texture2D playerTexture = Content.Load<Texture2D>("shitty3.0");

            player.Initialize(playerTexture, new Vector2(0, 0));

            Texture2D coinTexture = Content.Load<Texture2D>("Coin");
            coin.Initialize(coinTexture, new Vector2(19, 19));
            // TODO: use this.Content to load your game content here
#endif
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
            switch (gameState)
            {
                case GAMESTATE.MAINMENU:
                    menu.Update(gameTime);
                    break;
                case GAMESTATE.PAUSE:
                    pauseMenu.Update(gameTime);
                    break;
                case GAMESTATE.PLAY:
                    healthBar.Update(gameTime);
                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();

                    KeyboardState keystate = Keyboard.GetState();
                    if (keystate.IsKeyDown(Keys.P))
                    {
                        CapstoneProject.Game1.gameState = CapstoneProject.GAMESTATE.PAUSE;
                    }
                    if (keystate.IsKeyDown(Keys.S))
                    {
                        //  graphics.ToggleFullScreen();
                        gameMap.saveMap();
                        Map gameMap2 = gameMap;
                        gameMap = null;
                        gameMap = new Map();
                        gameMap = gameMap.LoadMap("Savegame.xml");
                        gameMap.loadTiles(this.Content);
                        player = (Player)gameMap.Player;
                    }

                    player.Update(gameTime, gameMap);
                    // TODO: Add your update logic here

                    break;
            }

#if DEBUG

            //#FPS_COUNTER
            counter.Update(gameTime);
            Terminal.CheckOpen(Keys.Tab, Keyboard.GetState());
#endif

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GAMESTATE.MAINMENU:
                    GraphicsDevice.Clear(Color.Black);
                    menu.Draw(gameTime, spriteBatch);
                    break;
                case GAMESTATE.PLAY:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    gameMap.Draw(spriteBatch, gameTime);
                    healthBar.Draw(gameTime, spriteBatch);
                    // TODO: Loop through all items instead of calling each one individually
                    coin.Draw(spriteBatch, gameTime);
                    break;
                case GAMESTATE.PAUSE:
                    // CapstoneProject.Game1.gameState = CapstoneProject.GAMESTATE.PAUSE;
                    GraphicsDevice.Clear(Color.Beige);
                    pauseMenu.Draw(gameTime, spriteBatch);
                    break;
                case GAMESTATE.EXIT:
                    this.Exit();
                    break;
            }

#if DEBUG
            //#FPS_COUNTER
            counter.Draw(spriteBatch, gameTime);
            Terminal.CheckDraw(false);
#endif

            base.Draw(gameTime);
        }
    }
}