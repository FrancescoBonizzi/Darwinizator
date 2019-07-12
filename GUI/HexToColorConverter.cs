using Darwinizator.Domain;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GUI
{
    public class HexToColorConverter
    {
        private readonly Dictionary<string, Color> _conversionsCache;

        public HexToColorConverter()
        {
            _conversionsCache = new Dictionary<string, Color>();
        }

        /// <summary>
        /// Dying animals fade out
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public Color CalculateColor(Animal animal)
        {
            var startingColor = ConvertToXnaColor(animal.Color);

            var alpha = FbonizziMonoGame.Numbers.MapValueFromIntervalToInterval(
                animal.Energy, 0, animal.MaximumEnergy,
                0f, 1f);

            return startingColor * alpha;
        }

        /// <summary>
        /// New vegetables fade in
        /// </summary>
        /// <param name="vegetable"></param>
        /// <returns></returns>
        public Color CalculateColor(Vegetable vegetable)
        {
            var startingColor = ConvertToXnaColor(vegetable.Color);

            float alpha = 0f;
            if (vegetable.Age <= 0.5f)
            {
                alpha = FbonizziMonoGame.Numbers.MapValueFromIntervalToInterval(
                    vegetable.Age, 0f, 0.5f,
                    0f, 1f);
            }
            else
            {
                alpha = 1f;
            }

            return startingColor * alpha;
        }

        public Color ConvertToXnaColor(string hexString)
        {
            if (_conversionsCache.ContainsKey(hexString))
                return _conversionsCache[hexString];

            int hexAsInt32 = Convert.ToInt32(hexString, 16);
            var int32bytes = BitConverter.GetBytes(hexAsInt32);
            var xnaColor = new Color(int32bytes[2], int32bytes[1], int32bytes[0]);

            _conversionsCache.Add(hexString, xnaColor);

            return xnaColor;
        }
    }
}
