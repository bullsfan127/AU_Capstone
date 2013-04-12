///USAGE:  Just inherit this class and override the FireEvent Method.
///
///
///
///
///
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

namespace GUI.Controls
{
   public class XButton : IXButton
    {
        private int lastclicked = 0;
        public const int COOLDOWN = 300;

        private Texture2D _ButtonImage;
        public Texture2D ButtonImage
        {
            get
            {
              return  _ButtonImage;
            }
            set
            {
                _ButtonImage = value;
            }
        }

        private Rectangle _ButtonRep;
        public Rectangle ButtonRep
        {
            get
            {
                return _ButtonRep;
            }
            set
            {
                _ButtonRep = value;
            }
        }

        private bool _Clicked;
        public bool Clicked
        {
            get
            {
                return _Clicked;
            }
            set
            {
                _Clicked = value;
            }
        }

        private bool _WasClicked = false;
        public bool WasClicked
        {
            get
            {
                return _WasClicked;
            }
            set
            {
                _WasClicked = value;
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }

        private Vector2 _Position;
        public Vector2 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _ButtonRep.X = (int)value.X;
                _ButtonRep.Y = (int)value.Y;
                _Position = value;
            }
        }

        public XButton(Vector2 buttonPosition, Texture2D Texture)
        {
            Position = buttonPosition;
            ButtonImage = Texture;
            _ButtonRep = new Rectangle((int)Position.X,(int) Position.Y, (int)(ButtonImage.Width), (int)(ButtonImage.Height));
        }
        /// <summary>
        /// Update method does all of the detection handling for the button.  Whether it was clicked or if its on cooldown.
        /// It will remain the same accross all buttons the only method to be changed is the FireEvent Method.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //Get the mouse state 
            MouseState mouse = Mouse.GetState();
            //Get the mouse position and convert it into a rectangle
            Rectangle mouseColision = new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1);

            //Cooldown
            if (gameTime.TotalGameTime.Milliseconds - lastclicked > COOLDOWN)
                _WasClicked = false;

            //Now we check if there is a collision
            if (mouseColision.Intersects(_ButtonRep))
            { 
                //If the right button is pressed and the button wasn't clicked recently
                if ((mouse.LeftButton == ButtonState.Pressed) /*&& !WasClicked*/)
                    Clicked = true;
            }

            //If the button was clicked
           
               if ((Clicked)&& (mouse.LeftButton != ButtonState.Pressed))
               {
                lastclicked = gameTime.TotalGameTime.Milliseconds;
                WasClicked = true;
                FireEvent();
                Clicked = false;
               }

        }
       /// <summary>
       /// Draw Method just draws the button into the target area.
       /// </summary>
       /// <param name="gameTime"></param>
       /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ButtonImage, ButtonRep, Color.White);
            spriteBatch.End();
        }
       /// <summary>
       /// Fire event is what the button actually does.
       /// </summary>
        public virtual void FireEvent()
        {
           
        }

        
    }
}
