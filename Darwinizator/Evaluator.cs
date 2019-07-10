using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Evaluator
    {
        private static readonly Random _random = new Random();
        private readonly Dictionary<string, List<Animal>> _population;
        private readonly int _worldX;
        private readonly int _worldY;

        private const float _positioningThreshold = 2f;

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
            return Distance(a.Mass.PosX, a.Mass.PosY, b.Mass.PosX, b.Mass.PosY);
        }

        internal int Distance(float aPosX, float aPosY, float bPosX, float bPosY)
        {
            return (int)(
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

            if (who.Mass.PosX <= to.Mass.PosX)
            {
                xMoveAmount = 1 * multiplier;
            }
            else
            {
                xMoveAmount = -1 * multiplier;
            }

            if (who.Mass.PosY <= to.Mass.PosY)
            {
                yMoveAmount = 1 * multiplier;
            }
            else
            {
                yMoveAmount = -1 * multiplier;
            }

            return SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal bool SetNewPosition(
            Animal who,
            float xMoveAmount,
            float yMoveAmount)
        {
            var newPosX = ClampToWorldBounds(who.Mass.PosX + xMoveAmount, _worldX - who.Mass.Weight);
            var newPosY = ClampToWorldBounds(who.Mass.PosY + yMoveAmount, _worldY - who.Mass.Height);

            if (CouldGoOnAnotherAnimal(
               who,
               newPosX,
               newPosY))
            {
                return false;
            }

            who.Mass.PosX = newPosX;
            who.Mass.PosY = newPosY;

            return true;
        }

        internal Animal Copulate(Animal father, Animal mother)
        {
            if (father.SpecieName != mother.SpecieName)
                throw new Exception("Cannot copulate between different species");

            var son = Generator.GenerateAnimal(father: father, mother: mother);
            father.NextYearCanReprouce += father.IntervalBetweenReproductions;
            mother.NextYearCanReprouce += mother.IntervalBetweenReproductions;

            return son;
        }

        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.NextYearCanReprouce;
        }

        internal void RandomMove(Animal who, TimeSpan elapsed)
        {
            float xMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;
            float yMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;

            var multiplier = (float)(who.MovementSpeed * elapsed.TotalSeconds);

            xMoveAmount *= multiplier;
            yMoveAmount *= multiplier;

            SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal float ClampToWorldBounds(float value, float referenceValue)
        {
            if (value < 0)
                return 0;

            if (value > referenceValue)
                return referenceValue;

            return value;
        }

        internal bool Flee(Animal who, Animal from, TimeSpan elasped)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.MovementSpeed * elasped.TotalSeconds);

            // Da migliorare enormemente
            if (who.Mass.PosX <= from.Mass.PosX)
            {
                xMoveAmount = -1 * multiplier;
            }
            else
            {
                xMoveAmount = 1 * multiplier;
            }

            if (who.Mass.PosY <= from.Mass.PosY)
            {
                yMoveAmount = -1 * multiplier;
            }
            else
            {
                yMoveAmount = 1 * multiplier;
            }

            return SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal void Attack(Animal attacker, Animal attacked)
        {
            var smash = (attacker.AttackPower + _random.Next(1, 3))
                - (attacked.DefensePower + _random.Next(1, 3));

            if (smash < 0)
            {
                // If defense is bigger, attaccker gets hit
                attacker.Health += smash;
            }
            else if (smash > 0)
            {
                // If attack is bigger, attaccked gets hit
                attacked.Health -= smash;
            }
        }

        internal bool CouldGoOnAnotherAnimal(Animal animal, float newPosX, float newPosY)
        {
            var newMass = new Mass()
            {
                PosX = newPosX,
                PosY = newPosY,
                Width = animal.Mass.Width,
                Height = animal.Mass.Height
            };

            // TODO Ovviamente poi indicizzerò la griglia perché scorrerli tutti fa schifo
            foreach (var s in _population.Values)
            {
                foreach (var a in s)
                {
                    if (a == animal)
                        continue;

                    if (newMass.Intersects(a.Mass, _positioningThreshold))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal bool IsEnoughCloseToInteract(Animal who, Animal withWho)
        {
            return who.Mass.Intersects(withWho.Mass);
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
