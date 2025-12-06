namespace SmellyMarsRover;

internal static class DirectionMapper
{
    private const string North = "N";
    private const string South = "S";
    private const string West = "W";

    public static Direction Create(string directionEncoding)
    {
        if (directionEncoding.Equals(North))
        {
            return Direction.CreateNorth();
        }

        if (directionEncoding.Equals(South))
        {
            return Direction.CreateSouth();
        }

        if (directionEncoding.Equals(West))
        {
            return Direction.CreateWest();
        }

        return Direction.CreateEast();
    }
}