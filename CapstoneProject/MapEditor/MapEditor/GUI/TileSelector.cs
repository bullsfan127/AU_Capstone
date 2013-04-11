using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI;
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
    public class TileSelector : IUpdateable, IDrawable, GComponent
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

        #endregion XNAImplement

        public TileSheet _tileSheet;
        public Texture2D _backGround;
        public Rectangle _renderTarget;
        public SpriteBatch _spritebatch;
        public Rectangle sourceRectangle;

        public Rectangle _LayOver;
        public Texture2D _LayOverText;

        private Tile _selectedTile;

        public Tile SelectedTile
        {
            get { return _selectedTile; }
            set { _selectedTile = value; }
        }

        private bool _isTileSelected = false;

        public bool IsTileSelected
        {
            get { return _isTileSelected; }
            set { _isTileSelected = value; }
        }

        public TileSelector(SpriteBatch batch, TileSheet intialTilesheet, GraphicsDevice graphicsDevice, Texture2D Layover)
        {
            _spritebatch = batch;
            _tileSheet = intialTilesheet;
            _LayOverText = Layover;
            //_backGround = new Texture2D(graphicsDevice, 320, 320, false, SurfaceFormat.Color);
            //Color[] a = new Color[320 * 320];
            //for (int x = 0; x < 320; x++)
            //{
            //    for (int y = 0; y < 320; y++)
            //    {
            //        a[x * y] = Color.Gray;
            //    }
            //}
            //_backGround.SetData<Color>(a);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(_renderTarget))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    IsTileSelected = true;
                    _selectedTile = _tileSheet.getTileAt(new Vector2(mouse.X - _renderTarget.X, mouse.Y - _renderTarget.Y));
                    _LayOver = _selectedTile.SourceRectangle;
                }
                else if (mouse.RightButton == ButtonState.Pressed)
                    _isTileSelected = false;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spritebatch.Begin();
            /// _spritebatch.Draw(_backGround, _renderTarget, Color.Gray);
            _spritebatch.Draw(_tileSheet.tileSheet, _renderTarget, Color.White);

            if (_isTileSelected)
            {
                int tilesWide = (_tileSheet.tileSheet.Width - 80) / _tileSheet.TileSize;

                Rectangle destinationRect = new Rectangle((_renderTarget.X + _LayOver.X), (_renderTarget.Y + _LayOver.Y), _renderTarget.Width / tilesWide, _renderTarget.Height / tilesWide);

                _spritebatch.Draw(_LayOverText, destinationRect, Color.Orange);
            }
            _spritebatch.End();
        }
    }
}