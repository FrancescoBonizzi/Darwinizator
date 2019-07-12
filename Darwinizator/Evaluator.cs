using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Evaluator
    {
        private static readonly Random _random = new Random();
        private readonly Generator _generator;
        private readonly Dictionary<string, List<Animal>> _population;
        private readonly List<Animal>[,] _grid;
        private readonly List<Vegetable> _vegetables;
        private readonly int _worldX;
        private readonly int _worldY;

        private const float _positioningThreshold = 2f;

        public Evaluator(
            Generator generator,
            Dictionary<string, List<Animal>> population,
            List<Vegetable> vegetables,
            int worldX,
            int worldY)
        {
            _generator = generator;
            _vegetables = vegetables;

            _population = population;
            _worldX = worldX;
            _worldY = worldY;

            _grid = new List<Animal>[_worldX, _worldY];

            for (int x = 0; x < _worldX; ++x)
            {
                for (int y = 0; y < _worldY; ++y)
                {
                    _grid[x, y] = new List<Animal>();
                }
            }

            // Grid cell division is approximated to integer number of PosX and PosY
            foreach (var specie in population)
            {
                foreach (var animal in specie.Value)
                {
                    _grid[(int)animal.Mass.PosX, (int)animal.Mass.PosY].Add(animal);
                }
            }

        }

        internal float Distance(IWithMass a, IWithMass b)
        {
            return Distance(a.Mass.PosX, a.Mass.PosY, b.Mass.PosX, b.Mass.PosY);
        }

        internal float Distance(float aPosX, float aPosY, float bPosX, float bPosY)
        {
            return (float)(Math.Pow(aPosX - bPosX, 2) + Math.Pow(aPosY - bPosY, 2));
        }

        internal Animal FirstEnemyInLineOfSight(Animal animal)
        {
            foreach (var specie in _population)
            {
                // An animal of the same specie isn't an enemy
                if (specie.Key == animal.SpecieName)
                    continue;

                foreach (var otherAnimal in specie.Value)
                {
                    // A defensive enemy isn't an enemy if you are defensive
                    if (animal.Diet == Diet.Herbivore
                        && otherAnimal.Diet == Diet.Herbivore)
                    {
                        continue;
                    }

                    if (animal.Diet == Diet.Carnivorous
                        && otherAnimal.Diet == Diet.Carnivorous)
                    {
                        continue;
                    }

                    if (Distance(otherAnimal, animal) <= animal.SeeDistance)
                    {
                        return otherAnimal;
                    }
                }
            }

            return null;
        }

        internal Vegetable FirstVegetableInLineOfSight(Animal animal)
        {
            foreach(var veg in _vegetables)
            {
                if (veg.IsEaten)
                    continue;

                if (Distance(animal, veg) <= animal.SeeDistance)
                {
                    return veg;
                }
            }

            return null;
        }

        internal bool Approach(Animal who, IWithMass to, TimeSpan elapsed)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.MovementSpeed * elapsed.TotalSeconds);

            if (who.Mass.PosX <= to.Mass.PosX)
            {
                xMoveAmount = 1 * multiplier;
            }
            else
            {
                xMoveAmount = -1 * multiplier;
            }

            if (who.Mass.PosY <= to.Mass.PosY)
            {
                yMoveAmount = 1 * multiplier;
            }
            else
            {
                yMoveAmount = -1 * multiplier;
            }

            return SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal bool SetNewPosition(
            Animal who,
            float xMoveAmount,
            float yMoveAmount)
        {
            var newPosX = ClampToWorldBounds(who.Mass.PosX + xMoveAmount, _worldX - who.Mass.Width);
            var newPosY = ClampToWorldBounds(who.Mass.PosY + yMoveAmount, _worldY - who.Mass.Height);

            if (CouldGoOnAnotherAnimal(
               who,
               newPosX,
               newPosY))
            {
                return false;
            }

            _grid[(int)who.Mass.PosX, (int)who.Mass.PosY].Remove(who);

            who.Mass.PosX = newPosX;
            who.Mass.PosY = newPosY;

            _grid[(int)who.Mass.PosX, (int)who.Mass.PosY].Add(who);

            return true;
        }

        internal Animal Copulate(Animal father, Animal mother)
        {
            if (father.SpecieName != mother.SpecieName)
                throw new Exception("Cannot copulate between different species");

            var son = _generator.GenerateAnimal(father: father, mother: mother);
            father.NextAgeCanReprouce += father.IntervalBetweenReproductions;
            mother.NextAgeCanReprouce += mother.IntervalBetweenReproductions;

            _grid[(int)son.Mass.PosX, (int)son.Mass.PosY].Add(son);

            return son;
        }

        internal void EvaluateMoveEnergyCost(Animal animal)
        {
            // TODO Evaluate according to movement speed and weight?
            animal.Energy -= StartingValues.EnergyCostForMoving;
        }

        internal void EvaluateAttackEnergyCost(Animal animal)
        {
            // TODO Evaluate according to weight or something else?
            animal.Energy -= StartingValues.EnergyCostForAttacking;
        }

        internal void Kill(Animal deadAnimal)
        {
            _grid[(int)deadAnimal.Mass.PosX, (int)deadAnimal.Mass.PosY].Remove(deadAnimal);
        }

        internal bool NeedsToReproduce(Animal animal)
        {
            return animal.Age >= animal.NextAgeCanReprouce;
        }

        internal bool RandomMove(Animal who, TimeSpan elapsed)
        {
            float xMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;
            float yMoveAmount = _random.NextDouble() >= 0.5 ? 1 : -1;

            var multiplier = (float)(who.MovementSpeed * elapsed.TotalSeconds);

            xMoveAmount *= multiplier;
            yMoveAmount *= multiplier;

            return SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal float ClampToWorldBounds(float value, float referenceValue)
        {
            if (value < 0)
                return 0;

            if (value > referenceValue)
                return referenceValue;

            return value;
        }

        internal bool Flee(Animal who, Animal from, TimeSpan elasped)
        {
            float xMoveAmount = 0;
            float yMoveAmount = 0;

            var multiplier = (float)(who.MovementSpeed * elasped.TotalSeconds);

            // Da migliorare enormemente
            if (who.Mass.PosX <= from.Mass.PosX)
            {
                xMoveAmount = -1 * multiplier;
            }
            else
            {
                xMoveAmount = 1 * multiplier;
            }

            if (who.Mass.PosY <= from.Mass.PosY)
            {
                yMoveAmount = -1 * multiplier;
            }
            else
            {
                yMoveAmount = 1 * multiplier;
            }

            return SetNewPosition(who, xMoveAmount, yMoveAmount);
        }

        internal void Attack(Animal attacker, Animal attacked)
        {
            var smash = (attacker.AttackPower + _random.Next(1, 3))
                - (attacked.DefensePower + _random.Next(1, 3));

            if (smash < 0)
            {
                // If defense is bigger, attaccker gets hit
                attacker.Energy += smash;
            }
            else if (smash > 0)
            {
                // If attack is bigger, attaccked gets hit
                attacked.Energy -= smash;
            }

            EvaluateAttackEnergyCost(attacker);
            EvaluateAttackEnergyCost(attacked);
        }

        internal void Eat(Animal animal, Vegetable vegetable)
        {
            animal.Energy += StartingValues.EnergyGainForEatingPlants;
            if (animal.Energy > animal.MaximumEnergy)
                animal.Energy = animal.MaximumEnergy;

            vegetable.IsEaten = true;
        }

        internal void Eat(Animal animal, Animal anotherAnimal)
        {
            animal.Energy += StartingValues.EnergyGainForEatingAnimals;
            if (animal.Energy > animal.MaximumEnergy)
                animal.Energy = animal.MaximumEnergy;

            // AnotherAnimal disappear
            anotherAnimal.Energy = -10;
        }

        internal bool CouldGoOnAnotherAnimal(Animal animal, float newPosX, float newPosY)
        {
            var newMass = new Mass()
            {
                PosX = newPosX,
                PosY = newPosY,
                Width = animal.Mass.Width,
                Height = animal.Mass.Height
            };

            foreach (var animalInCell in _grid[(int)newPosX, (int)newPosY])
            {
                if (animalInCell == animal)
                    continue;

                if (newMass.Intersects(animalInCell.Mass, _positioningThreshold))
                    return true;
            }

            return false;
        }

        internal bool IsEnoughCloseToInteract(Animal who, IWithMass withWho)
        {
            return who.Mass.Intersects(withWho.Mass);
        }

        internal void EvaluateAge(IWithAge lifeBeing)
        {
            lifeBeing.Age += 0.01f;
        }

        internal bool IsDead(Animal animal)
        {
            return animal.Age >= animal.Lifetime
                || animal.Energy <= 0;
        }

        internal Animal AllayInLineOfSight(Animal animal, bool? searchForReproduction = null)
        {
            var thisAnimalPopulation = _population[animal.SpecieName];
            foreach (var a in thisAnimalPopulation)
            {
                if (a == animal)
                    continue;

                if (searchForReproduction != null && a.Gender == animal.Gender)
                    continue;

                if (searchForReproduction != null && !NeedsToReproduce(a))
                    continue;

                if (Distance(animal, a) <= animal.SeeDistance)
                {
                    return a;
                }
            }

            return null;
        }
    }
}
