using Darwinizator;
using Darwinizator.Domain;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class GetEvolution : Form
    {
        private readonly Simulator _simulator;
        private readonly DarwinatorRenderer _darwinatorRenderer;

        private const int _maxGenerationsToDraw = 5;

        private int _carnivorousNumberOfDeathsPreviousRefresh = 0;
        private int _herbivoreNumberOfDeathsSincePreviousRefresh = 0;
        
        private Dictionary<string, ChartValues<ObservableValue>> BestHerbivoreEvolution { get; }
        private Dictionary<string, ChartValues<ObservableValue>> BestCarnivorousEvolution { get; }

        public GetEvolution(Simulator simulator, DarwinatorRenderer darwinatorRenderer)
        {
            InitializeComponent();
            _simulator = simulator;
            _simulator.DataRefresh += _simulator_DataRefresh;

            _darwinatorRenderer = darwinatorRenderer;

            flagDebug.Checked = _darwinatorRenderer.DebugMode;
            flagRendering.Checked = _darwinatorRenderer.Rendering;
            flagPause.Checked = _darwinatorRenderer.Paused;

            BestHerbivoreEvolution = new Dictionary<string, ChartValues<ObservableValue>>();
            BestCarnivorousEvolution = new Dictionary<string, ChartValues<ObservableValue>>();

            chartHerbivoreEvolution.Series = GetEmptySeries(BestHerbivoreEvolution);
            chartCarnivorousEvolution.Series = GetEmptySeries(BestCarnivorousEvolution);
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

            Animal carnivorousWithLongestGeneration = null;
            Animal herbivoreWithLongestGeneration = null;

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

            UpdateEvolution(herbivoreWithLongestGeneration);
            UpdateEvolution(carnivorousWithLongestGeneration);
        }

        private IEnumerable<ObservableValue> GetEmptyValues()
        {
            return Enumerable.Range(0, _maxGenerationsToDraw).Select(generation => new ObservableValue(0));
        }

        private SeriesCollection GetEmptySeries(Dictionary<string, ChartValues<ObservableValue>> bindedValues)
        {
            bindedValues.Add(nameof(Animal.MovementSpeed), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.AttackPower), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.DefensePower), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.IntervalBetweenReproductions), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.MaximumEnergy), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.EnergyGainForEating), new ChartValues<ObservableValue>(GetEmptyValues()));
            bindedValues.Add(nameof(Animal.EnergyAmountToSearchForFood), new ChartValues<ObservableValue>(GetEmptyValues()));
          //  bindedValues.Add(nameof(Animal.Lifetime), new ChartValues<ObservableValue>(GetEmptyValues()));
          //  bindedValues.Add(nameof(Animal.SeeDistance), new ChartValues<ObservableValue>(GetEmptyValues()));

            var series = new SeriesCollection();

            foreach (var property in bindedValues)
            {
                series.Add(new LineSeries()
                {
                    Title = property.Key,
                    Values = property.Value
                });
            }

            return series;
        }

        private void UpdateEvolution(Animal animal)
        {
            if (animal == null)
                return;

            var referenceBindedList = animal.Diet == Diet.Carnivorous ? BestCarnivorousEvolution : BestHerbivoreEvolution;

            var currentAnimal = animal;
            int iterationNumber = 0;
            while (currentAnimal != null && iterationNumber < _maxGenerationsToDraw)
            {
                //referenceBindedList[nameof(animal.Lifetime)][iterationNumber].Value = currentAnimal.Lifetime;
                referenceBindedList[nameof(animal.MovementSpeed)][iterationNumber].Value = currentAnimal.MovementSpeed;
                //referenceBindedList[nameof(animal.SeeDistance)][iterationNumber].Value = currentAnimal.SeeDistance;
                referenceBindedList[nameof(animal.AttackPower)][iterationNumber].Value = currentAnimal.AttackPower;
                referenceBindedList[nameof(animal.DefensePower)][iterationNumber].Value = currentAnimal.DefensePower;
                referenceBindedList[nameof(animal.IntervalBetweenReproductions)][iterationNumber].Value = currentAnimal.IntervalBetweenReproductions;
                referenceBindedList[nameof(animal.MaximumEnergy)][iterationNumber].Value = currentAnimal.MaximumEnergy;
                referenceBindedList[nameof(animal.EnergyGainForEating)][iterationNumber].Value = currentAnimal.EnergyGainForEating;
                referenceBindedList[nameof(animal.EnergyAmountToSearchForFood)][iterationNumber].Value = currentAnimal.EnergyAmountToSearchForFood;

                ++iterationNumber;
                currentAnimal = currentAnimal.Father;
            }
        }

        private void FlagPause_CheckedChanged(object sender, EventArgs e)
        {
            _darwinatorRenderer.Paused = (sender as CheckBox).Checked;
        }

        private void FlagDebug_CheckedChanged(object sender, EventArgs e)
        {
            _darwinatorRenderer.DebugMode = (sender as CheckBox).Checked;
        }

        private void FlagRendering_CheckedChanged(object sender, EventArgs e)
        {
            _darwinatorRenderer.Rendering = (sender as CheckBox).Checked;
        }
    }
}
