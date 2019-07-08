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

            var specieGenerator = new SpecieGenerator();
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
                foreach (var animal in specie.Value)
                {
                    //if (_evaluator.NeedsToReproduce(animal))
                    //{
                    //    // Se è in calore si muove verso la prima femmina il linea d'aria
                    //    throw new NotImplementedException();
                    //}


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

                    // If there is nothing interesting to do, move randomly
                    if (!moved)
                    {
                        _evaluator.RandomMove(animal, elapsed);
                    }

                    // * Attack
                    if (enemyNearby != null)
                    {
                        if (_evaluator.IsEnoughCloseToAttack(animal, enemyNearby))
                        {
                            // The animal needs to defend himself
                            _evaluator.Attack(animal, enemyNearby);
                        }
                    }

                    _evaluator.EvaluateAge(animal);
                }

                specie.Value.RemoveAll(a => _evaluator.IsDead(a));
            }
        }

    }
}
