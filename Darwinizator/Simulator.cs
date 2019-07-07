using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Simulator
    {
        public int XDimension { get; }
        public int YDimension { get; }
        public int[,] World { get; set; }
        public Dictionary<Specie, List<Animal>> Population { get; set; }

        private readonly Evaluator _evaluator;

        public Simulator(int xDimension, int yDimension)
        {
            XDimension = xDimension;
            YDimension = yDimension;

            var specieGenerator = new SpecieGenerator();
            Population = specieGenerator.InitializePopulation(
                biodiversity: 5,
                populationPerSpecie: 10,
                xDimension: XDimension,
                yDimension: YDimension);

            _evaluator = new Evaluator(Population, XDimension, YDimension);
        }

        public void Update(TimeSpan elapsed)
        {
            foreach (var specie in Population)
            {
                foreach (var animal in specie.Value)
                {
                    // Come un gioco in scatola, ogni animale fa un turno 
                    // che comprende varie possiblità di azione

                    // * Movimento...
                    // ...se deve riprodursi
                    //if (_evaluator.NeedsToReproduce(animal))
                    //{
                    //    // Se è in calore si muove verso la prima femmina il linea d'aria
                    //    throw new NotImplementedException();
                    //}

                    // ...se ha paura e deve scappare
                    bool moved = false;

                    // TODO fare meglio... non far gestire il tipo di animale negli evaluate, ma gestiscilo qui

                    var enemyNearby = _evaluator.EnemyNearby(animal);
                    if (enemyNearby != null)
                    {
                        if (animal.Specie.SocialIstinctToOtherSpecies == SocialIstinctToOtherSpecies.Defensive)
                        {
                            // Un difensivo scappa
                            moved = _evaluator.Flee(animal, enemyNearby, elapsed);
                        }
                        else
                        {
                            // Un aggressivo si avvicina
                            moved = _evaluator.Avvicinati(animal, enemyNearby, elapsed);
                        }

                        // Se è vicino, attacca sempre
                    }

                    if (!moved)
                    {
                        // ...se è lontano dagli altri membri della sua specie
                        var allyNearby = _evaluator.IsDistantFromHisSpecie(animal);
                        if (allyNearby != null)
                        {
                            // Ci si avvicina
                            Console.WriteLine("Avvicinati");
                            moved = _evaluator.Avvicinati(animal, allyNearby, elapsed);
                        }
                    }

                    // ...se non si è mosso, movimento a caso
                    if (!moved)
                    {
                        _evaluator.RandomMove(animal, elapsed);
                    }

                    // TODO Se ha fame...
                    // Altrimenti non ha motivo di muoversi

                    // * Attacco
                    var predator = _evaluator.IsUnderAttack(animal);
                    if (predator != null)
                    {
                        Console.WriteLine("Preda attacca per difendersi");
                        _evaluator.Attack(animal, predator);
                    }
                    else
                    {
                        var preda = _evaluator.WantsToAttack(animal);
                        if (preda != null)
                        {
                            Console.WriteLine("Attacco la preda");
                            _evaluator.Attack(animal, preda);
                        }
                    }

                    _evaluator.EvaluateAge(animal);

                }

                var animaliMorti = specie.Value.RemoveAll(a => _evaluator.IsDead(a));
                if (animaliMorti > 0)
                {
                    Console.WriteLine($"Numero animali morti: {animaliMorti}");
                }
            }
        }

    }
}
