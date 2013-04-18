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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainSaveKeys : XButton
    {
      
        int _button;
        const int MSAVEBUTTON = 0;

        public MainSaveKeys(Vector2 buttonPosition, Texture2D Texture, int button)
            : base(buttonPosition, Texture)
        {
            _button = button;
        }

        public override void FireEvent()
        {
            if (MSAVEBUTTON == 0)
            {
                Controls.Save();
                CapstoneProject.Game1.gameState = CapstoneProject.GAMESTATE.MAINMENU;
            }
            base.FireEvent();
        }
    }
}