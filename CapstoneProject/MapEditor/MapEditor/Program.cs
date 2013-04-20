using System;

namespace MapEditor
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        [STAThread]
        private static void Main(string[] args)
        {
            using (MapEditorMain game = new MapEditorMain())
            {
                game.Run();
            }
        }
    }

#endif
}