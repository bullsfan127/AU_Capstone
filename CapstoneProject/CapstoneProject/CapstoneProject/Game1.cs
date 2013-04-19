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
    public enum GAMESTATE { MAINMENU = 0, PLAY = 1, PAUSE = 2, EXIT = 3, SETTINGS = 4, NEWGAME = 5, CONTINUE = 6, MAINSETTINGS = 7, STORY = 8, SAVE = 9 };

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
        Texture2D story;
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

        ImageButton pauseButton;
        Texture2D pauseTexture;

        SoundManager soundManager;

        // Represents the player
        Player player;

        Rectangle mainFrame;
        Rectangle pauseMainFrame;

        //Health Bar
        HealthBar healthBar;

        Texture2D playerTexture;
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

            playerTexture = Content.Load<Texture2D>("shitty/FatJoe");
            player.Initialize(playerTexture, new Vector2(0, 0));
            pauseTexture = Content.Load<Texture2D>("PauseButton");
            pauseButton = new ImageButton(new Vector2(60, 19), pauseTexture, 0);
            backgroundTexture = Content.Load<Texture2D>("background");
            pauseBackground = Content.Load<Texture2D>("PauseBackground");
            Texture2D coinTexture = Content.Load<Texture2D>("items/Coin");
            coinTexture.Name = "items/Coin";
            Texture2D potionTexture = Content.Load<Texture2D>("items/Potion");
            potionTexture.Name = "items/Potion";
            Texture2D groundMonsterTexture = Content.Load<Texture2D>("Monsters/Zombies");
            groundMonsterTexture.Name = "Monsters/Zombies";
            Texture2D flyingMonsterTexture = Content.Load<Texture2D>("Monsters/Birds");
            flyingMonsterTexture.Name = "Monsters/Birds";

            story = this.Content.Load<Texture2D>("story");
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
                case GAMESTATE.SAVE:
                    gameMap.saveMap("SavedGame.xml");
                    gameState = GAMESTATE.PAUSE;
                    break;
                case GAMESTATE.SETTINGS:
                    settings.Update(gameTime);
                    break;
                case GAMESTATE.NEWGAME:
                    gameMap = gameMap.LoadMap("map.xml", this.Content);
                    gameMap.NpcList.Add(flyingMonster);
                    gameMap.NpcList.Add(groundMonster);
                    gameMap.MapItems.Add(coin);
                    gameMap.MapItems.Add(potion);

                    player = null;
                    player = new Player(this.Content);
                    player.Initialize(playerTexture, new Vector2(0, 0));
                    gameMap.Player = player;
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

                    if (keystate.IsKeyDown(Keys.F))
                    {
                        graphics.ToggleFullScreen();
                    }

                    if (keystate.IsKeyDown(Keys.S))
                    {
                        gameMap.saveMap("SavedGame.xml");
                    }

                    player.Update(gameTime, gameMap);
                    pauseButton.Update(gameTime);
                    healthBar.Update(gameTime, player);
                    scoreDisplay.Update(player.getLevelScore());

                    #region

                    if (keystate.IsKeyDown(Keys.S) && keystate.IsKeyDown(Keys.H) && keystate.IsKeyDown(Keys.I) && keystate.IsKeyDown(Keys.T))
                    {
                        playerTexture = Content.Load<Texture2D>("shitty/shitty3.0");
                        player.Initialize(playerTexture, player.Position);
                    }

                    #endregion

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

                case GAMESTATE.SAVE:
                    pauseMenu.Draw(gameTime, spriteBatch, this.pauseBackground, pauseMainFrame);
                    break;
                case GAMESTATE.STORY:
                    spriteBatch.Begin();
                    spriteBatch.Draw(story, mainFrame, Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        gameState = GAMESTATE.NEWGAME;
                    spriteBatch.End();
                    break;
                case GAMESTATE.PLAY:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    gameMap.Draw(spriteBatch, gameTime);
                    healthBar.Draw(gameTime, spriteBatch);
                    // TODO: Loop through all items instead of calling each one individually
                    //  coin.Draw(spriteBatch, gameTime);
                    //   potion.Draw(spriteBatch, gameTime);
                    scoreDisplay.Draw(spriteBatch, gameTime);
                    pauseButton.Draw(gameTime, spriteBatch);
                    break;
                case GAMESTATE.PAUSE:
                    // GraphicsDevice.Clear(Color.Gray);
                    pauseMenu.Draw(gameTime, spriteBatch, this.pauseBackground, pauseMainFrame);
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