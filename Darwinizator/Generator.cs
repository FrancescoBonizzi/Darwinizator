using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Generator
    {
        private static readonly Random _random = new Random();

        private readonly int _worldXSize;
        private readonly int _worldYSize;

        private int _animalSequenceNumber = 0;

        private readonly Evolutionator _evolutionator;

        public Generator(
            int worldXSize,
            int worldYSize)
        {
            _worldXSize = worldXSize;
            _worldYSize = worldYSize;
            _evolutionator = new Evolutionator();
        }

        public Dictionary<string, List<Animal>> InitializePopulation(int populationPerSpecie)
        {
            var population = new Dictionary<string, List<Animal>>();

            // To avoid spawing animal on top of each other
            var availableCoordinates = new List<(int X, int Y)>();
            for (int x = 0; x < _worldXSize; ++x)
            {
                for (int y = 0; y < _worldYSize; ++y)
                {
                    availableCoordinates.Add((x, y));
                }
            }

            {
                const Diet socialInstinct = Diet.Carnivorous;
                var specieName = socialInstinct.ToString();
                var animals = new List<Animal>();
                for (int i = 0; i < populationPerSpecie; ++i)
                {
                    var choosenCoordinatesIndex = _random.Next(0, availableCoordinates.Count);
                    var (X, Y) = availableCoordinates[choosenCoordinatesIndex];
                    availableCoordinates.RemoveAt(choosenCoordinatesIndex);

                    animals.Add(GenerateAnimal(
                        specieName: specieName,
                        diet: socialInstinct,
                        maleColor: "ff0000",
                        femaleColor: "ff0066",
                        X,
                        Y));
                }

                population.Add(specieName, animals);
            }

            {
                const Diet socialInstinct = Diet.Herbivore;
                var specieName = socialInstinct.ToString();
                var animals = new List<Animal>();
                for (int i = 0; i < populationPerSpecie; ++i)
                {
                    var choosenCoordinatesIndex = _random.Next(0, availableCoordinates.Count);
                    var (X, Y) = availableCoordinates[choosenCoordinatesIndex];
                    availableCoordinates.RemoveAt(choosenCoordinatesIndex);

                    animals.Add(GenerateAnimal(
                        specieName: specieName,
                        diet: socialInstinct,
                        maleColor: "006eff",
                        femaleColor: "00fff2",
                        X,
                        Y));
                }

                population.Add(specieName, animals);
            }

            return population;
        }

        internal List<Vegetable> SpawnVegetables(int howMany)
        {
            var vegetables = new List<Vegetable>();

            // To avoid vegetables animal on top of each other
            var availableCoordinates = new List<(int X, int Y)>();
            for (int x = 0; x < _worldXSize; ++x)
            {
                for (int y = 0; y < _worldYSize; ++y)
                {
                    availableCoordinates.Add((x, y));
                }
            }

            for (int i = 0; i < howMany; ++i)
            {
                var choosenCoordinatesIndex = _random.Next(0, availableCoordinates.Count);
                var (X, Y) = availableCoordinates[choosenCoordinatesIndex];
                availableCoordinates.RemoveAt(choosenCoordinatesIndex);

                vegetables.Add(new Vegetable()
                {
                    Mass = new Mass()
                    {
                        PosX = X,
                        PosY = Y,
                        Width = 2,
                        Height = 2
                    },
                    Age = 0,
                    IsEaten = false,
                    Color = "228b22"
                });
            }

            return vegetables;
        }

        public Animal GenerateAnimal(
            Animal father,
            Animal mother)
        {
            var animal = GenerateAnimal(
                father.SpecieName,
                father.Diet,
                father.Color,
                mother.Color,
                (int)mother.Mass.PosX,
                (int)mother.Mass.PosY);

            animal.Father = father;
            animal.Mother = mother;

            animal.IntervalBetweenReproductions = mother.IntervalBetweenReproductions;
            animal.GenerationAge = Math.Max(father.GenerationAge, mother.GenerationAge) + 1;

            // Randomization of animal traits based on its parents
            _evolutionator.RandomizeTraits(father, mother, animal);

            // When an animal borns, it is hungry
            animal.Energy = animal.MaximumEnergy - (animal.MaximumEnergy / 3);

            return animal;
        }

        public Animal GenerateAnimal(
            string specieName,
            Diet diet,
            string maleColor,
            string femaleColor,
            int posX,
            int posY)
        {
            var gender = _random.NextDouble() >= 0.5 ? Gender.Male : Gender.Female;
            var color = gender == Gender.Male ? maleColor : femaleColor;

            ++_animalSequenceNumber;

            return new Animal()
            {
                SpecieName = specieName,
                Color = color,
                Diet = diet,
                Gender = gender,
                Age = 0,
                Father = null,
                Mother = null,
                NextAgeCanReproduce = StartingValues.IntervalBetweenReproducions,
                Energy = StartingValues.MaximumEnergy,
                EnergyGainForEating = diet == Diet.Herbivore ? StartingValues.EnergyGainForEatingPlants : StartingValues.EnergyGainForEatingAnimals,

                GenerationAge = 1,

                Lifetime = StartingValues.Lifetime,
                MovementSpeed = StartingValues.MovementSpeed,
                SeeDistance = StartingValues.SeeDistance,

                AttackPower = StartingValues.AttackPower,
                DefensePower = StartingValues.DefensePower,
                IntervalBetweenReproductions = StartingValues.IntervalBetweenReproducions,
                MaximumEnergy = StartingValues.MaximumEnergy,
                EnergyAmountToSearchForFood = StartingValues.EnergyAmountToStartSearchingForFood,
                Mass = new Mass()
                {
                    PosX = posX,
                    PosY = posY,
                    Width = 6,
                    Height = 6
                },
                Name = $"A{_animalSequenceNumber}"
            };
        }
    }
}
