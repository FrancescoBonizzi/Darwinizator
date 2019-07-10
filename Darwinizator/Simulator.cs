﻿using Darwinizator.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Darwinizator
{
    public class Simulator
    {
        public int WorldXSize { get; }
        public int WorldYSize { get; }

        private readonly Generator _specieGenerator;

        public int[,] World { get; set; }
        public Dictionary<string, List<Animal>> Population { get; set; }

        private readonly Evaluator _evaluator;

        public Simulator(
            int worldXSize,
            int worldYSize)
        {
            WorldXSize = worldXSize;
            WorldYSize = worldYSize;

            _specieGenerator = new Generator();
            Population = _specieGenerator.InitializePopulation(
                populationPerSpecie: 30,
                worldXSize: WorldXSize,
                worldYSize: WorldYSize);

            _evaluator = new Evaluator(_specieGenerator, Population, WorldXSize, WorldYSize);
        }

        public void Update(TimeSpan elapsed)
        {
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

                    // * Attack
                    if (enemyNearby != null)
                    {
                        if (_evaluator.IsEnoughCloseToInteract(animal, enemyNearby))
                        {
                            _evaluator.Attack(animal, enemyNearby);
                            if (_evaluator.IsDead(animal))
                            {
                                continue;
                            }
                        }
                    }

                    // Move randomly to feel alive
                    bool randomMoved = moved;
                    randomMoved = _evaluator.RandomMove(animal, elapsed);

                    _evaluator.EvaluateAge(animal);
                }

                for (int a = specie.Value.Count - 1; a >= 0; --a)
                {
                    if (_evaluator.IsDead(specie.Value[a]))
                    {
                        _evaluator.Kill(specie.Value[a]);
                        specie.Value.RemoveAt(a);
                    }
                }

                specie.Value.AddRange(newGeneration);
            }
        }

    }
}
