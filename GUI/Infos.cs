using Darwinizator;
using Darwinizator.Domain;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Infos : Form
    {
        private readonly Simulator _simulator;
        private int _carnivorousNumberOfDeathsPreviousRefresh = 0;
        private int _herbivoreNumberOfDeathsSincePreviousRefresh = 0;

        public Infos(Simulator simulator)
        {
            InitializeComponent();
            _simulator = simulator;
            _simulator.DataRefresh += _simulator_DataRefresh;
        }

        private void _simulator_DataRefresh(object sender, EventArgs e)
        {
            txtGeneralInfos.Clear();
            txtCarnivorous.Clear();
            txtHerbivore.Clear();

            int carnivorousAlive = 0;
            int herbivoreAlive = 0;
            int carnivorousHungry = 0;
            int herbivoreHungry = 0;
            int carnivorousLongestGeneration = 0;
            int herbivoreLongestGeneration = 0;

            Animal carnivorousWithLongestGeneration;
            Animal herbivoreWithLongestGeneration;

            foreach (var specie in _simulator.Population)
            {
                foreach (var animal in specie.Value)
                {
                    if (animal.Diet == Diet.Carnivorous)
                    {
                        if (animal.IsHungry)
                            carnivorousHungry++;
                        carnivorousAlive++;

                        if (animal.GenerationAge > carnivorousLongestGeneration)
                        {
                            carnivorousLongestGeneration = animal.GenerationAge;
                            carnivorousWithLongestGeneration = animal;
                        }
                    }
                    else if (animal.Diet == Diet.Herbivore)
                    {
                        if (animal.IsHungry)
                            herbivoreHungry++;
                        herbivoreAlive++;

                        if (animal.GenerationAge > herbivoreLongestGeneration)
                        {
                            herbivoreLongestGeneration = animal.GenerationAge;
                            herbivoreWithLongestGeneration = animal;
                        }
                    }
                }
            }

            int carnivorousNumberOfDeathsSinceLastRefresh = _simulator.TotalDeadCarnivorousNumber - _carnivorousNumberOfDeathsPreviousRefresh;
            int herbivoreNumberOfDeathsSinceLastRefresh = _simulator.TotalDeadHerbivoreNumber - _herbivoreNumberOfDeathsSincePreviousRefresh;

            _carnivorousNumberOfDeathsPreviousRefresh = _simulator.TotalDeadCarnivorousNumber;
            _herbivoreNumberOfDeathsSincePreviousRefresh = _simulator.TotalDeadHerbivoreNumber;

            txtGeneralInfos.AppendText($"Total ever lived animals: {_simulator.TotalBornHerbivoreNumber + _simulator.TotalBornCarnivorousNumber}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total alive population: {carnivorousAlive + herbivoreAlive}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total vegetables: {_simulator.Vegetables.Count}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total deads: {_simulator.TotalDeadCarnivorousNumber + _simulator.TotalDeadHerbivoreNumber}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total deads/minute: {carnivorousNumberOfDeathsSinceLastRefresh + herbivoreNumberOfDeathsSinceLastRefresh}");

            txtCarnivorous.AppendText($"Ever lived: {_simulator.TotalBornCarnivorousNumber}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Alive: {carnivorousAlive}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Hungry: {carnivorousHungry}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Deads: {_simulator.TotalDeadCarnivorousNumber}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Longest generation: {carnivorousLongestGeneration}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Deads/minute: {carnivorousNumberOfDeathsSinceLastRefresh}");

            txtHerbivore.AppendText($"Ever lived: {_simulator.TotalBornHerbivoreNumber}");
            txtHerbivore.AppendText(Environment.NewLine + $"Alive: {herbivoreAlive}");
            txtHerbivore.AppendText(Environment.NewLine + $"Hungry: {herbivoreHungry}");
            txtHerbivore.AppendText(Environment.NewLine + $"Deads: {_simulator.TotalDeadHerbivoreNumber}");
            txtHerbivore.AppendText(Environment.NewLine + $"Longest generation: {herbivoreLongestGeneration}");
            txtHerbivore.AppendText(Environment.NewLine + $"Deads/minute: {herbivoreNumberOfDeathsSinceLastRefresh}");

        }
    }
}
