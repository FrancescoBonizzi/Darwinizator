using Darwinizator;
using System;

namespace GUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            const int simulationWorldWidth = 500;
            const int simulationWorldHeight = 500;

            var simulator = new Simulator(simulationWorldWidth, simulationWorldHeight);
            using (var game = new DarwinatorRenderer(simulator))
            {
                game.Run();
            }
        }
    }
}
