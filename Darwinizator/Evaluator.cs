using System;
using Darwinizator.Domain;

namespace Darwinizator
{
    public class Evaluator
    {
        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.Lifetime / 4;
        }

        internal bool NeedsToFlee(Animal animal)
        {
            if (animal.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
            {
                // TODO True se c'è un animale di un'altra specie aggressivo vicino.
                // Il "vicino" potrà essere un parametro ereditario di specie perché indica quanto vede lontano
            }

            return false;
        }

        internal bool NeedsToAttack(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal bool IsUnderAttack(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal bool WantsToAttack(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal void EvaluateAge(Animal animal)
        {
            animal.Lifetime -= 0.001f;
        }

        internal bool IsDead(Animal animal)
        {
            return animal.Lifetime <= 0;
        }

        internal bool IsDistantFromHisSpecie(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}
