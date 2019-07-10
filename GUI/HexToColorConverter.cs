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
