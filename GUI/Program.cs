using Darwinizator;
using System;

namespace GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            int simulationWorldWidth = 100;
            int simulationWorldHeight = 100;

            int cellSizePixel = 6; // Una cella nel mondo della simulazione è un rettangolo di cellSizePixel pixel rappresentato

            var simulator = new Simulator(simulationWorldWidth, simulationWorldHeight);
            using (var game = new DarwinatorRenderer(simulator, cellSizePixel))
            {
                game.Run();
            }
        }
    }
}
