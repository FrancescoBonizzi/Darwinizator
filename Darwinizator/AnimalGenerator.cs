using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class AnimalGenerator
    {
        private static readonly Random _random = new Random();
        private readonly ColorsProvider _colorsProvider = new ColorsProvider();

        public Dictionary<Specie, List<Animal>> InitializePopulation(
            int biodiversity,
            int populationPerSpecie,
            int xDimension,
            int yDimension)
        {
            var population = new Dictionary<Specie, List<Animal>>();

            var speciesNames = new List<string>();
            for (int s = 0; s < biodiversity; ++s)
            {
                speciesNames.Add($"s{s}");
            }

            foreach (var specieName in speciesNames)
            {
                var specie = new Specie()
                {
                    Name = specieName,
                    SocialIstinctToOtherSpecies = SocialIstinctToOtherSpecies.Defensive, //_random.Next() >= 0.7 ? SocialIstinctToOtherSpecies.Aggressive : SocialIstinctToOtherSpecies.Defensive,
                    MovementSpeed = 30,
                    SeeDistance = 10,
                    Lifetime = 20,
                    MaxSons = 3,
                    Color = _colorsProvider.GetNextColor()
                };

                var animals = new List<Animal>();
                for (int p = 0; p < populationPerSpecie; ++p)
                {
                    animals.Add(GenerateAnimal(
                        specie,
                        p % 2 == 0 ? Gender.Male : Gender.Female,
                        _random.Next(0, xDimension),
                        _random.Next(0, yDimension)));
                }

                population.Add(specie, animals);
            }

            return population;
        }

        public static Animal GenerateAnimal(
            Specie specie,
            Gender gender,
            float posX,
            float posY)
        {
            return new Animal()
            {
                Gender = gender,
                Specie = specie,

                PosX = posX,
                PosY = posY,

                Health = 20,
                Age = 0,

                AttackPower = specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Aggressive ? 5 : 2,
                DefensePower = specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive ? 10 : 2
                // SocialIstinctToSameSpecies = SocialIstinctToSameSpecies.Groupful
            };
        }
    }
}
