namespace SmellyMarsRover
{
    internal record Direction(string Value)
    {
        public bool IsFacingWest()
        {
            return Value.Equals("W");
        }

        public bool IsFacingSouth()
        {
            return Value.Equals("S");
        }

        public bool IsFacingNorth()
        {
            return Value.Equals("N");
        }

        public static Direction Create(string direction)
        {
            return new Direction(direction);
        }
    }
}