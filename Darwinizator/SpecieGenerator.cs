using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class SpecieGenerator
    {
        private static Random _random = new Random();

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
                speciesNames.Add(GenerateName());
            }

            foreach (var specieName in speciesNames)
            {
                var specie = new Specie() { Name = specieName };
                var animals = new List<Animal>();
                for (int p = 0; p < populationPerSpecie; ++p)
                {
                    animals.Add(
                        new Animal()
                        {
                            Gender = p % 2 == 0 ? Gender.Male : Gender.Female,
                            Specie = specie,
                            Lifetime = 100,
                            MovementSpeed = 5,
                            SocialIstinctToOtherSpecies = SocialIstinctToOtherSpecies.Aggressive,
                            PosX = _random.Next(0, xDimension),
                            PosY = _random.Next(0, yDimension),
                            // SocialIstinctToSameSpecies = SocialIstinctToSameSpecies.Groupful
                        });
                }

                population.Add(specie, animals);
            }

            return population;
        }

        private string GenerateName()
        {
            // TODO poi voglio un elenco di tutte le specie umane
            return Guid.NewGuid().ToString("N");
        }
    }
}
