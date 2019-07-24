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

            var simulator = new Simulator(simulationWorldWidth, simulationWorldHeight, TimeSpan.FromSeconds(1));


            using (var game = new DarwinatorRenderer(simulator))
            {
                using (var infosForm = new GetEvolution(simulator, game))
                {
                    infosForm.Show();
                    game.Run();
                }
            }
        }
    }
}

