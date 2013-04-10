using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GUI.Controls;
using TileEngine;

namespace CapstoneProject
{
    class MoveButton : XButton
    {
        int _button;
       Keys[] _key;
        const int RIGHTBUTTON = 0;
        const int LEFTBUTTON = 1;
        const int UPBUTTON = 3;
        
        public MoveButton(Vector2 buttonPosition, Texture2D Texture, int button)
            : base(buttonPosition, Texture)
        {
            _button = button;
        }

        public override void FireEvent()
        {

            _key = Keyboard.GetState().GetPressedKeys();
            switch (_button)
            {
                case RIGHTBUTTON:
                    Controls.Right = _key[0];
                    break;
                case LEFTBUTTON:
                    Controls.Left = _key[0];
                    break;
                case UPBUTTON:
                    Controls.Up = _key[0];
                    break;
                default:
                    break;
            }
            base.FireEvent();
        }
    }
}
