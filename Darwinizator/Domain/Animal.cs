namespace Darwinizator.Domain
{
    public class Animal
    {
        public string SpecieName { get; set; }
        public string Color { get; set; }
        public SocialIstinctToOtherSpecies SocialIstinctToOtherSpecies { get; set; }
        public Gender Gender { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float Age { get; set; }
        public Animal Father { get; set; }
        public Animal Mother { get; set; }
        public int NextYearCanReprouce { get; set; }

        // Survival/heritable charateristics
        public float Lifetime { get; set; }
        public int MovementSpeed { get; set; }
        public int SeeDistance { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public int IntervalBetweenReproductions { get; set; }
    }
}
