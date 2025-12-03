using System;

namespace SmellyMarsRover
{
    public class Rover
    {
        private Direction _direction;
        private Coordinates _coordinates;

        public Rover(int x, int y, string direction)
        {
            _direction = Direction.Create(direction);
            _coordinates = new Coordinates(x, y);
        }

        public void Receive(string commandsSequence)
        {
            for (var i = 0; i < commandsSequence.Length; ++i)
            {
                var command = commandsSequence.Substring(i, 1);

                if (command.Equals("r"))
                {
                    _direction = _direction.RotateRight();
                }
                else if (command.Equals("l"))
                {
                    _direction = _direction.RotateLeft();
                }
                else
                {
                    var displacement = -1;

                    if (command.Equals("f"))
                    {
                        displacement = 1;
                    }

                    _coordinates = _direction.Displace(_coordinates, displacement);
                }
            }
        }

        protected bool Equals(Rover other)
        {
            return Equals(_direction, other._direction) && Equals(_coordinates, other._coordinates);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Rover)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, _coordinates);
        }

        public override string ToString()
        {
            return $"{nameof(_direction)}: {_direction}, {nameof(_coordinates)}: {_coordinates}";
        }
    }
}