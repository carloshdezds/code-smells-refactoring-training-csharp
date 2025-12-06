namespace SmellyMarsRover;

internal abstract record Direction
{
    private const string NORTH = "N";
    private const string SOUTH = "S";
    private const string WEST = "W";
    private const string EAST = "E";

    public static Direction Create(string directionEncoding)
    {
        if (directionEncoding.Equals(NORTH))
        {
            return CreateNorth();
        }

        if (directionEncoding.Equals(SOUTH))
        {
           return CreateSouth();
        }

        if (directionEncoding.Equals(WEST))
        {
            return CreateWest();
        }

        return CreateEast();
    }

    private static Direction CreateNorth()
    {
        return new North();
    }

    private static Direction CreateSouth()
    {
        return new South();
    }

    private static Direction CreateWest()
    {
        return new West();
    }

    private static Direction CreateEast()
    {
        return new East();
    }

    public abstract Direction RotateRight();

    public abstract Direction RotateLeft();

    public abstract Coordinates Displace(Coordinates coordinates, int displacement);

    internal record North : Direction
    {
        public override Direction RotateRight()
        {
            return CreateEast();
        }

        public override Direction RotateLeft()
        {
            return CreateWest();
        }

        public override Coordinates Displace(Coordinates coordinates, int displacement)
        {
            return coordinates.MoveAlongY(displacement);
        }
    }

    internal record South : Direction
    {
        public override Direction RotateRight()
        {
            return CreateWest();
        }

        public override Direction RotateLeft()
        {
            return CreateEast();
        }

        public override Coordinates Displace(Coordinates coordinates, int displacement)
        {
            return coordinates.MoveAlongY(-displacement);
        }
    }

    internal record West : Direction
    {
        public override Direction RotateRight()
        {
            return CreateNorth();
        }

        public override Direction RotateLeft()
        {
            return CreateSouth();
        }

        public override Coordinates Displace(Coordinates coordinates, int displacement)
        {
            return coordinates.MoveAlongX(-displacement);
        }
    }

    internal record East : Direction
    {
        public override Direction RotateRight()
        {
            return CreateSouth();
        }

        public override Direction RotateLeft()
        {
            return CreateNorth();
        }

        public override Coordinates Displace(Coordinates coordinates, int displacement)
        {
            return coordinates.MoveAlongX(displacement);
        }
    }
}
