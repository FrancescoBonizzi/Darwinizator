using System;

namespace Darwinizator
{
    public static class StartingValues
    {
        public const int PopulationPerSpecie = 25;
        public const int NumberOfVegetables = 80;
        public const int EnergyGainForEatingAnimals = 25;
        public const int EnergyGainForEatingPlants = 15;
        public const float EnergyCostForMoving = 0.05f;
        public const float EnergyCostForAttacking = 0.08f;
        public const int IntervalBetweenReproducions = 5;
        public const int EnergyAmountToStartSearchingForFood = 50;
        public const int MaximumEnergy = 100;
        public const int Lifetime = 20;
        public const int MovementSpeed = 100;
        public const int SeeDistance = 10000;
        public const int AttackPower = 5;
        public const int DefensePower = 5;
        public static readonly TimeSpan IntervalForVegetablesGeneration = TimeSpan.FromSeconds(15);
    }
}
