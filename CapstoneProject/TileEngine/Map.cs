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

namespace TileEngine
{
    /// <summary>
    /// A Map is the basic structure for storing the tile engine data.  
    /// That includes tileSheets, Tiles, layers, npcs and anything else the player interacts with.
    /// Does practically all the heavy lifting.
    /// </summary>
    public class Map
    {
        //For now I'm hard coating the layers eventually we want to move to a list
        //based system.
        DrawableLayer<Tile> _Ground;
        DrawableLayer<Tile> _Mask;
        DrawableLayer<Tile> _Fringe;

        //Layers for collision detection
        Layer<Rectangle> _CollisionLayer;
        
        //Abstract Type for Player
        Avatar _Player;

        /// <summary>
        /// Default Doesn't do anything.
        /// </summary>
        public Map(){}
        
        //TODO:  Probably need to add a constuctor the gets the graphics device and does its "magic"

        /// <summary>
        /// Updates all map specific components
        /// </summary>
        /// <param name="gameTime">The gameTime snapshot</param>
        public void Update(GameTime gameTime)
        {
            _Player.Update(gameTime);
        
        
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //TODO:  Add a way to calculate the where the center should be based on the player.

            
            _Ground.Draw(spriteBatch, gameTime, _Player.Position);
            _Mask.Draw(spriteBatch, gameTime, _Player.Position);
            _Player.Draw(spriteBatch, gameTime);
            _Fringe.Draw(spriteBatch, gameTime, _Player.Position);
                  
        }
    }
}
