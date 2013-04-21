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
    public class GroundMonster : Monster
    {
        //position relative to player
        Vector2 relPosition = new Vector2(0, 0);

        //movement of monster
        Vector2 movement = new Vector2(0, 0);

        //whether monster is on screen;
        bool active = false;

        /// <summary>
        /// GroundMonster class - The monster that cannot fly
        /// </summary>
        public GroundMonster()
        {
            SpriteWidth = 64;
            SpriteHeight = 128;
            SpriteFrame = 2;

            MaxDamage = 1;
            MaxHealth = 1;
        }

        public override void Update(GameTime gameTime, Vector2 player, Vector2 offset)
        {
            //is monster on screen check
            if (Position.X - offset.X < 600 && Position.X - offset.X > -70 && Position.Y - offset.Y < 600 && Position.Y - offset.Y > 0)
            {
                active = true;
            }
            else
                active = false;
            //if monster is on screen
            if (active)
            {
                //define relative position
                relPosition = Position - offset;
                //define animation space
                _monsterAnimation.Position = relPosition;
                _monsterAnimation.Update(gameTime);
                //if monster is to the left of player
                if (relPosition.X - player.X < 0)
                {
                    movement.X = 2;
                }
                //if monster is to the right of player
                else
                    movement.X = -2;
                //gravity
                movement.Y += 1;
                //floor
                if (relPosition.Y >= 372)
                {
                    movement.Y = 0;
                    relPosition.Y = 372;
                }


                if (movement.X < 0)
                {
                    _monsterAnimation.state = Animation.Animate.ZLEFT;
                }
                else
                {
                    _monsterAnimation.state = Animation.Animate.ZRIGHT;
                }

                //actually move
                Position += movement;
            }

            X = Position.X;
            Y = Position.Y;
            base.Update(gameTime);
        }
    }
}