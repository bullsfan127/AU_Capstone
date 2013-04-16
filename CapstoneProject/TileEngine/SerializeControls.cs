using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TileEngine;

namespace TileEngine
{
    public class SerializeControls
    {
        private Keys _right = Controls.Right;
        private Keys _left = Controls.Left;
        private Keys _up = Controls.Up;
        CustomSerialization.Serialize<SerializeControls> serializer = new CustomSerialization.Serialize<SerializeControls>();

        public Microsoft.Xna.Framework.Input.Keys Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public Microsoft.Xna.Framework.Input.Keys Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public Microsoft.Xna.Framework.Input.Keys Up
        {
            get { return _up; }
            set { _up = value; }
        }

        public void save()
        {
            serializer.FileName = "controls.xml";
            serializer.Save(this, true);
        }

        public void Load()
        {
            SerializeControls temp = new SerializeControls();
            temp = serializer.Load("controls.xml");
            Controls.Right = temp._right;
            Controls.Left = temp._left;
            Controls.Up = temp._up;
        }
    }
}