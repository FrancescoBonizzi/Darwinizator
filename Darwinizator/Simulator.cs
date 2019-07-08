using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Simulator
    {
        public int XDimension { get; }
        public int YDimension { get; }
        public int[,] World { get; set; }
        public Dictionary<Specie, List<Animal>> Population { get; set; }

        private readonly Evaluator _evaluator;

        public Simulator(int xDimension, int yDimension)
        {
            XDimension = xDimension;
            YDimension = yDimension;

            var specieGenerator = new AnimalGenerator();
            Population = specieGenerator.InitializePopulation(
                biodiversity: 10,
                populationPerSpecie: 10,
                xDimension: XDimension,
                yDimension: YDimension);

            _evaluator = new Evaluator(Population, XDimension, YDimension);
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
                        if (animal.Specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                        {
                            // A defensive runs away
                            moved = _evaluator.Flee(animal, enemyNearby, elapsed);
                        }
                        else if (animal.Specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Aggressive)
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
                        var otherGenderAllyNearby = _evaluator.AllayInLineOfSight(animal);
                        if (otherGenderAllyNearby != null)
                        {
                            moved = _evaluator.Approach(animal, otherGenderAllyNearby, elapsed);

                            if (_evaluator.IsEnoughCloseToInteract(animal, otherGenderAllyNearby)
                                && _evaluator.CanCopulate(animal, otherGenderAllyNearby))
                            {
                                var newAnimal = _evaluator.Copulate(animal, otherGenderAllyNearby);
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

                var animalKilled = specie.Value.RemoveAll(a => _evaluator.IsDead(a));
                specie.Value.AddRange(newGeneration);
                if (newGeneration.Count > 0)
                    System.Diagnostics.Debug.WriteLine($"NewGeneration: {specie.Key.Name} - {newGeneration.Count}");
                if (animalKilled > 0)
                    System.Diagnostics.Debug.WriteLine($"Killed: {specie.Key.Name} - {animalKilled}");
            }
        }

    }
}
