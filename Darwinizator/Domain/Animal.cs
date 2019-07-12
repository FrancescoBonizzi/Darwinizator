namespace Darwinizator.Domain
{
    public class Animal : IWithMass
    {
        public string SpecieName { get; set; }
        public string Color { get; set; }
        public Diet Diet { get; set; }
        public Gender Gender { get; set; }
        public float Age { get; set; }
        public Animal Father { get; set; }
        public Animal Mother { get; set; }
        public int NextAgeCanReprouce { get; set; }
        public Mass Mass { get; set; }
        public string Name { get; set; }
        public float Energy { get; set; }

        // Survival/heritable charateristics
        public float Lifetime { get; set; }
        public int MovementSpeed { get; set; }
        public int SeeDistance { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public int IntervalBetweenReproductions { get; set; }
        public int MaximumHealth { get; set; }
        public int MaximumEnergy { get; set; }
        public int EnergyAmountToSearchForFood { get; set; }

        public bool IsHungry => Energy <= EnergyAmountToSearchForFood;
    }
}
