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
                    // Rotate Rover Left
                    if (_direction.IsFacingNorth())
                    {
                        _direction = Direction.Create("W");
                    }
                    else if (_direction.IsFacingSouth())
                    {
                        _direction = Direction.Create("E");
                    }
                    else if (_direction.IsFacingWest())
                    {
                        _direction = Direction.Create("S");
                    }
                    else
                    {
                        _direction = Direction.Create("N");
                    }
                }
                else
                {
                    // Displace Rover
                    var displacement1 = -1;

                    if (command.Equals("f"))
                    {
                        displacement1 = 1;
                    }

                    var displacement = displacement1;

                    if (_direction.IsFacingNorth())
                    {
                        _coordinates = _coordinates.MoveAlongY(displacement);
                    }
                    else if (_direction.IsFacingSouth())
                    {
                        _coordinates = _coordinates.MoveAlongY(-displacement);
                    }
                    else if (_direction.IsFacingWest())
                    {
                        _coordinates = _coordinates.MoveAlongX(-displacement);
                    }
                    else
                    {
                        _coordinates = _coordinates.MoveAlongX(displacement);
                    }
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