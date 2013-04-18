using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI.Controls;
using GUI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

namespace CapstoneProject
{
    internal class MoveButton : XButton
    {
        int _button;
        Keys[] _key;
        const int RIGHTBUTTON = 0;
        const int LEFTBUTTON = 1;
        const int UPBUTTON = 2;
        const int ATTACKBUTTON = 3;
        
       

        public MoveButton(Vector2 buttonPosition, Texture2D Texture, int button)
            : base(buttonPosition, Texture)
        {
            _button = button;
        }

        public override void FireEvent()
        {
            _key = Keyboard.GetState().GetPressedKeys();
          
            if (_key.Length == 0)
            {
                FireAgain = true;
            }
            else
            {
                FireAgain = false;

                switch (_button)
                {
                    case RIGHTBUTTON:
                        Controls.Right = _key[0];
                        Color = Color.White;
                        break;
                    case LEFTBUTTON:
                        Controls.Left = _key[0];
                        Color = Color.White;
                        break;
                    case UPBUTTON:
                        Controls.Up = _key[0];
                        Color = Color.White;
                        break;
                    case ATTACKBUTTON:
                        Controls.Attack = _key[0];
                        Color = Color.White;
                        break;
                    default:
                        break;
                }
            }
            base.FireEvent();
        }
    }
}