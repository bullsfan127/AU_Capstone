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
    public class Animation
    {
        public enum Animate { IDLE, RMOVING, LMOVING };

        public Animate state;

        private Animate lastState;

        // The image representing the collection of images used for animation
        Texture2D spriteStrip;

        // The scale used to display the sprite strip
        float scale;

        // The time since we last updated the frame
        int elapsedTime;

        // The time we display a frame until the next one
        int frameTime;

        // The number of frames that the animation contains
        int frameCount;

        // The index of the current frame we are displaying
        public int currentFrame;

        // The color of the frame we will be displaying
        Color color;

        // The area of the image strip we want to display
        public Rectangle sourceRect = new Rectangle();

        // The area where we want to display the image strip in the game
        public Rectangle destinationRect = new Rectangle();

        // Width of a given frame
        public int FrameWidth;

        // Height of a given frame
        public int FrameHeight;

        // The state of the Animation
        public bool Active;

        // Determines if the animation will keep playing or deactivate after one run
        public bool Looping;

        // Width of a given frame
        public Vector2 Position;

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
                    currentFrame = 2;
                    this.state = Animate.RMOVING;
                    break;
                case Animate.LMOVING:
                    currentFrame = 5;
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

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X,
            (int)Position.Y,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));

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