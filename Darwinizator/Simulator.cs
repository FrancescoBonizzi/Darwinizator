using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Simulator
    {
        public int WorldXSize { get; }
        public int WorldYSize { get; }

        private readonly Generator _generator;

        public int[,] World { get; set; }
        public Dictionary<string, List<Animal>> Population { get; set; }
        public List<Vegetable> Vegetables { get; set; }

        private TimeSpan _elapsedSinceLastVegetableGeneration;

        private readonly Evaluator _evaluator;
        public int DeadCarnivorousNumber { get; private set; } = 0;
        public int DeadHerbivoreNumber { get; private set; } = 0;

        public Simulator(
            int worldXSize,
            int worldYSize)
        {
            WorldXSize = worldXSize;
            WorldYSize = worldYSize;

            _generator = new Generator(WorldXSize, WorldYSize);

            Population = _generator.InitializePopulation(StartingValues.PopulationPerSpecie);
            Vegetables = _generator.SpawnVegetables(StartingValues.NumberOfVegetables);

            _evaluator = new Evaluator(
                _generator,
                Population,
                Vegetables,
                WorldXSize,
                WorldYSize);

            _elapsedSinceLastVegetableGeneration = TimeSpan.Zero;
        }

        public void Update(TimeSpan elapsed)
        {
            _elapsedSinceLastVegetableGeneration += elapsed;

            foreach (var specie in Population)
            {
                var newGeneration = new List<Animal>();

                foreach (var animal in specie.Value)
                {
                    // He could die due to another animal attack
                    if (_evaluator.IsDead(animal))
                        continue;

                    // * Movement
                    bool moved = false;

                    // Hunger is always the priority
                    if (animal.IsHungry)
                    {
                        if (animal.Diet == Diet.Herbivore)
                        {
                            var vegetableNearby = _evaluator.FirstVegetableInLineOfSight(animal);
                            if (vegetableNearby != null)
                            {
                                moved = _evaluator.Approach(animal, vegetableNearby, elapsed);

                                if (_evaluator.IsEnoughCloseToInteract(animal, vegetableNearby))
                                {
                                    _evaluator.Eat(animal, vegetableNearby);
                                }
                            }
                        }
                        else if (animal.Diet == Diet.Carnivorous)
                        {
                            var enemyNearby = _evaluator.FirstEnemyInLineOfSight(animal);
                            if (enemyNearby != null)
                            {
                                moved = _evaluator.Approach(animal, enemyNearby, elapsed);

                                if (_evaluator.IsEnoughCloseToInteract(animal, enemyNearby))
                                {
                                    _evaluator.Attack(animal, enemyNearby);
   
                                    if (_evaluator.IsDead(enemyNearby))
                                    {
                                        _evaluator.Eat(animal, enemyNearby);
                                    }

                                    if (_evaluator.IsDead(animal))
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (animal.Diet == Diet.Herbivore)
                        {
                            var enemyNearby = _evaluator.FirstEnemyInLineOfSight(animal);
                            if (enemyNearby != null)
                            {
                                moved = _evaluator.Flee(animal, enemyNearby, elapsed);
                            }
                        }

                        // If it is in love, find a partner
                        if (!moved && _evaluator.NeedsToReproduce(animal))
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

                        if (!moved)
                        {
                            // Every animal tends to stay in group with his specie
                            var allyNearby = _evaluator.AllayInLineOfSight(animal);
                            if (allyNearby != null)
                            {
                                moved = _evaluator.Approach(animal, allyNearby, elapsed);
                            }
                        }
                    }

                    // Move randomly to feel alive
                    _evaluator.RandomMove(animal, elapsed);
                    _evaluator.EvaluateMoveEnergyCost(animal);
                    _evaluator.EvaluateAge(animal);
                }

                for (int a = specie.Value.Count - 1; a >= 0; --a)
                {
                    if (_evaluator.IsDead(specie.Value[a]))
                    {
                        if (specie.Value[a].Diet == Diet.Carnivorous)
                            DeadCarnivorousNumber++;
                        else DeadHerbivoreNumber++;

                        _evaluator.Kill(specie.Value[a]);
                         specie.Value.RemoveAt(a);
                    }
                }

                specie.Value.AddRange(newGeneration);
            }

            for(int v = Vegetables.Count -1; v >= 0; --v)
            {
                _evaluator.EvaluateAge(Vegetables[v]);
                if (Vegetables[v].IsEaten)
                    Vegetables.RemoveAt(v);
            }

            if (_elapsedSinceLastVegetableGeneration > StartingValues.IntervalForVegetablesGeneration)
            {
                Vegetables.AddRange(_generator.SpawnVegetables(StartingValues.NumberOfVegetables - Vegetables.Count));
                _elapsedSinceLastVegetableGeneration = TimeSpan.Zero;
            }

        }

    }
}
