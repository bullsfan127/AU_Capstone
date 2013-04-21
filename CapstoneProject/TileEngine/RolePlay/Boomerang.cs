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
    public class Boomerang : Weapon
    {
        Animation _boomerangAnimation;
        Vector2 position;
        Vector2 movemnet = new Vector2(0, 0);
        bool active;

        /// <summary>
        /// Boomerang class - Sets damage amount
        /// </summary>
        public Boomerang()
        {
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            MaxDamage = 1;
            Scale = 1.0f;
        }

        public override void Initialize(Texture2D spriteStrip, Vector2 position)
        {
            _boomerangAnimation = new Animation();

            // Set starting position of the monster
            this.position = position;
            //set starting speed of boomerang
            movemnet.X = 20;
            // TODO: Need to set correct image/location
            _boomerangAnimation.Initialize(spriteStrip, position, SpriteWidth, SpriteHeight, SpriteFrame, 250, Color.White, 1.0f, true);

            // Set the monster to be active
            _boomerangAnimation.Active = true;

            active = true;
        }

        public void Update(Vector2 player)
        {
            if (active)
            {
                if (position.X - player.X < 0)
                {
                    movemnet.X++;
                }
                else
                    movemnet.X--;
                if (position.Y - player.Y < 0)
                {
                    movemnet.Y++;
                }
                else
                    movemnet.Y--;
                position += movemnet;
                if (position == player)
                {
                    active = false;
                }
            }
        }
    }
}