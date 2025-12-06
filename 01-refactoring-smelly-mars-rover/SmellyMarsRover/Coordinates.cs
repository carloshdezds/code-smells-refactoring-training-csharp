namespace SmellyMarsRover;

internal record Coordinates
{
    private readonly int _x;
    private readonly int _y;

    public Coordinates(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Coordinates MoveAlongY(int displacement)
    {
        return new Coordinates(_x, _y + displacement);
    }
    public Coordinates MoveAlongX(int displacement)
    {
        return new Coordinates(_x + displacement, _y);
    }
}
