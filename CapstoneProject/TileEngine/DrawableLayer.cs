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
  
    public class DrawableLayer<T> where T : TileEngine.Interfaces.Drawable
    {

      private T[,] layer;
      private int mapWidth;
      private int mapHeight;
      //max pixels
      private int maxX;
      private int maxY;
      //max tile size for square tiles
      private int scale;
      // max tiles for the rows and columns
      private int rows;
      private int columns;

       /// <summary>
       /// The Constuctor for a drawable Layer
       /// </summary>
       /// <param name="size">A Vector for the Size casted when the layer storage is declared</param>
       public DrawableLayer(Vector2 size, GraphicsDevice graphics)
       {
           //Probably going to need to populate the Array
           layer = new T[(int)size.X, (int)size.Y];

           //map maxes
           mapWidth = (int)size.X;
           mapHeight = (int)size.Y;

           maxX = graphics.Viewport.Width;
           maxY = graphics.Viewport.Height;

           //find the proper tile size
           scale = TileEngine.Utility.TileMath.gcd(maxY, maxX);

           if (scale > 64)
               scale = TileEngine.Utility.TileMath.gcd(scale, 32);

           rows = maxX / scale;
           columns = maxY / scale;
          
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
               output = layer[(int)location.X,(int) location.Y];
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
               layer[(int)location.X, (int)location.Y] = newItem;
           }
           catch (IndexOutOfRangeException e)
           {
               throw e;
           }

       }

       public void Draw(SpriteBatch batch, GameTime gameTime, Vector2 location)
       {

           int startPosX;
           int startPosY;

           if (location.X < columns)
               startPosX = 0;
           else//later we'll modify this to add perpixel scrolling
               startPosX = (int)location.X;

           if (location.Y < rows)
               startPosY = 0;
           else
               startPosY = (int)location.Y;

           //begin the SpriteBatch
           batch.Begin();
            int _X = 0; //viewport starting pos
           //~~~~~~~~~~~~~~~~~DRAW LOGIC~~~~~~~~~~~~~~~~~~~~~
           for (int x = startPosX; x < (startPosX + columns); x++)
           {
            
               int _Y = 0;
               for (int y = startPosY; y < (startPosY + rows); y++)
               {
                 T currentItem = layer[x, y];

                 batch.Draw(currentItem.getTexture(),
                            new Vector2(_X * scale, _Y * scale),
                            currentItem.getSourceRectangle(),
                            currentItem.getTint(),
                            currentItem.getRotation(),
                            currentItem.getOrigin(),
                            scale, 
                            currentItem.getSpriteEffect(),
                            currentItem.getDepth());
                            
                            


               }//inner for
            _Y++;
            _X++;
           }//outer for


           batch.End();


       }
        
        

    }
}
