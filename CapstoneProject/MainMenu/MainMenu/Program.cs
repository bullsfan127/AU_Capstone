using System;

namespace MainMenu
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            using (MainMenu game = new MainMenu())
            {
                game.Run();
            }
        }
    }

#endif
}