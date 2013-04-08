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
    public class SoundManager : Microsoft.Xna.Framework.GameComponent
    {
        private ContentManager _content;

        public ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }

        MediaQueue queue;

        Song bgm;
     

        private string[] songs = Directory.GetFiles(@"..\..\..\..\CapstoneProjectContent\Songs", "*.wav")
                                     .Select(path => Path.GetFileNameWithoutExtension(path))
                                     .ToArray();

        public SoundManager(Game game, ContentManager content)
            : base(game)
        {
            // Pass in content manager
            _content = content;
        }

       

        public void QueueSongs()
        {
            for (int i=0; i < songs.Length; i++)
            {
            bgm = _content.Load<Song>("Songs\\" + songs[i]);
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

        //public void PlaySong(Song bgm)
        //{
            
        //    // Play song
        //    try
        //    {
        //        MediaPlayer.Play(bgm);

        //        MediaPlayer.IsRepeating = true;
        //    }
        //    catch
        //    {
        //    }

        //}

        public void SwitchSong()
        {
            MediaPlayer.Stop();
            MediaPlayer.MovePrevious();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
