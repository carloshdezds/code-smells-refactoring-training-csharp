using System;

namespace SmellyMarsRover
{
    internal abstract record Direction(string Value)
    {
        private const string NORTH = "N";
        private const string SOUTH = "S";
        private const string WEST = "W";
        private const string EAST = "E";

        public bool IsFacingWest()
        {
            return Value.Equals(WEST);
        }

        public bool IsFacingSouth()
        {
            return Value.Equals(SOUTH);
        }

        public bool IsFacingNorth()
        {
            return Value.Equals(NORTH);
        }

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

        internal record North() : Direction(NORTH)
        {
            public override Direction RotateRight()
            {
                return Create(EAST);
            }
        }

        internal record South() : Direction(SOUTH)
        {
            public override Direction RotateRight()
            {
                return Create(WEST);
            }
        }

        internal record West() : Direction(WEST)
        {
            public override Direction RotateRight()
            {
                return Create(NORTH);
            }
        }

        internal record East() : Direction(EAST)
        {
            public override Direction RotateRight()
            {
                return Create(SOUTH);
            }
        }
    }
}