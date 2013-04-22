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
        public const int TILE_WIDTH = 64;

        //offset to determine center of screen.
        public Vector2 offset = Vector2.Zero;

        CustomSerialization.Serialize<Map> serializer = new CustomSerialization.Serialize<Map>();

        DrawableLayer<Tile> _Ground;
        List<Item> _mapItems = new List<Item>();
        List<Avatar> _NpcList = new List<Avatar>();
        DrawableLayer<Tile> _Mask;

        //Layers for collision detection
        Layer<Rectangle> _CollisionLayer;

        DrawableLayer<Tile> _Fringe;

        //Base Type for Player
        Avatar _Player;

        int _MapID;

        #region encapsulatedFields

        /// <summary>
        /// MapID FIELD
        /// </summary>
        public int MapID
        {
            get { return _MapID; }
            set { _MapID = value; }
        }

        /// <summary>
        /// Tracking for NPC
        /// </summary>
        public List<Avatar> NpcList
        {
            get { return _NpcList; }
            set { _NpcList = value; }
        }

        /// <summary>
        /// Tracking for MapItems
        /// </summary>
        public List<Item> MapItems
        {
            get { return _mapItems; }
            set { _mapItems = value; }
        }

        /// <summary>
        /// The Ground Layer
        /// </summary>
        public DrawableLayer<Tile> Ground
        {
            get { return _Ground; }
            set { _Ground = value; }
        }

        /// <summary>
        /// The Mask Layer
        /// </summary>
        public DrawableLayer<Tile> Mask
        {
            get { return _Mask; }
            set { _Mask = value; }
        }

        /// <summary>
        /// The Fringe Layer
        /// </summary>
        public DrawableLayer<Tile> Fringe
        {
            get { return _Fringe; }
            set { _Fringe = value; }
        }

        /// <summary>
        /// The CollisionLayer
        /// </summary>
        public Layer<Rectangle> CollisionLayer
        {
            get { return _CollisionLayer; }
            set { _CollisionLayer = value; }
        }

        /// <summary>
        /// The Player
        /// </summary>
        public Avatar Player
        {
            get { return _Player; }
            set { _Player = value; }
        }

        #endregion encapsulatedFields

        /// <summary>
        /// Default Doesn't do anything.
        /// </summary>
        public Map() { }

        public Map(DrawableLayer<Tile> Ground, DrawableLayer<Tile> Mask, DrawableLayer<Tile> Fringe)
        {
            _Ground = Ground;
            _Mask = Mask;
            _Fringe = Fringe;
        }

        /// <summary>
        /// Sets the CollisionLayer size according to a layer.
        /// </summary>
        /// <param name="currentLayer1">the layer whose side you want to replicate</param>
        public void SetCollisionLayer(DrawableLayer<Tile> currentLayer1)
        {
            _CollisionLayer = new Layer<Rectangle>(new Vector2(currentLayer1.MapWidth, currentLayer1.MapHeight));
        }

        //#UPDATE
        /// <summary>
        /// Updates all map specific components
        /// </summary>
        /// <param name="gameTime">The gameTime snapshot</param>
        //@Offset Why do you need this here?  Just add it to the avatar class this is linked to the player class
        public void Update(GameTime gameTime, Vector2 offset)
        {
            this.offset = offset;
            if (this.offset.X < 0)
            {
                this.offset.X = 0;
            }

            foreach (Item item in _mapItems)
            {
                item.Update(gameTime, this.Player.Position, offset);
            }

            foreach (Monster a in _NpcList)
            { a.Update(gameTime, this.Player.Position, offset); }
        }

        //#DRAW
        /// <summary>
        /// Draw the map in the entire screen
        /// </summary>
        /// <param name="spriteBatch">spritebatch</param>
        /// <param name="gameTime">gametime</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _Ground.Draw(spriteBatch, gameTime, offset);
            _Mask.Draw(spriteBatch, gameTime, offset);

            foreach (Item item in _mapItems)
            {
                //TODO:  Add Logic for drawing based on player Proximity
                item.Draw(spriteBatch, gameTime);
            }

            foreach (Monster ava in _NpcList)
            {
                //TODO:  Add Logic for drawing based on player Proximity
                ava.Draw(spriteBatch, gameTime);
            }
            _Player.Draw(spriteBatch, gameTime);
            _Fringe.Draw(spriteBatch, gameTime, offset);
        }

        /// <summary>
        /// Draws the map in a specific region
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        /// <param name="gameTime">Gametime</param>
        /// <param name="Location">Rectancle stating the region</param>

        public void DrawInWindow(SpriteBatch spriteBatch, GameTime gameTime, Rectangle Location)
        {
            _Ground.Draw(spriteBatch, gameTime, offset, Location, _Player);
            _Mask.Draw(spriteBatch, gameTime, offset, Location, _Player);
            _Player.Draw(spriteBatch, gameTime);
            _Fringe.Draw(spriteBatch, gameTime, offset, Location, _Player);
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

        /// <summary>
        /// Save map to disk
        /// </summary>
        /// <param name="filename">The name you want to give the xml file</param>
        public void saveMap(string filename = "SaveGame.xml", bool path = true)
        {
            serializer.FileName = filename;
            serializer.Save(this, true, path);
        }

        /// <summary>
        /// Loads map from disk
        /// </summary>
        /// <param name="filename">Name of map being loaded</param>
        /// <param name="contentManager">The contentManager to load textures</param>
        /// <returns></returns>
        public Map LoadMap(string filename, ContentManager contentManager, bool path = true)
        {
            int HW = 600;
            int RC = 10;
            int MWH = 100;
            int s = 0;
            Map newMap = new Map();
            newMap = serializer.Load(filename, path);
            newMap.LoadExtraContent(contentManager);

            newMap.Fringe.MaxViewPortHeight = HW;
            newMap.Fringe.MaxViewPortWidth = HW;
            newMap.Fringe.MaxRows = RC;
            newMap.Fringe.MaxColumns = RC;
            newMap.Fringe.MapWidth = MWH;
            newMap.Fringe.MapHeight = MWH;
            newMap.Fringe.Scale = s;

            newMap.Mask.MaxViewPortHeight = HW;
            newMap.Mask.MaxViewPortWidth = HW;
            newMap.Mask.MaxRows = RC;
            newMap.Mask.MaxColumns = RC;
            newMap.Mask.MapWidth = MWH;
            newMap.Mask.MapHeight = MWH;
            newMap.Mask.Scale = s;

            newMap.Ground.MaxViewPortHeight = HW;
            newMap.Ground.MaxViewPortWidth = HW;
            newMap.Ground.MaxRows = RC;
            newMap.Ground.MaxColumns = RC;
            newMap.Ground.MapWidth = MWH;
            newMap.Ground.MapHeight = MWH;
            newMap.Ground.Scale = s;

            return newMap;
        }

        /// <summary>
        /// Loads extra content, that Serialization doesn't pick up
        /// </summary>
        /// <param name="contentManager">The content Manager to load textures</param>
        public void LoadExtraContent(ContentManager contentManager)
        {
            List<Texture2D> tiles = new List<Texture2D>();
            Texture2D t = contentManager.Load<Texture2D>("Tiles//tile");
            t.Name = "Tiles//tile";
            Texture2D m = contentManager.Load<Texture2D>("Tiles//tileM");
            m.Name = "Tiles//tileM";
            Texture2D f = contentManager.Load<Texture2D>("Tiles//tileF");
            f.Name = "Tiles//tileF";
            Texture2D fg = contentManager.Load<Texture2D>("Tiles//FinalGrid");
            fg.Name = "Tiles//FinalGrid";

            tiles.Add(t);
            tiles.Add(m);
            tiles.Add(f);
            tiles.Add(fg);

            //Load layers and tiles
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if (this._Fringe.Layer[x, y] != null)
                    {
                        foreach (Texture2D tile in tiles)
                            if (this._Fringe.Layer[x, y].Name == tile.ToString())
                            {
                                this._Fringe.Layer[x, y].setTexture(tile);
                                this._Fringe.Layer[x, y].setSourceRectangle(new Rectangle(this._Fringe.Layer[x, y].SR[0], this._Fringe.Layer[x, y].SR[1], 64, 64));
                                this._Fringe.Layer[x, y].Scale = 320;
                            }
                    }

                    if (this._Ground.Layer[x, y] != null)
                    {
                        foreach (Texture2D tile in tiles)
                            if (this._Ground.Layer[x, y].Name == tile.ToString())
                            {
                                this._Ground.Layer[x, y].setTexture(tile);
                                this._Ground.Layer[x, y].setSourceRectangle(new Rectangle(this._Ground.Layer[x, y].SR[0], this._Ground.Layer[x, y].SR[1], 64, 64));
                                this.Ground.Layer[x, y].Scale = 320;
                            }
                    }

                    if (this._Mask.Layer[x, y] != null)
                    {
                        foreach (Texture2D tile in tiles)
                            if (this._Mask.Layer[x, y].Name == tile.ToString())
                                this._Mask.Layer[x, y].setTexture(tile);
                        this._Mask.Layer[x, y].setSourceRectangle(new Rectangle(this._Mask.Layer[x, y].SR[0], this._Mask.Layer[x, y].SR[1], 64, 64));
                        this.Mask.Layer[x, y].Scale = 320;
                    }
                }
            }
        }

        /// <summary>
        /// Reconstructs player after reconstruction, should only be called when loading a saved game
        /// </summary>
        /// <param name="contentManager"></param>
        public void LoadPlayer(ContentManager contentManager)
        {
            //convert to player instead of Avatar
            Player temp = new Player(contentManager);
            temp = (Player)this.Player;

            //load player
            Texture2D playerTexture = contentManager.Load<Texture2D>("shitty/FatJoe");
            Vector2 playerPosition = new Vector2(this._Player.X, this._Player.Y);
            temp.Position = playerPosition;

            //load playerAnimation
            Rectangle srect = new Rectangle();
            srect = new Rectangle(this._Player.PlayerAnimation.SRect[0], this._Player.PlayerAnimation.SRect[1], this._Player.PlayerAnimation.SRect[2], this._Player.PlayerAnimation.SRect[3]);
            temp.PlayerAnimation.sourceRect = srect;
            temp.PlayerAnimation.spriteStrip = playerTexture;
            temp.PlayerAnimation.destinationRect = new Rectangle(this._Player.PlayerAnimation.DRect[0], this._Player.PlayerAnimation.DRect[1], this._Player.PlayerAnimation.DRect[2], this._Player.PlayerAnimation.DRect[3]);
            temp.PlayerAnimation.Position = this._Player.Position;
            temp.Offset = new Vector2(this._Player.OX, this.Player.OY);

            //load weapons
            temp._rangedTexture = contentManager.Load<Texture2D>("Items//sword");
            temp._swordTexture = contentManager.Load<Texture2D>("Items//boomerang");
            temp.Weapon.Initialize(temp._rangedTexture, temp.Position);
            this.Player = temp;

            List<Texture2D> monsters = new List<Texture2D>();
            Texture2D z = contentManager.Load<Texture2D>("Monsters/Zombies");
            z.Name = "Monsters/Zombies";
            Texture2D b = contentManager.Load<Texture2D>("Monsters/Birds");
            b.Name = "Monsters/Birds";
            Texture2D spriteStrip;
            //load monsters
            foreach (Monster a in _NpcList)
            {
                Type type = a.GetType();
                spriteStrip = type == typeof(GroundMonster) ? z : b;
                a.Position = new Vector2(a.X, a.Y);
                a.Initialize(spriteStrip, a.Position);
            }

            //load items
            List<Texture2D> items = new List<Texture2D>();
            Texture2D c = contentManager.Load<Texture2D>("items/Coin");
            c.Name = "items/Coin";
            Texture2D p = contentManager.Load<Texture2D>("items/Potion");
            p.Name = "items/Potion";
            Texture2D spriteStrip2;

            foreach (Item a in _mapItems)
            {
                Type type = a.GetType();
                spriteStrip2 = type == typeof(Coin) ? c : p;
                a.Initialize(spriteStrip2, new Vector2(a.X, a.Y));
            }
        }
    }
}