namespace Darwinizator.Domain
{
    public class Vegetable : IWithMass, IWithAge
    {
        public Mass Mass { get; set; }
        public bool IsEaten { get; set; }
        public float Age { get; set; }
        public string Color { get; set; }
    }
}
