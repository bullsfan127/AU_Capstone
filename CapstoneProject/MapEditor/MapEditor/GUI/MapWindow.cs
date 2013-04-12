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
using GUI.Controls;
using GUI;

namespace MapEditor.GUI
{
   public class MapWindow : IDrawable, IUpdateable, GComponent
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

        TileSelector _selector;
        SpriteBatch _spritebatch;
        Map _map;
        //These will be removed soon
        DrawableLayer<Tile> _ground;
        DrawableLayer<Tile> _mask;
        DrawableLayer<Tile> _Fringe;
        
        Tile a;
        Tile b;
        Tile c;

        public Label layerLabel;
        Texture2D UnderPanel;
        DrawableLayer<Tile> currentLayer1;
        DrawableLayer<Tile> currentLayerA;
        DrawableLayer<Tile> currentLayerB;

        FocalPoint _center = new FocalPoint(new Vector2(0, 90), 10, 100, 50, 50);
       public enum currentLayer { GROUND, MASK, FRINGE };
        /// <summary>
        /// For determining what layer you are laying tiles in.
        /// </summary>
       public currentLayer _currentLayer = currentLayer.FRINGE;

       public MapWindow(Rectangle Location, SpriteBatch batch, SpriteFont Font, Texture2D Underpanel)
        {
            this.UnderPanel = Underpanel;
            _location = Location;
            _spritebatch = batch;
            layerLabel = new Label(Font, "", new Vector2(_location.X - 10, _location.Y - 10), Color.BlanchedAlmond, 1.0f);
        
        }
       public MapWindow(SpriteBatch batch, GraphicsDeviceManager graphics, ContentManager Content, SpriteFont Font, TileSelector selector, Texture2D Underpanel)
       {
           this.UnderPanel = Underpanel;
           _spritebatch = batch;
           _location = new Rectangle(100, 100, 500, 500);
           currentLayer1 = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
           currentLayerA = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
           currentLayerB = new DrawableLayer<Tile>(new Vector2(100, 100), graphics.GraphicsDevice);
           a = new Tile(new Rectangle(0, 0, 64, 64), Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
           b = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
           c = new Tile(new Rectangle(0, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
           _selector = selector;
           for (int x = 0; x < 100; x++)
           {
               for (int y = 0; y < 100; y++)
               {
                   currentLayer1.setItemAt(new Vector2(x, y), a);
                   if (x % 3 == 0)
                       currentLayerA.setItemAt(new Vector2(x, y), b);

                   if (y % 3 == 0)
                       currentLayerB.setItemAt(new Vector2(x, y), c);
               }
           }
           a.setTexture(Content.Load<Texture2D>("Tiles//FinalGrid"));
           a.Name = "Tiles//tile";
           b.setTexture(Content.Load<Texture2D>("Tiles//tileM"));
           b.Name = "Tiles//tileM";
           c.setTexture(Content.Load<Texture2D>("Tiles//tileF"));
           c.Name = "Tiles//tileF";
           _map = new Map();
           _map.SwapMaskLayer(currentLayerA);
           _map.SwapGoundLayer(currentLayer1);
           _map.SwapFringeLayer(currentLayerB);
           _map.SetCollisionLayer(currentLayer1);
           layerLabel = new Label(Font, "", new Vector2(_location.X - 10, _location.Y - 10), Color.DarkGreen, 1.0f);
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
            _map.Update(gameTime, Vector2.Zero);
            layerLabel.Update(gameTime);
            switch (_currentLayer)
            { 
                case(currentLayer.GROUND):
                    layerLabel.Text = "ground";
                    break;
                case (currentLayer.MASK):
                    layerLabel.Text = "MASK";
                    break;
                case (currentLayer.FRINGE):
                    layerLabel.Text = "FRINGE";
                    break;
            
            }
            MouseState mouse = Mouse.GetState();
            Rectangle mouseColision = new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1);
            //Now we check if there is a collision
            if (mouseColision.Intersects(_location))
            {
                //If the right button is pressed and the button wasn't clicked recently
                if ((mouse.LeftButton == ButtonState.Pressed && _selector.IsTileSelected ) /*&& !WasClicked*/)
                {
                    ///Mouse location on texture
                    int mouseX = (mouse.X - _location.X) / 48;
                    int mouseY = (mouse.Y - _location.Y) / 48;

                    switch (_currentLayer)
                    {
                        case (currentLayer.GROUND):
                            if (_selector.IsTileSelected)
                            {
                                _map.Ground.setItemAt(new Vector2(mouseX, mouseY), _selector.SelectedTile, _map.Player);
                                _map.CollisionLayer.setItemAt(new Vector2(mouseX, mouseY), _selector.SelectedTile.SourceRectangle, _map.Player);
                            }
                             break;
                        case (currentLayer.MASK):
                            layerLabel.Text = "MASK";
                            if (_selector.IsTileSelected)
                                _map.Mask.setItemAt(new Vector2(mouseX, mouseY), _selector.SelectedTile, _map.Player);
                            break;
                        case (currentLayer.FRINGE):
                            layerLabel.Text = "FRINGE";
                            if (_selector.IsTileSelected)
                                _map.Fringe.setItemAt(new Vector2(mouseX, mouseY), _selector.SelectedTile, _map.Player);
                            break;

                    }//endswitch

                }//end leftbutton if

                //If the right button is pressed and the button wasn't clicked recently
                if ((mouse.RightButton == ButtonState.Pressed) /*&& !WasClicked*/)
                {
                    ///Mouse location on texture
                    int mouseX = (mouse.X - _location.X) / 48;
                    int mouseY = (mouse.Y - _location.Y) / 48;

                    switch (_currentLayer)
                    {
                        case (currentLayer.GROUND):
                            {
                                _map.Ground.setItemAt(new Vector2(mouseX, mouseY),null, _map.Player);
                                _map.CollisionLayer.setItemAt(new Vector2(mouseX, mouseY), Rectangle.Empty, _map.Player);
                            }
                            break;
                        case (currentLayer.MASK):
                            layerLabel.Text = "MASK";
                            if (_selector.IsTileSelected)
                                _map.Mask.setItemAt(new Vector2(mouseX, mouseY), null, _map.Player);
                            break;
                        case (currentLayer.FRINGE):
                            layerLabel.Text = "FRINGE";
                            if (_selector.IsTileSelected)
                                _map.Fringe.setItemAt(new Vector2(mouseX, mouseY), null, _map.Player);
                            break;

                    }//endswitch

                }//end rightbutton if

            }


            //If the button was clicked

           
            
            /*
            _map = new Map(_ground, _mask, _Fringe);
            */_map.Player = _center;
            
         
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(UnderPanel, new Rectangle(_location.X - 20, _location.Y - 20, 540, 540), Color.LawnGreen);
            spriteBatch.End();
            Draw(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
           //_map.Draw(_spritebatch, gameTime);
            
            _map.DrawInWindow(_spritebatch, gameTime, _location);
            layerLabel.Draw(gameTime, _spritebatch);
            
        }


        public Vector2 Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                _location.X = (int)value.X;
                _location.Y = (int)value.Y;
            }
        }
   }
}
