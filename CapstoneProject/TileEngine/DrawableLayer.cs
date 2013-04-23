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

        public int MaxViewPortWidth
        {
            get { return _maxViewPortWidth; }
            set { _maxViewPortWidth = value; }
        }

        public int MaxViewPortHeight
        {
            get { return _maxViewPortHeight; }
            set { _maxViewPortHeight = value; }
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public int MaxRows
        {
            get { return _maxRows; }
            set { _maxRows = value; }
        }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set { _maxColumns = value; }
        }

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

        public T getItemAt(Vector2 location, Avatar a)
        {
            T output;

            try
            {
                output = _layer[(int)(location.X + a.Position.X), (int)(location.Y + a.Position.Y)];
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

        public void setItemAt(Vector2 location, T newItem, Avatar a)
        {
            try
            {
                _layer[(int)(location.X + a.Position.X), (int)(location.Y + a.Position.Y)] = newItem;
            }
            catch (IndexOutOfRangeException e)
            {
                e.ToString();
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

            int startPosX = (int)centerLocation.X / 64;//Where are we starting horizontally

            //~~~~~~~~~~~~~~~~~DRAW LOGIC~~~~~~~~~~~~~~~~~~~~~
            for (int x = 0; x < _maxColumns + 1; x++)
            {
                int startPosY = 0;
                //(int)centerLocation.Y;//Where are we starting vertically in the map layer

                for (int y = 0; y < _maxRows; y++)
                {
                    T currentItem = _layer[startPosX, startPosY];//The current drawable item we are working with.

                    //TODO:  Space this out so the draw code is easier to understand.  #TODO
                    if (currentItem != null)
                        spriteBatch.Draw(currentItem.getTexture(),
                                   new Vector2(x * currentItem.getTexture().Width * (_scale / currentItem.getTexture().Width) - (int)centerLocation.X % Map.TILE_WIDTH,
                                       startPosY * currentItem.getTexture().Height * (_scale / currentItem.getTexture().Width)),
                                   currentItem.getSourceRectangle(),
                                   currentItem.getTint(),
                                   currentItem.getRotation(),
                                   currentItem.getOrigin(),
                                   _scale / Map.TILE_WIDTH,
                                   currentItem.getSpriteEffect(),
                                   currentItem.getDepth());

                    startPosY++;
                }//inner for

                startPosX++;
            }//outer for

            spriteBatch.End();
        }

        /// <summary>
        /// The Draw Method for a Drawable Layer
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch that you are drawing with</param>
        /// <param name="gameTime">GameTime</param>
        /// <param name="centerLocation">Where the tile map should be centered.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 centerLocation, Rectangle containedWithin, Avatar player)
        {
            //TODO:  Use the Decimal point of the centerLocation to move the tiles to the right by
            //a percentage of the tile width allowing for smooth scrolling.  #TODO

            //TODO: Allow the use of jagged screen sizes IE:  9,10  #TODO

            //TODO: Possibly remove GameTime because its not being used yet.  Mostly used if we wanted to cap he FPS.

            //begin the SpriteBatch
            spriteBatch.Begin();

            _scale = containedWithin.Width / _maxColumns;

            int startPosX = (int)centerLocation.X / 64;//Where are we starting horizontally

            //~~~~~~~~~~~~~~~~~DRAW LOGIC~~~~~~~~~~~~~~~~~~~~~
            for (int x = 0; x < _maxColumns; x++)
            {
                int startPosY = 0;
                //(int)centerLocation.Y;//Where are we starting vertically in the map layer

                for (int y = 0; y < _maxRows; y++)
                {
                    T currentItem = _layer[startPosX + (int)player.Position.X, startPosY + (int)player.Position.Y];//The current drawable item we are working with.
                    int renderTargetX = x;

                    //TODO:  Space this out so the draw code is easier to understand.  #TODO
                    if (currentItem != null)
                        //spriteBatch.Draw(currentItem.getTexture(),
                        //           new Vector2(renderTargetX * currentItem.getTexture().Width
                        //                         * (_scale / currentItem.getTexture().Width)
                        //                         - (int)centerLocation.X % 64 + containedWithin.X,
                        //               startPosY * currentItem.getTexture().Height * (_scale / currentItem.getTexture().Width)+ containedWithin.Y),
                        //           currentItem.getSourceRectangle(),
                        //           currentItem.getTint(),
                        //           currentItem.getRotation(),
                        //           currentItem.getOrigin(),
                        //           _scale / currentItem.getTexture().Width,
                        //           currentItem.getSpriteEffect(),
                        //           currentItem.getDepth());
                        spriteBatch.Draw(currentItem.getTexture(), new Rectangle((int)(renderTargetX * (containedWithin.Width / 10) + containedWithin.X),
                                                                                 (int)(y * (containedWithin.Height / 10) + containedWithin.Y),
                                                                                 containedWithin.Width / 10,
                                                                                 containedWithin.Height / 10)
                                                                                 , currentItem.getSourceRectangle(),
                            currentItem.getTint(), currentItem.getRotation(), currentItem.getOrigin(), currentItem.getSpriteEffect(), currentItem.getDepth());

                    startPosY++;
                }//inner for

                startPosX++;
            }//outer for

            spriteBatch.End();
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~``
        /// <summary>
        /// The Draw Method for a Drawable Layer
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch that you are drawing with</param>
        /// <param name="gameTime">GameTime</param>
        /// <param name="centerLocation">Where the tile map should be centered.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 centerLocation, Avatar player)
        {
            Rectangle containedWithin = new Rectangle(0, 0, MaxViewPortWidth, MaxViewPortHeight);
            
            //If there there is an offset we need to draw an extra tile
            int offsetIncrease = 0;

            //If there is a deminsion to the offset
            if ((player.Offset.X > 0) || (player.Offset.X < 0) || (player.Offset.X > 0) || (player.Offset.X < 0))
                offsetIncrease = 1;

            //begin the SpriteBatch
            spriteBatch.Begin();

            _scale = containedWithin.Width / _maxColumns;

            int startPosX = (int)centerLocation.X / 64;//Where are we starting horizontally

            //~~~~~~~~~~~~~~~~~DRAW LOGIC~~~~~~~~~~~~~~~~~~~~~
            for (int x = 0; x < _maxColumns + offsetIncrease; x++)
            {
                int startPosY = 0;
                //(int)centerLocation.Y;//Where are we starting vertically in the map layer

                for (int y = 0; y < _maxRows + offsetIncrease; y++)
                {
                    T currentItem = _layer[startPosX + (int)player.Position.X, startPosY + (int)player.Position.Y];//The current drawable item we are working with.
                    int renderTargetX = x;

                    //TODO:  Space this out so the draw code is easier to understand.  #TODO
                    if (currentItem != null)
                        spriteBatch.Draw(currentItem.getTexture(), new Rectangle((int)(renderTargetX * (containedWithin.Width / 10) + containedWithin.X - player.Offset.X),
                                                                                 (int)(y * (containedWithin.Height / 10) + containedWithin.Y - player.Offset.Y),
                                                                                 containedWithin.Width / 10,
                                                                                 containedWithin.Height / 10)
                                                                                 , currentItem.getSourceRectangle(),
                            currentItem.getTint(), currentItem.getRotation(), currentItem.getOrigin(), currentItem.getSpriteEffect(), currentItem.getDepth());

                    startPosY++;
                }//inner for

                startPosX++;
            }//outer for

            spriteBatch.End();
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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