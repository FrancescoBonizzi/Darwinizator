using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class SpecieGenerator
    {
        private static Random _random = new Random();
        private ColorsProvider _colorsProvider = new ColorsProvider();

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
                    SocialIstinctToOtherSpecies = SocialIstinctToOtherSpecies.Defensive //_random.Next() >= 0.7 ? SocialIstinctToOtherSpecies.Aggressive : SocialIstinctToOtherSpecies.Defensive,
                    MovementSpeed = 30,
                    SeeDistance = 8,
                    Lifetime = 50,
                    Color = _colorsProvider.GetNextColor()
                };

                var animals = new List<Animal>();
                for (int p = 0; p < populationPerSpecie; ++p)
                {
                    animals.Add(
                        new Animal()
                        {
                            Gender = p % 2 == 0 ? Gender.Male : Gender.Female,
                            Specie = specie,

                            PosX = _random.Next(0, xDimension),
                            PosY = _random.Next(0, yDimension),

                            Health = 20,
                            Age = 0,

                            AttackPower = specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Aggressive ? 5 : 2,
                            DefensePower = specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive ? 10 : 2
                            // SocialIstinctToSameSpecies = SocialIstinctToSameSpecies.Groupful
                        });
                }

                population.Add(specie, animals);
            }

            return population;
        }
    }
}
