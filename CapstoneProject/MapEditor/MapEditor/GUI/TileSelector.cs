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
   public class TileSelector : IUpdateable, IDrawable
   {
       #region XNAImplement
       public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

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

        

        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> UpdateOrderChanged;
#endregion

        public TileSheet _tileSheet;
        public Texture2D _backGround;
       public Rectangle _renderTarget;
       public SpriteBatch _spritebatch;
       public Rectangle sourceRectangle;
       private bool _isTileSelected = false;
        
       public bool IsTileSelected
       {
           get { return _isTileSelected; }
           set { _isTileSelected = value; }
       }

       public TileSelector(SpriteBatch batch, TileSheet intialTilesheet, GraphicsDevice graphicsDevice)
       {
           _spritebatch = batch;
          _tileSheet = intialTilesheet;
          _backGround = new Texture2D(graphicsDevice, 320, 320, false, SurfaceFormat.Color);
       
       }
       

       


        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            _spritebatch.Begin();
            _spritebatch.Draw(_backGround, _renderTarget, Color.Gray);
            _spritebatch.Draw(_tileSheet.tileSheet, _renderTarget, Color.White);

            _spritebatch.End();
        }
   
   
   }
}
