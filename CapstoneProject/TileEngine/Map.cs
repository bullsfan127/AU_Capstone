using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Polenter.Serialization;

namespace TileEngine
{
    /// <summary>
    /// A Map is the basic structure for storing the tile engine data.
    /// That includes tileSheets, Tiles, layers, npcs and anything else the player interacts with.
    /// Does practically all the heavy lifting.
    /// </summary>
    public class Map
    {
        //offset to determine center of screen.
        Vector2 offset = Vector2.Zero;

        CustomSerialization.Serialize<Map> serializer = new CustomSerialization.Serialize<Map>();

        //For now I'm hard coding the layers eventually we want to move to a list
        //based system.
        DrawableLayer<Tile> _Ground;

        public DrawableLayer<Tile> Ground
        {
            get { return _Ground; }
            set { _Ground = value; }
        }

        DrawableLayer<Tile> _Mask;

        public DrawableLayer<Tile> Mask
        {
            get { return _Mask; }
            set { _Mask = value; }
        }

        DrawableLayer<Tile> _Fringe;

        public DrawableLayer<Tile> Fringe
        {
            get { return _Fringe; }
            set { _Fringe = value; }
        }

        //Layers for collision detection
        Layer<Rectangle> _CollisionLayer;

        //Abstract Type for Player
        Avatar _Player;

        public Avatar Player
        {
            get { return _Player; }
            set { _Player = value; }
        }

        /// <summary>
        /// Default Doesn't do anything.
        /// </summary>
        public Map() { }

        //TODO:  Probably need to add a constuctor the gets the graphics device and does its "magic"
        public Map(DrawableLayer<Tile> Ground, DrawableLayer<Tile> Mask, DrawableLayer<Tile> Fringe)
        {
            _Ground = Ground;
            _Mask = Mask;
            _Fringe = Fringe;
        }

        /// <summary>
        /// Updates all map specific components
        /// </summary>
        /// <param name="gameTime">The gameTime snapshot</param>
        public void Update(GameTime gameTime, Vector2 offset)
        {
            this.offset = offset;
            if (this.offset.X < 0)
            {
                this.offset.X = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //TODO:  Add a way to calculate the where the center should be based on the player.
            _Ground.Draw(spriteBatch, gameTime, offset);
            _Mask.Draw(spriteBatch, gameTime, offset);
            _Player.Draw(spriteBatch, gameTime);
            _Fringe.Draw(spriteBatch, gameTime, offset);
        }
        public void DrawInWindow(SpriteBatch spriteBatch, GameTime gameTime, Rectangle Location)
        {
            _Ground.Draw(spriteBatch, gameTime, offset, Location);
            _Mask.Draw(spriteBatch, gameTime, offset, Location);
            _Player.Draw(spriteBatch, gameTime);
            _Fringe.Draw(spriteBatch, gameTime, offset, Location);
     
        }

        /// <summary>
        /// Swaps out the Ground Layer
        /// </summary>
        /// <param name="swapLayer">The layer to be added to the Map</param>
        public void SwapGoundLayer(DrawableLayer<Tile> swapLayer)
        {
            if (_Ground == null)
                _Ground = swapLayer;
            else
                _Ground.swapLayer(swapLayer);
        }

        public void SwapMaskLayer(DrawableLayer<Tile> swapLayer)
        {
            if (_Mask == null)
                _Mask = swapLayer;
            else
                _Mask.swapLayer(swapLayer);
        }

        public void SwapFringeLayer(DrawableLayer<Tile> swapLayer)
        {
            if (_Fringe == null)
                _Fringe = swapLayer;
            else
                _Fringe.swapLayer(swapLayer);
        }

        /// <summary>
        /// Save map to disk
        /// </summary>
        /// <param name="filename">The name you want to give the xml file</param>
        public void saveMap(string filename = "SaveGame.xml")
        {
            serializer.FileName = filename;
            serializer.Save(this, true);
        }

        /// <summary>
        /// Loads map from disk
        /// </summary>
        /// <param name="filename">Name of map being loaded</param>
        /// <param name="contentManager">The contentManager to load textures</param>
        /// <returns></returns>
        public Map LoadMap(string filename, ContentManager contentManager)
        {
            Map newMap = new Map();
            newMap = serializer.Load("SaveGame.xml");
            newMap.LoadExtraContent(contentManager);
            return newMap;
        }

        /// <summary>
        /// Loads extra content, that Serialization doesn't pick up
        /// </summary>
        /// <param name="contentManager">The content Manager to load textures</param>
        public void LoadExtraContent(ContentManager contentManager)
        {
            //Load layers and tiles
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if (this._Fringe.Layer[x, y] != null)
                    {
                        this._Fringe.Layer[x, y].setTexture(contentManager.Load<Texture2D>(this._Fringe.Layer[x, y].Name));
                        this._Fringe.Layer[x, y].setSourceRectangle(new Rectangle(0, 0, 64, 64));
                    }

                    if (this._Ground.Layer[x, y] != null)
                    {
                        this._Ground.Layer[x, y].setTexture(contentManager.Load<Texture2D>(this._Ground.Layer[x, y].Name));
                        this._Ground.Layer[x, y].setSourceRectangle(new Rectangle(0, 0, 64, 64));
                    }

                    if (this._Mask.Layer[x, y] != null)
                    {
                        this._Mask.Layer[x, y].setTexture(contentManager.Load<Texture2D>(this._Mask.Layer[x, y].Name));
                        this._Mask.Layer[x, y].setSourceRectangle(new Rectangle(0, 0, 64, 64));
                    }
                }
            }

            //load player
            Texture2D playerTexture = contentManager.Load<Texture2D>("shitty3.0");
            Vector2 playerPosition = new Vector2(this._Player.X, this._Player.Y);
            this._Player.Position = playerPosition;

            //load playerAnimation
            Rectangle srect = new Rectangle();
            srect = new Rectangle(this._Player.PlayerAnimation.SRect[0], this._Player.PlayerAnimation.SRect[1], this._Player.PlayerAnimation.SRect[2], this._Player.PlayerAnimation.SRect[3]);
            this._Player.PlayerAnimation.sourceRect = srect;
            this._Player.PlayerAnimation.spriteStrip = playerTexture;
            this._Player.PlayerAnimation.destinationRect = new Rectangle(this._Player.PlayerAnimation.DRect[0], this._Player.PlayerAnimation.DRect[1], this._Player.PlayerAnimation.DRect[2], this._Player.PlayerAnimation.DRect[3]);
            this._Player.PlayerAnimation.Position = this._Player.Position;
            this._Player.Offset = new Vector2(this._Player.OX, this.Player.OY);
        }
    }
}