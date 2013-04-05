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
    public class SoundManagement : Microsoft.Xna.Framework.GameComponent
    {
        private ContentManager _content;

        public ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string[] songs = Directory.GetFiles("Songs", "*.wav")
                                     .Select(path => Path.GetFileName(path))
                                     .ToArray();

        public SoundManagement(Game game, ContentManager content)
            : base(game)
        {
            // Pass in content manager
            _content = content;
        }

       
        public override void LoadSong()
        {

            // Loading songs
            _content.Load<Song>(songs[0]);
            
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
