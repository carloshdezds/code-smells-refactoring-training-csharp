using System;

namespace SmellyMarsRover
{
    public class Rover
    {
        private int _y;
        private int _x;
        private Direction _direction;

        public Rover(int x, int y, string direction)
        {
            Direction = direction;
            _y = y;
            _x = x;
        }

        private string Direction {
            set => _direction = new Direction(value);
        }

        public void Receive(string commandsSequence)
        {
            for (var i = 0; i < commandsSequence.Length; ++i)
            {
                var command = commandsSequence.Substring(i, 1);

                if (command.Equals("r"))
                {
                    // Rotate Rover Right
                    if (_direction.IsFacingNorth())
                    {
                        Direction = "E";
                    }
                    else if (_direction.IsFacingSouth())
                    {
                        Direction = "W";
                    }
                    else if (_direction.IsFacingWest())
                    {
                        Direction = "N";
                    }
                    else
                    {
                        Direction = "S";
                    }
                }
                else if (command.Equals("l"))
                {
                    // Rotate Rover Left
                    if (_direction.IsFacingNorth())
                    {
                        Direction = "W";
                    }
                    else if (_direction.IsFacingSouth())
                    {
                        Direction = "E";
                    }
                    else if (_direction.IsFacingWest())
                    {
                        Direction = "S";
                    }
                    else
                    {
                        Direction = "N";
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
                        _y += displacement;
                    }
                    else if (_direction.IsFacingSouth())
                    {
                        _y -= displacement;
                    }
                    else if (_direction.IsFacingWest())
                    {
                        _x -= displacement;
                    }
                    else
                    {
                        _x += displacement;
                    }
                }
            }
        }

        protected bool Equals(Rover other) {
            return _y == other._y && _x == other._x && _direction.Equals(other._direction);
        }

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (obj.GetType() != GetType()) {
                return false;
            }

            return Equals((Rover)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_y, _x, _direction);
        }

        public override string ToString() {
            return $"{nameof(_y)}: {_y}, {nameof(_x)}: {_x}, {nameof(_direction)}: {_direction}";
        }
    }

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
    }
}