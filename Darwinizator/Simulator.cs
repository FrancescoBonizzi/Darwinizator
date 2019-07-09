using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Simulator
    {
        public int WorldXSize { get; }
        public int WorldYSize { get; }
        public int[,] World { get; set; }
        public Dictionary<string, List<Animal>> Population { get; set; }

        private readonly Evaluator _evaluator;

        public Simulator(
            int worldXSize,
            int worldYSize)
        {
            WorldXSize = worldXSize;
            WorldYSize = worldYSize;

            var specieGenerator = new AnimalGenerator();
            Population = specieGenerator.InitializePopulation(
                populationPerSpecie: 20,
                worldXSize: WorldXSize,
                worldYSize: WorldYSize);

            _evaluator = new Evaluator(Population, WorldXSize, WorldYSize);
        }

        public void Update(TimeSpan elapsed)
        {
            foreach (var specie in Population)
            {
                var newGeneration = new List<Animal>();

                foreach (var animal in specie.Value)
                {
                    // * Movement
                    bool moved = false;

                    // This is the difference between Aggressive and Defensive animals
                    var enemyNearby = _evaluator.FirstEnemyInLineOfSight(animal);
                    if (enemyNearby != null)
                    {
                        if (animal.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                        {
                            // A defensive runs away
                            moved = _evaluator.Flee(animal, enemyNearby, elapsed);
                        }
                        else if (animal.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Aggressive)
                        {
                            // An aggressive approaches to attack
                            moved = _evaluator.Approach(animal, enemyNearby, elapsed);
                        }
                    }

                    if (!moved)
                    {
                        // Every animal tends to stay in group with his specie
                        var allyNearby = _evaluator.AllayInLineOfSight(animal);
                        if (allyNearby != null)
                        {
                            moved = _evaluator.Approach(animal, allyNearby, elapsed);
                        }
                    }

                    // If it is in love, find a partner
                    if (_evaluator.NeedsToReproduce(animal))
                    {
                        var otherGenderAllyNearbyThatNeedsToReproduce = _evaluator.AllayInLineOfSight(animal, true);
                        if (otherGenderAllyNearbyThatNeedsToReproduce != null)
                        {
                            moved = _evaluator.Approach(animal, otherGenderAllyNearbyThatNeedsToReproduce, elapsed);

                            if (_evaluator.IsEnoughCloseToInteract(animal, otherGenderAllyNearbyThatNeedsToReproduce))
                            {
                                var father = animal.Gender == Gender.Male ? animal : otherGenderAllyNearbyThatNeedsToReproduce;
                                var mother = animal.Gender == Gender.Female ? animal : otherGenderAllyNearbyThatNeedsToReproduce;

                                var newAnimal = _evaluator.Copulate(father: father, mother: mother);
                                newGeneration.Add(newAnimal);
                            }
                        }
                    }

                    // If there is nothing interesting to do, move randomly
                    if (!moved)
                    {
                        _evaluator.RandomMove(animal, elapsed);
                    }


                    // * Attack
                    if (enemyNearby != null)
                    {
                        if (_evaluator.IsEnoughCloseToInteract(animal, enemyNearby))
                        {
                            // The animal needs to defend himself
                            _evaluator.Attack(animal, enemyNearby);
                        }
                    }

                    _evaluator.EvaluateAge(animal);
                }

                specie.Value.RemoveAll(a => _evaluator.IsDead(a));
                specie.Value.AddRange(newGeneration);
            }
        }

    }
}
