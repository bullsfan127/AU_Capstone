﻿using System;
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
    public class Animation
    {
        // Enum for Animation states
        public enum Animate { IDLE, RMOVING, LMOVING };

        // Hold the current state of the Animation
        public Animate state;

        //Hold values to recreate Source Rectangle
        private int[] _sRect = new int[4];

        public int[] SRect
        {
            get { return _sRect; }
            set { _sRect = value; }
        }

        //Hold values to recreate Destination Rectangle
        private int[] _dRect = new int[4];

        public int[] DRect
        {
            get { return _dRect; }
            set { _dRect = value; }
        }

        // Holds the last state of the animation
        private Animate _lastState;

        // The image representing the collection of images used for animation
        public Texture2D spriteStrip;

        // The scale used to display the sprite strip
        private float _scale;

        // The time since we last updated the frame
        private int _elapsedTime;

        // The time we display a frame until the next one
        private int _frameTime;

        // The number of frames that the animation contains
        private int _frameCount;

        // The index of the current frame we are displaying
        private int _currentFrame;

        // The color of the frame we will be displaying
        private Color _color;

        // The area of the image strip we want to display
        private Rectangle _sourceRect = new Rectangle();

        // The area where we want to display the image strip in the game
        private Rectangle _destinationRect = new Rectangle();

        public Microsoft.Xna.Framework.Rectangle sourceRect
        {
            get { return _sourceRect; }
            set { _sourceRect = value; }
        }

        public Microsoft.Xna.Framework.Rectangle destinationRect
        {
            get { return _destinationRect; }
            set { _destinationRect = value; }
        }

        // Width of a given frame
        private int _FrameWidth;

        // Height of a given frame
        private int _FrameHeight;

        // The state of the Animation
        private bool _Active;

        // Determines if the animation will keep playing or deactivate after one run
        private bool _Looping;

        // Width of a given frame
        private Vector2 _Position;

        public TileEngine.Animation.Animate lastState
        {
            get { return _lastState; }
            set { _lastState = value; }
        }

        public float scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public int elapsedTime
        {
            get { return _elapsedTime; }
            set { _elapsedTime = value; }
        }

        public int frameTime
        {
            get { return _frameTime; }
            set { _frameTime = value; }
        }

        public int frameCount
        {
            get { return _frameCount; }
            set { _frameCount = value; }
        }

        public int currentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = value; }
        }

        public Microsoft.Xna.Framework.Color color
        {
            get { return _color; }
            set { _color = value; }
        }

        public int FrameWidth
        {
            get { return _FrameWidth; }
            set { _FrameWidth = value; }
        }

        public int FrameHeight
        {
            get { return _FrameHeight; }
            set { _FrameHeight = value; }
        }

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public bool Looping
        {
            get { return _Looping; }
            set { _Looping = value; }
        }

        public Microsoft.Xna.Framework.Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        /// <summary>
        /// Initializes the Animation code
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="frameCount"></param>
        /// <param name="frametime"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="looping"></param>
        /// <param name="animation"></param>
        public void Initialize(Texture2D texture, Vector2 position,
        int frameWidth, int frameHeight, int frameCount,
        int frametime, Color color, float scale, bool looping, Animate animation = Animate.IDLE)
        {
            // Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            // Set the time to zero
            elapsedTime = 0;

            switch (animation)
            {
                case Animate.IDLE:
                    currentFrame = 0;
                    this.state = Animate.IDLE;
                    break;
                case Animate.RMOVING:
                    currentFrame = 1;
                    this.state = Animate.RMOVING;
                    break;
                case Animate.LMOVING:
                    currentFrame = 3;
                    this.state = Animate.LMOVING;
                    break;
                default:
                    currentFrame = 0;
                    break;
            }

            // Set the Animation to active by default
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (Active == false)
                return;

            if (this.state != lastState)
            {
                elapsedTime = 0;

                switch (this.state)
                {
                    case Animate.IDLE:
                        currentFrame = 0;
                        frameCount = 1;
                        this.state = Animate.IDLE;
                        break;
                    case Animate.RMOVING:
                        currentFrame = 1;
                        frameCount = 3;
                        this.state = Animate.RMOVING;
                        break;
                    case Animate.LMOVING:
                        currentFrame = 3;
                        frameCount = 6;
                        this.state = Animate.LMOVING;
                        break;
                    default:
                        currentFrame = 0;
                        break;
                }
            }
            // Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            // If the elapsed time is larger than the frame time
            // we need to switch frames

            if (elapsedTime > frameTime)
            {
                // Move to the next frame
                currentFrame++;

                elapsedTime = 0;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    switch (this.state)
                    {
                        case Animate.IDLE:
                            currentFrame = 0;
                            frameCount = 2;
                            this.state = Animate.IDLE;
                            break;
                        case Animate.RMOVING:
                            currentFrame = 2;
                            frameCount = 5;
                            this.state = Animate.RMOVING;
                            break;
                        case Animate.LMOVING:
                            currentFrame = 5;
                            frameCount = 8;
                            this.state = Animate.LMOVING;
                            break;
                        default:
                            currentFrame = 0;
                            break;
                    }

                    // If we are not looping deactivate the animation
                    if (Looping == false)
                        Active = false;
                }
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            _sRect[0] = sourceRect.X;
            _sRect[1] = sourceRect.Y;
            _sRect[2] = sourceRect.Width;
            _sRect[3] = sourceRect.Height;

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X,
            (int)Position.Y,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));
            DRect[0] = destinationRect.X;
            DRect[1] = destinationRect.Y;
            DRect[2] = destinationRect.Width;
            DRect[3] = destinationRect.Height;

            lastState = this.state;
        }

        // Draw the Animation Strip
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Only draw the animation when we are active
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
            spriteBatch.End();
        }
    }
}