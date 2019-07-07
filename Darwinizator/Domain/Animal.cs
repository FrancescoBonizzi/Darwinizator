namespace Darwinizator.Domain
{
    public class Animal
    {
        public Specie Specie { get; set; }
     
        public float Age { get; set; }
        public Gender Gender { get; set; }
       
        public int Health { get; set; }

        public int AttackPower { get; set; }
        public int DefensePower { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        // TODO intuitivamente gli animali devono avere una sorta di override 
        // rispetto ai parametri di base, perché ogni individuo ha una sua specificità 
        // che deve poi essere trasmessa: es uno casualmente ha un'attacco molto più alto
        // e questo ha più probabilità di sopravvivenza

    }
}
