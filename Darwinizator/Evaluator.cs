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
        private static Random _random = new Random();

        public Evaluator(
            Dictionary<Specie, List<Animal>> population,
            int worldX,
            int worldY)
        {
            _population = population;
            _worldX = worldX;
            _worldY = worldY;
        }

        internal int Distance(Animal a, Animal b)
        {
            return Convert.ToInt32(
                Math.Sqrt(
                    Math.Pow(a.PosX - b.PosX, 2)
                    + Math.Pow(a.PosY - b.PosY, 2)));
        }

        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.Specie.Lifetime / 4;
        }

        internal Animal EnemyNearby(Animal animal)
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
                    if (Distance(a, animal) <= animal.Specie.SeeDistance)
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
                    if (Distance(a, animal) <= 2)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        internal bool Avvicinati(Animal who, Animal to, TimeSpan elapsed)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.Specie.MovementSpeed * elapsed.TotalSeconds);

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

            if (!PossibleMove(who.PosX + xMoveAmount, _worldX))
                return false;

            if (!PossibleMove(who.PosY + yMoveAmount, _worldY))
                return false;

            if (!PossibleNewPosition(
                who,
                who.PosX + xMoveAmount,
                who.PosY + yMoveAmount))
            {
                return false;
            }

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;

            return true;
        }

        internal bool RandomMove(Animal who, TimeSpan elapsed)
        {
            float xMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;
            float yMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;

            var multiplier = (float)(who.Specie.MovementSpeed * elapsed.TotalSeconds);

            xMoveAmount *= multiplier;
            yMoveAmount *= multiplier;

            if (!PossibleMove(who.PosX + xMoveAmount, _worldX))
                return false;

            if (!PossibleMove(who.PosY + yMoveAmount, _worldY))
                return false;

            if (!PossibleNewPosition(
                who,
                who.PosX + xMoveAmount,
                who.PosY + yMoveAmount))
            {
                return false;
            }

            who.PosX += xMoveAmount;
            who.PosY += yMoveAmount;

            return true;
        }

        internal bool Flee(Animal who, Animal from, TimeSpan elasped)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.Specie.MovementSpeed * elasped.TotalSeconds);

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

            if (!PossibleMove(who.PosX + xMoveAmount, _worldX))
                return false;

            if (!PossibleMove(who.PosY + yMoveAmount, _worldY))
                return false;

            if (!PossibleNewPosition(
                who,
                who.PosX + xMoveAmount,
                who.PosY + yMoveAmount))
            {
                return false;
            }

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

            // TODO Dubbia... però attaccare deve costare qualcosa
            attacker.Health--;
        }

        internal bool PossibleMove(float pos, int reference)
        {
            return pos < reference && pos >= 0;
        }

        internal bool PossibleNewPosition(Animal animal, float newPosX, float newPosY)
        {
            // TODO Ovviamente poi indicizzerò la griglia
            foreach (var s in _population.Values)
            {
                foreach (var a in s)
                {
                    if (a == animal)
                        continue;

                    if (a.PosX == newPosX && a.PosY == newPosY)
                        return false;
                }
            }

            return true;
        }

        internal Animal IsUnderAttack(Animal animal)
        {
            foreach (var s in _population)
            {
                // TODO poi è da togliere altrimenti non si riescono a fare le lotto per la riproduzione
                if (s.Key == animal.Specie || s.Key.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                    continue;

                foreach (var a in s.Value)
                {
                    if (Distance(animal, a) <= 2)
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

                if (Distance(animal, a) <= animal.Specie.SeeDistance)
                {
                    return a;
                }
            }

            return null;
        }
    }
}
