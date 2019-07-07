using Darwinizator;
using Darwinizator.Domain;
using System;
using System.Diagnostics;
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

            var timer = new Stopwatch();
            timer.Start();

            _simulation = new Simulator();
            ShowInfos();

            while (true)
            {
                _simulation.Update();
                if (timer.Elapsed >= TimeSpan.FromSeconds(5))
                {
                    timer.Restart();
                    ShowInfos();
                }
            }
        }

        private static void ShowInfos()
        {
            Console.Clear();

            foreach(var p in _simulation.Population)
            {
                Console.WriteLine(p.Key.Name);
                Console.WriteLine($"\tPopulation: {p.Value.Count}");
                Console.WriteLine($"\tMales: {p.Value.Count(a => a.Gender == Gender.Male)}");
                Console.WriteLine($"\tFemales: {p.Value.Count(a => a.Gender == Gender.Female)}");
                foreach(var a in p.Value)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tPosition: ({a.PosX}; {a.PosY})");
                    Console.WriteLine($"\tHealth: {a.Health}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }

    }
}
