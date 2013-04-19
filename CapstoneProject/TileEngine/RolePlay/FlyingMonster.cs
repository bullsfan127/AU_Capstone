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
    public class FlyingMonster : Monster
    {
        Vector2 relPosition = new Vector2(0, 0);
        Vector2 movement = new Vector2(0, 0);
        bool active = false;

        /// <summary>
        /// GroundMonster class - The monster that cannot fly
        /// </summary>
        public FlyingMonster()
        {
            SpriteWidth = 64;
            SpriteHeight = 64;
            SpriteFrame = 2;

            MaxDamage = 1;
            MaxHealth = 1;
        }

        public void Update(GameTime gameTime, Vector2 player, Vector2 offset)
        {
            relPosition = Position - offset;
            if (Position.X - offset.X < 600 && Position.X - offset.X > -70 && Position.Y - offset.Y < 600 && Position.Y - offset.Y > 0)
            {
                active = true;
            }
            else
                active = false;
            if (active)
            {
                _monsterAnimation.Position = relPosition;
                _monsterAnimation.Update(gameTime);
                if (relPosition.X - player.X < 0)
                {
                    movement.X = 4;
                }
                else
                    movement.X = -4;
                if (relPosition.Y - player.Y < 0)
                {
                    movement.Y = 2;
                }
                else
                    movement.Y = -2;

                Position += movement;
            }
            base.Update(gameTime);
        }
    }
}