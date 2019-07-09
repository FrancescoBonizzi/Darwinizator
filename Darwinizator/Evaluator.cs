using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Evaluator
    {
        private readonly Dictionary<string, List<Animal>> _population;
        private readonly int _worldX;
        private readonly int _worldY;
        private static readonly Random _random = new Random();

        public Evaluator(
            Dictionary<string, List<Animal>> population,
            int worldX,
            int worldY)
        {
            _population = population;
            _worldX = worldX;
            _worldY = worldY;
        }

        internal int Distance(Animal a, Animal b)
        {
            return Distance(a.PosX, a.PosY, b.PosX, b.PosY);
        }

        internal int Distance(float aPosX, float aPosY, float bPosX, float bPosY)
        {
            return Convert.ToInt32(
                Math.Sqrt(
                    Math.Pow(aPosX - bPosX, 2)
                    + Math.Pow(aPosY - bPosY, 2)));
        }
        
        internal Animal FirstEnemyInLineOfSight(Animal animal)
        {
            foreach (var specie in _population)
            {
                // An animal of the same specie isn't an enemy
                if (specie.Key == animal.SpecieName)
                    continue;

                foreach (var otherAnimal in specie.Value)
                {
                    // A defensive enemy isn't an enemy if you are defensive
                    if (animal.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive
                        && otherAnimal.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                    {
                        continue;
                    }

                    if (Distance(otherAnimal, animal) <= animal.SeeDistance)
                    {
                        return otherAnimal;
                    }
                }
            }

            return null;
        }

        internal bool Approach(Animal who, Animal to, TimeSpan elapsed)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.MovementSpeed * elapsed.TotalSeconds);

            if (who.PosX <= to.PosX)
            {
                xMoveAmount = 1 * multiplier;
            }
            else
            {
                xMoveAmount = -1 * multiplier;
            }

            if (who.PosY <= to.PosY)
            {
                yMoveAmount = 1 * multiplier;
            }
            else
            {
                yMoveAmount = -1 * multiplier;
            }

            if (!IsNewPositionPossible(who, xMoveAmount, yMoveAmount))
                return false;

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;

            return true;
        }

        internal Animal Copulate(Animal father, Animal mother)
        {
            if (father.SpecieName != mother.SpecieName)
                throw new Exception("Cannot copulate between different species");

            var son = AnimalGenerator.GenerateAnimal(father: father, mother: mother);
            father.NextYearCanReprouce += father.IntervalBetweenReproductions;
            mother.NextYearCanReprouce += mother.IntervalBetweenReproductions;

            return son;
        }

        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.NextYearCanReprouce;
        }

        internal bool RandomMove(Animal who, TimeSpan elapsed)
        {
            float xMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;
            float yMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;

            var multiplier = (float)(who.MovementSpeed * elapsed.TotalSeconds);

            xMoveAmount *= multiplier;
            yMoveAmount *= multiplier;

            if (!IsNewPositionPossible(who, xMoveAmount, yMoveAmount))
                return false;

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;

            return true;
        }

        internal bool IsNewPositionPossible(
            Animal who,
            float xMoveAmount,
            float yMoveAmount)
        {
            if (!CouldBeOutOfWorld(who.PosX + xMoveAmount, _worldX))
                return false;

            if (!CouldBeOutOfWorld(who.PosY + yMoveAmount, _worldY))
                return false;

            if (!CouldBeOnAnotherAnimal(
                who,
                who.PosX + xMoveAmount,
                who.PosY + yMoveAmount))
            {
                return false;
            }

            return true;
        }

        internal bool Flee(Animal who, Animal from, TimeSpan elasped)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.MovementSpeed * elasped.TotalSeconds);

            // Da migliorare enormemente
            if (who.PosX <= from.PosX)
            {
                xMoveAmount = -1 * multiplier;
            }
            else
            {
                xMoveAmount = 1 * multiplier;
            }

            if (who.PosY <= from.PosY)
            {
                yMoveAmount = -1 * multiplier;
            }
            else
            {
                yMoveAmount = 1 * multiplier;
            }

            if (!IsNewPositionPossible(who, xMoveAmount, yMoveAmount))
                return false;

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;

            return true;
        }

        internal void Attack(Animal attacker, Animal attacked)
        {
            var botta = attacker.AttackPower - attacked.DefensePower;
            if (botta < 0)
                botta = 0;

            attacked.Health -= botta;
        }

        internal bool CouldBeOutOfWorld(float pos, int reference)
        {
            return pos < reference && pos >= 0;
        }

        internal bool CouldBeOnAnotherAnimal(Animal animal, float newPosX, float newPosY)
        {
            // TODO Ovviamente poi indicizzerò la griglia perché così fa schifo
            foreach (var s in _population.Values)
            {
                foreach (var a in s)
                {
                    if (a == animal)
                        continue;

                    // Soglia per determinare se due animali non possono essere sovrapposti
                    if (Distance(a.PosX, a.PosY, newPosX, newPosY) <= 0.9f)
                        return false;
                }
            }

            return true;
        }

        internal bool IsEnoughCloseToInteract(Animal who, Animal withWho)
        {
            return Distance(who, withWho) <= 1;
        }

        internal void EvaluateAge(Animal animal)
        {
            animal.Age += 0.01f;
        }

        internal bool IsDead(Animal animal)
        {
            return animal.Age >= animal.Lifetime || animal.Health <= 0;
        }

        internal Animal AllayInLineOfSight(Animal animal, bool? searchForReproduction = null)
        {
            var thisAnimalPopulation = _population[animal.SpecieName];
            foreach (var a in thisAnimalPopulation)
            {
                if (a == animal)
                    continue;

                if (searchForReproduction != null && a.Gender == animal.Gender)
                    continue;

                if (searchForReproduction != null && !NeedsToReproduce(a))
                    continue;

                if (Distance(animal, a) <= animal.SeeDistance)
                {
                    return a;
                }
            }

            return null;
        }
    }
}
