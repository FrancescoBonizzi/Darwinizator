using Darwinizator.Domain;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Simulator
    {
        private int _xDimension = 5000;
        private int _yDimension = 5000;
        public int [,] World { get; set; }
        public Dictionary<Specie, List<Animal>> Population { get; set; }

        private Evaluator _evaluator;

        public Simulator()
        {
            var specieGenerator = new SpecieGenerator();
            Population = specieGenerator.InitializePopulation(
                biodiversity: 4, 
                populationPerSpecie: 10,
                xDimension: _xDimension,
                yDimension: _yDimension);

            _evaluator = new Evaluator();
        }

        public void Update()
        {
            foreach(var specie in Population)
            {
                foreach(var animal in specie.Value)
                {
                    // Come un gioco in scatola, ogni animale fa un turno 
                    // che comprende varie possiblità di azione
                    
                    // * Movimento...
                    // ...se deve riprodursi
                    if (_evaluator.NeedsToReproduce(animal))
                    {
                        // Se è in calore si muove verso la prima femmina il linea d'aria
                    }
                    // ...se ha paura e deve scappare
                    else if (_evaluator.NeedsToFlee(animal))
                    {

                    }
                    // ...se ha paura e deve attaccare
                    else if (_evaluator.NeedsToAttack(animal))
                    {

                    }
                    // ...se è lontano dagli altri membri della sua specie
                    else if (_evaluator.IsDistantFromHisSpecie(animal))
                    {
                        // Ci si avvicina
                    }

                    // TODO Se ha fame...
                    // Altrimenti non ha motivo di muoversi

                    // * Attacco
                    if (_evaluator.IsUnderAttack(animal))
                    {
                        // Attacca
                        // animal.Attack();
                    }
                    else if (_evaluator.WantsToAttack(animal))
                    {
                        // Attacca
                        // animal.Attack();
                    }

                    _evaluator.EvaluateAge(animal);

                    if (_evaluator.IsDead(animal))
                    {
                        // animal.Die();
                    }
                }
            }
        }

    }
}
