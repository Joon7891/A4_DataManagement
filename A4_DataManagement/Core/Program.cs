using System;

namespace A4_DataManagement
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new CoffeeShopSimulation())
                game.Run();
        }
    }
#endif
}
