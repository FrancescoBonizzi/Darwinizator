using System;
using System.Collections.Generic;
using Darwinizator.Domain;

namespace Darwinizator
{
    public class Evaluator
    {
        private readonly Dictionary<Specie, List<Animal>> _population;
        private readonly int _worldX;
        private readonly int _worldY;

        public Evaluator(
            Dictionary<Specie, List<Animal>> population,
            int worldX,
            int worldY)
        {
            _population = population;
            _worldX = worldX;
            _worldY = worldY;
        }

        internal int QuadraticDistance(Animal a, Animal b)
        {
            return Convert.ToInt32(Math.Pow(a.PosX - b.PosX, 2) + Math.Pow(a.PosY - b.PosY, 2));
        }

        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.Specie.Lifetime / 4;
        }

        internal Animal NeedsToFlee(Animal animal)
        {
            // True se c'è un animale di un'altra specie aggressivo vicino.

            if (animal.Specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Aggressive)
                return null;
          
            foreach (var s in _population)
            {
                if (s.Key == animal.Specie)
                    continue;

                if (s.Key.SocialIstinctToOtherSpecies != SocialIstinctToOtherSpecies.Aggressive)
                    continue;

                foreach (var a in s.Value)
                {
                    if (QuadraticDistance(a, animal) <= animal.Specie.SeeDistance)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        internal Animal WantsToAttack(Animal animal)
        {
            if (animal.Specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                return null;
            // In realtà ci saranno altri vincoli tipo "sono in calore, vicino ad una femmina e c'è un altro pretendente"

            foreach (var s in _population)
            {
                if (s.Key == animal.Specie)
                    continue;

                foreach (var a in s.Value)
                {
                    if (QuadraticDistance(a, animal) <= animal.Specie.SeeDistance)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        internal void Avvicinati(Animal who, Animal to)
        {
            int xMoveAmount = 0;
            int yMoveAmount = 0;

            // Da migliorare enormemente
            if (who.PosX <= to.PosX)
            {
                xMoveAmount = 1 * who.Specie.MovementSpeed;
            }
            else
            {
                xMoveAmount = -1 * who.Specie.MovementSpeed;
            }

            if (who.PosY <= to.PosY)
            {
                yMoveAmount = 1 * who.Specie.MovementSpeed;
            }
            else
            {
                yMoveAmount = -1 * who.Specie.MovementSpeed;
            }

            if (who.PosX + xMoveAmount >= _worldX || who.PosX + xMoveAmount <= 0)
                xMoveAmount = 0;

            if (who.PosY + yMoveAmount >= _worldY || who.PosX + yMoveAmount <= 0)
                yMoveAmount = 0;

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;
        }

        internal void Flee(Animal who, Animal from)
        {
            int xMoveAmount = 0;
            int yMoveAmount = 0;

            // Da migliorare enormemente
            if (who.PosX <= from.PosX)
            {
                xMoveAmount = -1 * who.Specie.MovementSpeed;
            }
            else
            {
                xMoveAmount = 1 * who.Specie.MovementSpeed;
            }

            if (who.PosY <= from.PosY)
            {
                yMoveAmount = -1 * who.Specie.MovementSpeed;
            }
            else
            {
                yMoveAmount = 1 * who.Specie.MovementSpeed;
            }

            if (who.PosX + xMoveAmount >= _worldX || who.PosX + xMoveAmount <= 0)
                xMoveAmount = 0;

            if (who.PosY + yMoveAmount >= _worldY || who.PosX + yMoveAmount <= 0)
                yMoveAmount = 0;

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;
        }

        internal void Attack(Animal attacker, Animal attacked)
        {
            var botta = attacker.AttackPower - attacked.DefensePower;
            attacked.Health -= botta;
            attacker.Health--;
        }

        internal Animal IsUnderAttack(Animal animal)
        {
            foreach (var s in _population)
            {
                // TODO poi è da togliere
                if (s.Key == animal.Specie)
                    continue;

                foreach (var a in s.Value)
                {
                    if (Math.Abs(a.PosX - animal.PosX) == 1 // E se sono uno sopra l'altro?
                        || Math.Abs(a.PosY - animal.PosY) == 1)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        internal void EvaluateAge(Animal animal)
        {
            animal.Age += 0.00001f;
        }

        internal bool IsDead(Animal animal)
        {
            return animal.Age >= animal.Specie.Lifetime || animal.Health <= 0;
        }

        internal Animal IsDistantFromHisSpecie(Animal animal)
        {
            var thisAnimalPopulation = _population[animal.Specie];
            foreach (var a in thisAnimalPopulation)
            {
                if (a == animal)
                    continue;

                if (QuadraticDistance(animal, a) <= animal.Specie.SeeDistance / 2)
                {
                    return a;
                }
            }

            return null;
        }
    }
}
