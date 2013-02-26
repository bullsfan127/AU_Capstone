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

namespace TileEngine
{
    /// <summary>
    /// A layer specifically for drawing.
    /// </summary>
    /// <typeparam name="T">Any Object that implements Drawable</typeparam>
    public class DrawableLayer<T> where T : TileEngine.Interfaces.Drawable
    {
        private T[,] _layer;

        public T[,] Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        //Storage Details for the max map Width and Height
        //Might want to change to a smaller size maybe a short.
        private int _mapWidth;

        public int MapWidth
        {
            get { return _mapWidth; }
            set { _mapWidth = value; }
        }

        private int _mapHeight;

        public int MapHeight
        {
            get { return _mapHeight; }
            set { _mapHeight = value; }
        }

        //max viewport size
        private int _maxViewPortWidth;

        private int _maxViewPortHeight;

        //max tile size for square tiles
        private float _scale;

        // The max tiles to be painted onto the screen.  Perfers squares.
        //TODO: Move allow these values to be set in the
        //Constructor
        private int _maxRows = 10;

        private int _maxColumns = 10;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DrawableLayer() { }

        /// <summary>
        /// The Constuctor for a drawable Layer
        /// </summary>
        /// <param name="size">A Vector for the Size casted when the layer storage is declared</param>
        public DrawableLayer(Vector2 size, GraphicsDevice graphics)
        {
            //Probably going to need to populate the Array
            //Or add a way to swap the layer out entirely.
            _layer = new T[(int)size.X, (int)size.Y];

            //map maxes
            _mapWidth = (int)size.X;
            _mapHeight = (int)size.Y;

            _maxViewPortWidth = graphics.Viewport.Width;
            _maxViewPortHeight = graphics.Viewport.Height;
        }

        /// <summary>
        /// Gets the object at a location
        /// </summary>
        /// <param name="location">The (X,Y) location of the Item</param>
        /// <returns>A reference to the item</returns>
        public T getItemAt(Vector2 location)
        {
            T output;

            try
            {
                output = _layer[(int)location.X, (int)location.Y];
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }

            return (output);
        }

        /// <summary>
        /// Sets the Item at a specific location to a new Item
        /// </summary>
        /// <param name="location">The (X,Y) location</param>
        /// <param name="newItem">the New Item</param>
        public void setItemAt(Vector2 location, T newItem)
        {
            try
            {
                _layer[(int)location.X, (int)location.Y] = newItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// The Draw Method for a Drawable Layer
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch that you are drawing with</param>
        /// <param name="gameTime">GameTime</param>
        /// <param name="centerLocation">Where the tile map should be centered.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 centerLocation)
        {
            //TODO:  Use the Decimal point of the centerLocation to move the tiles to the right by
            //a percentage of the tile width allowing for smooth scrolling.  #TODO

            //TODO: Allow the use of jagged screen sizes IE:  9,10  #TODO

            //TODO: Possibly remove GameTime because its not being used yet.  Mostly used if we wanted to cap he FPS.

            //begin the SpriteBatch
            spriteBatch.Begin();

            _scale = _maxViewPortWidth / _maxColumns;

            int startPosX = (int)centerLocation.X;//Where are we starting horizontally

            //~~~~~~~~~~~~~~~~~DRAW LOGIC~~~~~~~~~~~~~~~~~~~~~
            for (int x = 0; x < _maxColumns; x++)
            {
                int startPosY = 0;
                    //(int)centerLocation.Y;//Where are we starting vertically in the map layer

                for (int y = 0; y < _maxRows; y++)
                {
                    T currentItem = _layer[startPosX, startPosY];//The current drawable item we are working with.

                    //TODO:  Space this out so the draw code is easier to understand.  #TODO
                    if (currentItem != null)
                        spriteBatch.Draw(currentItem.getTexture(),
                                   new Vector2(x * currentItem.getTexture().Width * (_scale / currentItem.getTexture().Width), startPosY * currentItem.getTexture().Height * (_scale / currentItem.getTexture().Width)),
                                   currentItem.getSourceRectangle(),
                                   currentItem.getTint(),
                                   currentItem.getRotation(),
                                   currentItem.getOrigin(),
                                   _scale / currentItem.getTexture().Width,
                                   currentItem.getSpriteEffect(),
                                   currentItem.getDepth());

                    startPosY++;
                }//inner for

                startPosX++;
            }//outer for

            spriteBatch.End();
        }

        /// <summary>
        /// Swaps the layer for another layer through shallow copying
        /// </summary>
        /// <param name="newLayer">The new layer to be swapped out with.</param>
        public void swapLayer(DrawableLayer<T> newLayer)
        {
            //swap the references
            _layer = newLayer._layer;
        }
    }
}