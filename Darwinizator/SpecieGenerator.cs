using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class SpecieGenerator
    {
        public List<Animal> InitializePopulation(
            int biodiversity,
            int populationPerSpecie)
        {
            var population = new List<Animal>();

            var speciesNames = new List<string>();
            for (int s = 0; s < biodiversity; ++s)
            {
                speciesNames.Add(GenerateName());
            }

            foreach (var specie in speciesNames)
            {
                for (int p = 0; p < populationPerSpecie; ++p)
                {
                    population.Add(new Animal()
                    {
                        Gender = p % 2 == 0 ? Gender.Male : Gender.Female,
                        Specie = specie,
                        Lifetime = 100,
                        MovementSpeed = 5,
                        SocialIstinctToOtherSpecies = SocialIstinctToOtherSpecies.Aggressive,
                        SocialIstinctToSameSpecies = SocialIstinctToSameSpecies.Groupful
                    });
                }
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
