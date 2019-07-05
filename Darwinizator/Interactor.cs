using Darwinizator.Domain;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Interactor
    {
        public int [,] World { get; set; }
        public List<Animal> Population { get; set; }

        public Interactor()
        {
            var specieGenerator = new SpecieGenerator();
            Population = specieGenerator.InitializePopulation(4, 10);
        }

        private void Update()
        {
            foreach(var animal in Population)
            {
                
            }
        }

        public void Live()
        {
            while (true)
            {
                Update();
            }
        }

    }
}
