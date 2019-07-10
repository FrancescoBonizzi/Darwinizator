namespace Darwinizator.Domain
{
    public class Mass
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // TODO posso usarlo per stabilire un costo alle azioni
        public int Weight { get; set; }

        public float Left => PosX;
        public float Right => PosX + Width;
        public float Top => PosY;
        public float Bottom => PosY + Height;

        public bool Intersects(
            Mass otherMass,
            float tolerance = 0f)
        {
            return otherMass.Left + tolerance < Right - tolerance
                && Left + tolerance < otherMass.Right - tolerance
                && otherMass.Top + tolerance < Bottom - tolerance
                && Top + tolerance < otherMass.Bottom - tolerance;
        }
    }
}
