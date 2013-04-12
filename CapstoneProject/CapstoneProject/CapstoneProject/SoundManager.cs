using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;


namespace CapstoneProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SoundManager 
    {
        private ContentManager _content;

        public ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }

        GAMESTATE lastState = GAMESTATE.PAUSE;
        KeyboardState keystate = Keyboard.GetState();
        SoundEffect sound;


        private string[] songs = Directory.GetFiles(@"..\..\..\..\CapstoneProjectContent\Songs", "*.wav")
                                     .Select(path => Path.GetFileNameWithoutExtension(path))
                                     .ToArray();
        private string[] sounds = Directory.GetFiles(@"..\..\..\..\CapstoneProjectContent\Sounds", "*.wav")
                                     .Select(path => Path.GetFileNameWithoutExtension(path))
                                     .ToArray();

        public SoundManager(Game game, ContentManager content)
            
        {
            // Pass in content manager
            _content = content;
        }

        public void PlaySong()
        {
            int i;

            if (lastState != Game1.gameState)
            {
                switch (Game1.gameState)
                {
                    case GAMESTATE.MAINMENU:
                        i = 4;
                        break;
                    case GAMESTATE.PLAY:
                        i = 0;
                        break;
                    default:
                        i = 1;
                        break;
                }

                lastState = Game1.gameState;

                // Load song
                Song bgm = _content.Load<Song>("Songs\\" + songs[i]);


                // Play song
                try
                {
                    MediaPlayer.Play(bgm);

                    MediaPlayer.IsRepeating = true;
                }
                catch
                {
                }
            }
        }

        public void PlaySound(int i)
        {
            sound = _content.Load<SoundEffect>("Sounds\\" + sounds[i]);
            sound.Play();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
      
    }
}
