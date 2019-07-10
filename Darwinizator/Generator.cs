﻿using Darwinizator.Domain;
using System;
using System.Collections.Generic;

namespace Darwinizator
{
    public class Generator
    {
        private static readonly Random _random = new Random();

        public Dictionary<string, List<Animal>> InitializePopulation(
            int populationPerSpecie,
            int worldXSize,
            int worldYSize)
        {
            var population = new Dictionary<string, List<Animal>>();

            {
                const SocialIstinctToOtherSpecies socialInstinct = SocialIstinctToOtherSpecies.Aggressive;
                var specieName = socialInstinct.ToString();
                var animals = new List<Animal>();
                for (int i = 0; i < populationPerSpecie; ++i)
                {
                    animals.Add(GenerateAnimal(
                        specieName: specieName,
                        socialIstinctToOtherSpecies: socialInstinct,
                        maleColor: "ff0000",
                        femaleColor: "ff0066",
                        _random.Next(0, worldXSize),
                        _random.Next(0, worldYSize)));
                }

                population.Add(specieName, animals);
            }

            {
                const SocialIstinctToOtherSpecies socialInstinct = SocialIstinctToOtherSpecies.Defensive;
                var specieName = socialInstinct.ToString();
                var animals = new List<Animal>();
                for (int i = 0; i < populationPerSpecie; ++i)
                {
                    animals.Add(GenerateAnimal(
                        specieName: specieName,
                        socialIstinctToOtherSpecies: socialInstinct,
                        maleColor: "51ff00",
                        femaleColor: "00fff2",
                        _random.Next(0, worldXSize),
                        _random.Next(0, worldYSize)));
                }

                population.Add(specieName, animals);
            }

            return population;
        }

        public static Animal GenerateAnimal(
            Animal father,
            Animal mother)
        {
            var animal = GenerateAnimal(
                father.SpecieName,
                father.SocialIstinctToOtherSpecies,
                father.Color,
                mother.Color,
                (int)mother.Mass.PosX,
                (int)mother.Mass.PosY);

            animal.Father = father;
            animal.Mother = mother;

            // TODO Come calcolo il numero di generazione? Sommo padre, o madre, o entrambi +1?
            animal.IntervalBetweenReproductions = mother.IntervalBetweenReproductions;

            // TODO randomizza caratteristiche di specie con una funzione che parte dai genitori

            return animal;
        }

        public static Animal GenerateAnimal(
            string specieName,
            SocialIstinctToOtherSpecies socialIstinctToOtherSpecies,
            string maleColor,
            string femaleColor,
            int posX,
            int posY)
        {
            var gender = _random.NextDouble() >= 0.5 ? Gender.Male : Gender.Female;
            var color = gender == Gender.Male ? maleColor : femaleColor;

            return new Animal()
            {
                SpecieName = specieName,
                Color = color,
                SocialIstinctToOtherSpecies = socialIstinctToOtherSpecies,
                Gender = gender,
                Age = 0,
                Father = null,
                Mother = null,
                NextYearCanReprouce = 5,

                Lifetime = 20,
                MovementSpeed = 100,
                SeeDistance = 100,
                Health = 20,
                AttackPower = 5,
                DefensePower = 5,
                IntervalBetweenReproductions = 5,
                Mass = new Mass()
                {
                    PosX = posX,
                    PosY = posY,
                    Weight = 6,
                    Width = 6,
                    Height = 6
                }
            };
        }
    }
}