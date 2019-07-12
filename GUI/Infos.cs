using Darwinizator;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Infos : Form
    {
        private readonly Simulator _simulator;

        public Infos(Simulator simulator)
        {
            InitializeComponent();
            _simulator = simulator;
        }

        private void TimerRefresh_Tick(object sender, System.EventArgs e)
        {
            txtGeneralInfos.Clear();
            txtCarnivorous.Clear();
            txtHerbivore.Clear();

            int carnivorousAlive = 0;
            int herbivorousAlive = 0;
            int carnivorousHungry = 0;
            int herbivorousHungry = 0;

            foreach (var specie in _simulator.Population)
            {
                foreach (var animal in specie.Value)
                {
                    if (animal.Diet == Darwinizator.Domain.Diet.Carnivorous)
                    {
                        if (animal.IsHungry)
                            carnivorousHungry++;
                        carnivorousAlive++;
                    }
                    else if (animal.Diet == Darwinizator.Domain.Diet.Herbivore)
                    {
                        if (animal.IsHungry)
                            herbivorousHungry++;
                        herbivorousAlive++;
                    }
                }
            }

            txtGeneralInfos.AppendText($"Total population: {carnivorousAlive + herbivorousAlive}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total vegetables: {_simulator.Vegetables.Count}");
            txtGeneralInfos.AppendText(Environment.NewLine + $"Total deads: {_simulator.DeadCarnivorousNumber + _simulator.DeadHerbivoreNumber}");

            txtCarnivorous.AppendText($"Total carnivorous: {carnivorousAlive}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Total carnivorous hungry: {carnivorousHungry}");
            txtCarnivorous.AppendText(Environment.NewLine + $"Total deads: {_simulator.DeadCarnivorousNumber}");

            txtHerbivore.AppendText($"Total herbivorous: {herbivorousAlive}");
            txtHerbivore.AppendText(Environment.NewLine + $"Total herbivorous hungry: {herbivorousHungry}");
            txtHerbivore.AppendText(Environment.NewLine + $"Total deads: {_simulator.DeadHerbivoreNumber}");
        }
    }
}
