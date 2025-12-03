namespace SmellyMarsRover
{
    internal record Coordinates(int x, int y)
    {
        public Coordinates MoveAlongY(int displacement)
        {
            return new Coordinates(x, y + displacement);
        }
        public Coordinates MoveAlongX(int displacement)
        {
            return new Coordinates(x + displacement, y);
        }
    }
}