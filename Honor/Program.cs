using GameEngine.Systems.Main;

namespace Honor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            SystemManager.Initialize();
            RenderSystem.window.Run();
        }
    }
}
