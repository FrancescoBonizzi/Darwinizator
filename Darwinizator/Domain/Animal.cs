namespace Darwinizator.Domain
{
    public class Animal
    {
        public string Specie { get; set; }
        public int Lifetime { get; set; }
        public Gender Gender { get; set; }
        public int MovementSpeed { get; set; }
        public SocialIstinctToOtherSpecies SocialIstinctToOtherSpecies { get; set; }
        public SocialIstinctToSameSpecies SocialIstinctToSameSpecies { get; set; }

        public int Health { get; set; }

        public int AttackPower { get; set; }
        public int DefensePower { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Hungriness { get; set; }

        public bool NeedsToReproduce()
        {
            return false;
        }

        
    }
}
