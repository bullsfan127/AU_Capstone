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
    class Player : Avatar
    {
        // Animation representing the player
        public Animation PlayerAnimation;
        
        // Position of the Player
        public Vector2 Position;

        // State of the player
        public bool Active;

        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;

            // Set starting position of the player
            Position = position;

            // Set the player to be active
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            PlayerAnimation.Draw(spriteBatch);
        }

    }
}
