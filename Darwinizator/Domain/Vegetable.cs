namespace Darwinizator.Domain
{
    public class Vegetable : IWithMass
    {
        public Mass Mass { get; set; }
        public bool IsEaten { get; set; }
    }
}
