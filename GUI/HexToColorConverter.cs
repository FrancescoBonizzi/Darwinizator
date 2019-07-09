using Microsoft.Xna.Framework;
using System;

namespace GUI
{
    public static class HexToColorConverter
    {
        public static Color ConvertToXnaColor(this string hexString)
        {
            int val = Convert.ToInt32(hexString, 16);
            var bytes = BitConverter.GetBytes(val);
            return new Color(bytes[2], bytes[1], bytes[0]);
        }
    }
}
