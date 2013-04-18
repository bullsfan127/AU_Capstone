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
using TileEngine;

/**************************************************
 * Added an XNA debugger, it can debug in real time,
 * see http://www.protohacks.net/xna_debug_terminal/HowTo%20v2.1.php5 for usuage.
 * Press the tab key to invoke it
 **************************************************************/

namespace CapstoneProject
{
    public enum GAMESTATE { MAINMENU = 0, PLAY = 1, PAUSE = 2, EXIT = 3, SETTINGS = 4, NEWGAME = 5, CONTINUE = 6, MAINSETTINGS = 7 };


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

        Texture2D backgroundTexture;
        Texture2D pauseBackground;

        MainMenu.MainMenu menu;
        PauseMenu.PauseMenu pauseMenu;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Coin coin = new Coin();
        Potion potion = new Potion();
        GroundMonster groundMonster = new GroundMonster();
        FlyingMonster flyingMonster = new FlyingMonster();
        Settings settings;
        MainSettings msettings;
       

        SoundManager soundManager;

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

        Rectangle mainFrame;
        Rectangle pauseMainFrame;

        //Health Bar
        HealthBar healthBar;

        ScoreDisplay scoreDisplay;

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
            settings = new Settings(graphics, this.Content);
            msettings = new MainSettings(graphics, this.Content);

            soundManager = new SoundManager(this, this.Content);
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
            scoreDisplay = new ScoreDisplay(graphics);

            IsMouseVisible = true;

            player = new Player(this.Content);
            gameMap = new Map();

#if !LOAD_FROM_FILE
            currentLayer = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerA = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
            currentLayerB = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);

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
            settings.LoadContent();
            msettings.LoadContent();
            Controls.Load();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            pauseMainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
#if DEBUG
            //#FPS_COUNTER
            counter.loadFont(this.Content.Load<SpriteFont>("FPS"));
            scoreDisplay.loadFont(this.Content.Load<SpriteFont>("FPS"));
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


            // TODO: use this.Content to load your game content here
#endif

            // Texture2D playerTexture = Content.Load<Texture2D>("shitty/FatJoe");
            Texture2D playerTexture = Content.Load<Texture2D>("shitty/FatJoe");
            player.Initialize(playerTexture, new Vector2(0, 0));
            backgroundTexture = Content.Load<Texture2D>("background");
            pauseBackground = Content.Load<Texture2D>("PauseBackground");
            Texture2D coinTexture = Content.Load<Texture2D>("items/Coin");
            Texture2D potionTexture = Content.Load<Texture2D>("items/Potion");
            Texture2D groundMonsterTexture = Content.Load<Texture2D>("Monsters/Zombies");
            Texture2D flyingMonsterTexture = Content.Load<Texture2D>("Monsters/Birds");
            coin.Initialize(coinTexture, new Vector2(19, 19));
            potion.Initialize(potionTexture, new Vector2(83, 83));
            groundMonster.Initialize(groundMonsterTexture, new Vector2(250, 250));
            flyingMonster.Initialize(flyingMonsterTexture, new Vector2(150, 150));
            scoreDisplay.loadFont(this.Content.Load<SpriteFont>("FPS"));
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
                case GAMESTATE.SETTINGS:
                    settings.Update(gameTime);
                    break;
                case GAMESTATE.NEWGAME:
                    //gameMap = gameMap.LoadMap("map.xml", this.Content);
                    //player = null;
                    //player = new Player(this.Content);
                    //Texture2D playerTexture = Content.Load<Texture2D>("shitty/shitty3.0");
                    //player.Initialize(playerTexture, new Vector2(0, 0));
                    //gameMap.Player = player;
                    gameState = GAMESTATE.PLAY;
                    break;
                case GAMESTATE.CONTINUE:
                    try
                    {
                        gameMap = gameMap.LoadMap("SavedGame.xml", this.Content);
                        gameMap.LoadPlayer(this.Content);
                        player = (Player)gameMap.Player;
                    }
                    catch (Exception e)
                    {
                        gameMap = gameMap = gameMap.LoadMap("map.xml", this.Content);
                        gameMap.Player = player;
                    }
                    gameState = GAMESTATE.PLAY;
                    break;
                case GAMESTATE.MAINSETTINGS:
                    msettings.Update(gameTime);
                    break;
                case GAMESTATE.PLAY:

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
                        //  save map
                        gameMap.saveMap("SavedGame.xml");
                    }

                    //if (keystate.IsKeyDown(Keys.L))
                    //{
                    //    gameMap = null;
                    //    gameMap = new Map();
                    //    gameMap = gameMap.LoadMap("../../../../../MapEditor/MapEditor/SavedMaps/test.xml", this.Content);
                    //    gameMap.Player = null;
                    //    gameMap.Player = player;
                    //}

                    if (keystate.IsKeyDown(Keys.F))
                    {
                        graphics.ToggleFullScreen();
                    }

                    //if (keystate.IsKeyDown(Keys.Up) && player.Position.Y == 372)
                    //{
                    //    soundManager.PlaySound(5);
                    //}

                    player.Update(gameTime, gameMap);
                    groundMonster.Update(gameTime);
                    flyingMonster.Update(gameTime);
                    healthBar.Update(gameTime, player);
                    scoreDisplay.Update(player.getLevelScore());
                    // TODO: Add your update logic here

                    break;
            }

#if DEBUG

            //#FPS_COUNTER
            counter.Update(gameTime);
            Terminal.CheckOpen(Keys.Tab, Keyboard.GetState());
#endif
            soundManager.PlaySong();
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

                    menu.Draw(gameTime, spriteBatch, this.backgroundTexture, mainFrame);
                    break;
                case GAMESTATE.PLAY:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    gameMap.Draw(spriteBatch, gameTime);
                    healthBar.Draw(gameTime, spriteBatch);
                    // TODO: Loop through all items instead of calling each one individually
                    coin.Draw(spriteBatch, gameTime);
                    potion.Draw(spriteBatch, gameTime);
                    groundMonster.Draw(spriteBatch, gameTime);
                    flyingMonster.Draw(spriteBatch, gameTime);
                    scoreDisplay.Draw(spriteBatch, gameTime);
                    break;
                case GAMESTATE.PAUSE:
                    GraphicsDevice.Clear(Color.Gray);
                    pauseMenu.Draw(gameTime, spriteBatch,this.pauseBackground, pauseMainFrame);
                    break;
                case GAMESTATE.SETTINGS:
                    GraphicsDevice.Clear(Color.Black);
                    settings.Draw(gameTime, spriteBatch);
                    break;
                case GAMESTATE.MAINSETTINGS:
                    GraphicsDevice.Clear(Color.Black);
                    msettings.Draw(gameTime, spriteBatch);
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