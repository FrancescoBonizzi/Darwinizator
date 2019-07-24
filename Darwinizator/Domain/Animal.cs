namespace Darwinizator.Domain
{
    public class Animal : IWithMass, IWithAge
    {
        public string SpecieName { get; set; }
        public string Color { get; set; }
        public Diet Diet { get; set; }
        public Gender Gender { get; set; }
        public float Age { get; set; }
        public Animal Father { get; set; }
        public Animal Mother { get; set; }
        public float NextAgeCanReproduce { get; set; }
        public Mass Mass { get; set; }
        public string Name { get; set; }
        public float Energy { get; set; }
        public int GenerationAge { get; set; }

        // Traits/survival/heritable charateristics
        public float Lifetime { get; set; }
        public float MovementSpeed { get; set; }
        public float SeeDistance { get; set; }
        public float AttackPower { get; set; }
        public float DefensePower { get; set; }
        public float IntervalBetweenReproductions { get; set; }
        public float MaximumEnergy { get; set; }
        public float EnergyAmountToSearchForFood { get; set; }
        public float EnergyGainForEating { get; set; }

        public bool IsHungry => Energy <= EnergyAmountToSearchForFood;
    }
}
