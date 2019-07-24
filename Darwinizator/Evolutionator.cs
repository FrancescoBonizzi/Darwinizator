using Darwinizator.Domain;
using System;

namespace Darwinizator
{
    public class Evolutionator
    {
        private static readonly Random _random = new Random();
        private const double _min = -1;
        private const double _max = 1;

        public void RandomizeTraits(Animal father, Animal mother, Animal son)
        {
            son.Lifetime = Randomize(father.Lifetime, mother.Lifetime);
            son.MovementSpeed = Randomize(father.MovementSpeed, mother.MovementSpeed);
            son.SeeDistance = Randomize(father.SeeDistance, mother.SeeDistance);
            son.AttackPower = Randomize(father.AttackPower, mother.AttackPower);
            son.DefensePower = Randomize(father.DefensePower, mother.DefensePower);
            son.IntervalBetweenReproductions = Randomize(father.IntervalBetweenReproductions, mother.IntervalBetweenReproductions);
            son.MaximumEnergy = Randomize(father.MaximumEnergy, mother.MaximumEnergy);
            son.EnergyAmountToSearchForFood = Randomize(father.EnergyAmountToSearchForFood, mother.EnergyAmountToSearchForFood);
            son.EnergyGainForEating = Randomize(father.EnergyGainForEating, mother.EnergyGainForEating);
            son.NextAgeCanReproduce = son.IntervalBetweenReproductions;
        }

        private float Randomize(float a, float b)
        {
            double randomValueBetween0And1 = _min + (_random.NextDouble() * (_max - _min));
            float average = (a + b) / 2F;
            return average + (float)randomValueBetween0And1;
        }
    }
}
