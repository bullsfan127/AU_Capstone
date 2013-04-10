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
using TileEngine;
namespace MapEditor.GUI
{
   public class MapWindow : IDrawable, IUpdateable
   {

       #region AutoImplements
       /// <summary>
       /// I believe this is for priority rendering
       /// </summary>
        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> UpdateOrderChanged;
        public bool Visible
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> VisibleChanged;

        public bool Enabled
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> EnabledChanged;
       #endregion //only here because this section is annoying
        /// <summary>
        /// The location on the screen where we will be rendering the map.
        /// </summary>
        Rectangle _location;

        public Rectangle Location
        {
            get { return _location; }
            set { _location = value; }
        }
        SpriteBatch _spritebatch;
        Map _map;
        DrawableLayer<Tile> _ground;
        DrawableLayer<Tile> _mask;
        DrawableLayer<Tile> _Fringe;
        
        Tile a;
        Tile b;
        Tile c;
        DrawableLayer<Tile> currentLayer;
        DrawableLayer<Tile> currentLayerA;
        DrawableLayer<Tile> currentLayerB;

        FocalPoint _center = new FocalPoint(new Vector2(10, 10), 10, 100, 50, 50);
       // enum currentLayer { GROUND, MASK, FRINGE }
        /// <summary>
        /// For determining what layer you are laying tiles in.
        /// </summary>
       // currentLayer _currentLayer = currentLayer.GROUND;
       public MapWindow(Rectangle Location, SpriteBatch batch)
        {
            _location = Location;
            _spritebatch = batch;
        
        }
       public MapWindow(SpriteBatch batch, GraphicsDeviceManager graphics, ContentManager Content)
       {
           _spritebatch = batch;
           _location = new Rectangle(100, 100, 400, 400);
           currentLayer = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
           currentLayerA = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
           currentLayerB = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
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
           a.setTexture(Content.Load<Texture2D>("Tiles//tile"));
           a.Name = "Tiles//tile";
           b.setTexture(Content.Load<Texture2D>("Tiles//tileM"));
           b.Name = "Tiles//tileM";
           c.setTexture(Content.Load<Texture2D>("Tiles//tileF"));
           c.Name = "Tiles//tileF";
           _map = new Map();
           _map.SwapMaskLayer(currentLayerA);
           _map.SwapGoundLayer(currentLayer);
           _map.SwapFringeLayer(currentLayerB);
       
       }
        public void Initialize()
        {
            _ground = new DrawableLayer<Tile>();
            _mask = new DrawableLayer<Tile>();
            _Fringe = new DrawableLayer<Tile>();
        
        }


        public void Update(GameTime gameTime)
        {
            _center.Update(gameTime);
          
           /*
            _map = new Map(_ground, _mask, _Fringe);
            */_map.Player = _center;
            
         
        }
        public void Draw(GameTime gameTime)
        {
           //_map.Draw(_spritebatch, gameTime);
            _map.DrawInWindow(_spritebatch, gameTime, _location);
            
        }
        
    }
}
