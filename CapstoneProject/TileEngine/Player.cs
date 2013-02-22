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
   public class Player : Avatar
    {
        // Animation representing the player
        public Animation PlayerAnimation;
        
        

        // State of the player
        public bool Active;

        public Player()
        {
            
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;

            // Set starting position of the player
            Position = position;

            // Set the player to be active
            Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 Position = Vector2.Zero;
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //going to need to check whether the 
            PlayerAnimation.Draw(spriteBatch);


            base.Draw(spriteBatch, gameTime);
        }

    }
}
