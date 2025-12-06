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
            return new North();
        }

        if (directionEncoding.Equals(SOUTH))
        {
           return new South();
        }

        if (directionEncoding.Equals(WEST))
        {
            return new West();
        }

        return new East();
    }

    public abstract Direction RotateRight();

    public abstract Direction RotateLeft();

    public abstract Coordinates Displace(Coordinates coordinates, int displacement);

    internal record North : Direction
    {
        public override Direction RotateRight()
        {
            return Create(EAST);
        }

        public override Direction RotateLeft()
        {
            return Create(WEST);
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
            return Create(WEST);
        }

        public override Direction RotateLeft()
        {
            return Create(EAST);
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
            return Create(NORTH);
        }

        public override Direction RotateLeft()
        {
            return Create(SOUTH);
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
            return Create(SOUTH);
        }

        public override Direction RotateLeft()
        {
            return Create(NORTH);
        }

        public override Coordinates Displace(Coordinates coordinates, int displacement)
        {
            return coordinates.MoveAlongX(displacement);
        }
    }
}
