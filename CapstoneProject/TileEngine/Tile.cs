//TODO:  Needs Auto documentation.

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
    /// Stores all information required to render a tile to the screen
    /// </summary>
    public class Tile : TileEngine.Interfaces.Drawable
    {
        //TODO maybe convert these to private
        private float _Scale;

        public float Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Texture2D _Texture;

        private Rectangle _SourceRectangle;

        public Rectangle SourceRectangle
        {
            get { return _SourceRectangle; }
            set { _SourceRectangle = value; }
        }

        private Color _ColorTint;

        public Color ColorTint
        {
            get { return _ColorTint; }
            set { _ColorTint = value; }
        }

        private float _Rotation;

        public float Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }

        private Vector2 _Origin;

        public Vector2 Origin
        {
            get { return _Origin; }
            set { _Origin = value; }
        }

        private SpriteEffects _Effect;

        public SpriteEffects Effect
        {
            get { return _Effect; }
            set { _Effect = value; }
        }

        private float _Depth;

        public float Depth
        {
            get { return _Depth; }
            set { _Depth = value; }
        }

        //TODO:  Add a constuction that creates a default tile based off a source
        //       Rectangle and a Texture.
        public Tile() { }

        public Tile(Rectangle _sourceRectangle, Color _tint, float _rotation, Vector2 _origin, SpriteEffects _effect, float _depth)
        {
            _SourceRectangle = _sourceRectangle;

            _ColorTint = _tint;
            _Rotation = _rotation;
            _Origin = _origin;
            _Effect = _effect;
            _Depth = _depth;

            _Scale = 0.0f;//Scale is a scale factor
            //so if it says two the tile will be twice as big.
        }

        public void setTexture(Texture2D _texture)
        {
            _Texture = _texture;
        }

        public Texture2D getTexture()
        {
            return _Texture;
        }

        public Rectangle getSourceRectangle()
        {
            return _SourceRectangle;
        }

        public void setSourceRectangle(Rectangle _sourceRectangle)
        {
            _SourceRectangle = _sourceRectangle;
        }

        public Color getTint()
        {
            return _ColorTint;
        }

        public void setTint(Color color)
        {
            _ColorTint = color;
        }

        public float getRotation()
        {
            return _Rotation;
        }

        public void setRotation(float _rotation)
        {
            _Rotation = _rotation;
        }

        public Vector2 getOrigin()
        {
            return _Origin;
        }

        public void setOrigin(Vector2 _origin)
        {
            _Origin = _origin;
        }

        public SpriteEffects getSpriteEffect()
        {
            return _Effect;
        }

        public void setSpriteEffect(SpriteEffects spriteEffect)
        {
            _Effect = spriteEffect;
        }

        public float getDepth()
        {
            return _Depth;
        }

        public void setDepth(float a)
        {
            _Depth = a;
        }

        public float getScale()
        {
            return _Scale;
        }

        //TODO:  Add a setScale that takes a target resolution and calculates the scale for that resolution and
        //       and stores it in scale.
        public void setScale(float _scale)
        {
            _Scale = _scale;
        }
    }
}