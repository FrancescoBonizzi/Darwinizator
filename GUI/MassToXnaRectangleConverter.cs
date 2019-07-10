using Darwinizator.Domain;
using Microsoft.Xna.Framework;

namespace GUI
{
    public static class MassToXnaRectangleConverter
    {
        public static Rectangle ToXnaRectangle(this Mass mass)
            => new Rectangle(
                (int)mass.PosX,
                (int)mass.PosY,
                mass.Width,
                mass.Height);
    }
}
