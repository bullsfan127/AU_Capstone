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
    public class Tile : TileEngine.Interfaces.Drawable
    {
        protected float scale;
        protected Texture2D texture;
      
        protected Rectangle sourceRectangle;
        protected Color tint;
        protected float rotation;
        protected Vector2 origin;
        protected SpriteEffects effect;
        protected float depth;

        public Tile() { }

        public Tile(Rectangle _sourceRectangle, Color _tint, float _rotation, Vector2 _origin, SpriteEffects _effect, float _depth)
        {
           
            sourceRectangle = _sourceRectangle;
            tint = _tint;
            rotation = _rotation;
            origin = _origin;
            effect = _effect;
            depth = _depth;


        }
        
        
        public void setTexture(Texture2D _texture)
        {
            texture = _texture;
        }

        public Texture2D getTexture()
        {
            return texture;
        }


        public Rectangle getSourceRectangle()
        {
            return sourceRectangle;
        }

        public void setSourceRectangle(Rectangle _sourceRectangle)
        {
            sourceRectangle = _sourceRectangle;
        }

        public Color getTint()
        {
            return tint;
        }

        public void setTint(Color color)
        {
            tint = color;
        }

        public float getRotation()
        {
            return rotation;
        }

        public void setRotation(float _rotation)
        {
            rotation = _rotation;
        }

        public Vector2 getOrigin()
        {
            return origin;
        }

        public void setOrigin(Vector2 _origin)
        {
            origin = _origin;
        }

        public SpriteEffects getSpriteEffect()
        {
            return effect;
        }

        public void setSpriteEffect(SpriteEffects spriteEffect)
        {
            effect = spriteEffect;
        }

        public float getDepth()
        {
            return depth;
        }

        public void setDepth(float a)
        {
            depth = a;
        }

        public float getScale()
        {
            return scale;
        }

        public void setScale(float _scale)
        {
            scale = _scale;
        }
    }
}
