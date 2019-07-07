using System.Collections.Generic;

namespace Darwinizator.Domain
{
    public class Specie
    {
        public string Name { get; set; }
        public float Lifetime { get; set; }
        public int MovementSpeed { get; set; }
        public int SeeDistance { get; set; }
        public SocialIstinctToOtherSpecies SocialIstinctToOtherSpecies { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Specie specie &&
                   Name == specie.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(Specie left, Specie right)
        {
            return EqualityComparer<Specie>.Default.Equals(left, right);
        }

        public static bool operator !=(Specie left, Specie right)
        {
            return !(left == right);
        }
    }
}

// public SocialIstinctToSameSpecies SocialIstinctToSameSpecies { get; set; }
