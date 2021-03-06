﻿using System;
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
    public static class Controls
    {
        private static Keys _left = Keys.Left;
        private static Keys _right = Keys.Right;
        private static Keys _up = Keys.Up;
        private static Keys _attack = Keys.Space;

        public static Keys Attack
        {
            get { return Controls._attack; }
            set { Controls._attack = value; }
        }

        public static Microsoft.Xna.Framework.Input.Keys Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public static Microsoft.Xna.Framework.Input.Keys Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public static Microsoft.Xna.Framework.Input.Keys Up
        {
            get { return _up; }
            set { _up = value; }
        }

        public static void Save()
        {
            SerializeControls serializer = new SerializeControls();
            serializer.save();
        }

        public static void Load()
        {
            SerializeControls serializer = new SerializeControls();
            serializer.Load();
        }

        public static void setDefault()
        {
            _left = Keys.Left;
            _right = Keys.Right;
            _up = Keys.Up;
        }
    }
}