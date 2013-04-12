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
   public class Layer<T>
    {
       private T[,] layer;

       /// <summary>
       /// The Constuctor for a generic Layer
       /// </summary>
       /// <param name="size">A Vector for the Size casted when the layer storage is declared</param>
       public Layer(Vector2 size)
       {
           //Probably going to need to populate the Array
           layer = new T[(int)size.X, (int)size.Y];
          
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
        
       public void setItemAt(Vector2 location, T newItem, Avatar a)
       {
           try
           {
               layer[(int)(location.X + a.Position.X), (int)(location.Y + a.Position.Y)] = newItem;
           }
           catch (IndexOutOfRangeException e)
           {
               //throw e;
           }
       }

   
   }
}
