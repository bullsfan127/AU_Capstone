using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tile a;
        Tile b;
        DrawableLayer<Tile> currentLayer;
        //#FPS_COUNTER
        private FPS_Counter counter;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            
            //#FPS_COUNTER
            counter = new FPS_Counter(graphics);
            a = new Tile(new Rectangle(0, 0, 64, 64), Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            b = new Tile(new Rectangle(0, 0, 64, 64), Color.ForestGreen, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
#if DEBUG
            counter.setVisibility(true);
#endif
            for(int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if (y % 3 == 0)
                        currentLayer.setItemAt(new Vector2(x, y), a);
                    else
                        currentLayer.setItemAt(new Vector2(x, y), b);
                }
            }
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
            a.setTexture(this.Content.Load<Texture2D>("Tiles//tile"));
            b.setTexture(this.Content.Load<Texture2D>("Tiles//tile"));
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

            currentLayer.Draw(spriteBatch, gameTime, Vector2.Zero);
            base.Draw(gameTime);
        }
    }
}
