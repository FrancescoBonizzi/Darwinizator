using Darwinizator;
using Darwinizator.Domain;
using System;
using System.Linq;

namespace Playground
{
    static class Program
    {
        private static Simulator _simulation;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to DARWINIZATOR!");
            Console.WriteLine();

            _simulation = new Simulator();

            int i = 0;
            while (true)
            {
                _simulation.Update();
                if (i == short.MaxValue)
                {
                    i = 0;
                    ShowInfos();
                }

                ++i;
            }
        }

        private static void ShowInfos()
        {
            foreach(var p in _simulation.Population)
            {
                Console.WriteLine(p.Key);
                Console.WriteLine($"\tPopulation: {p.Value.Count}");
                Console.WriteLine($"\tMales: {p.Value.Count(a => a.Gender == Gender.Male)}");
                Console.WriteLine($"\tPopulation: {p.Value.Count(a => a.Gender == Gender.Female)}");
                foreach(var a in p.Value)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tPosition: ({a.PosX}; {a.PosY})");
                    Console.WriteLine($"\tLifetime: {a.Lifetime}");
                    Console.WriteLine($"\tHealth: {a.Health}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }

    }
}
