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
    public class Controls
    {
        private Keys _left = Keys.Left;
        private Keys _right = Keys.Right;
        private Keys _up = Keys.Up;

        public Microsoft.Xna.Framework.Input.Keys Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public Microsoft.Xna.Framework.Input.Keys Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public Microsoft.Xna.Framework.Input.Keys Up
        {
            get { return _up; }
            set { _up = value; }
        }
    }
}