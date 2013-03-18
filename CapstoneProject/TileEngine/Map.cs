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

        //For now I'm hard coding the layers eventually we want to move to a list
        //based system.
        DrawableLayer<Tile> _Ground;

        CustomSerialization.Serialize<Map> serializer = new CustomSerialization.Serialize<Map>();

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
        public void Update(GameTime gameTime,Vector2 offset)
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

        public void saveMap(/*string filename*/)
        {
            serializer.Save(this, true);
        }

        public void LoadMap(string filename)
        {
            serializer.Load("SaveGame.xml");
        }
    }
}