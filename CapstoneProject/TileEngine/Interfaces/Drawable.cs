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

namespace TileEngine.Interfaces
{
    public interface Drawable
    {

        float getScale();
        void setScale(float scale);


        void setTexture(Texture2D text);
        Texture2D getTexture();

        Rectangle getSourceRectangle();
        void setSourceRectangle(Rectangle sourceRectangle);

        Color getTint();
        void setTint(Color color);

        float getRotation();
        void setRotation(float rotation);

        Vector2 getOrigin();
        void setOrigin(Vector2 origin);

        SpriteEffects getSpriteEffect();
        void setSpriteEffect(SpriteEffects effect);

        float getDepth();
        void setDepth(float a);





    }
}
