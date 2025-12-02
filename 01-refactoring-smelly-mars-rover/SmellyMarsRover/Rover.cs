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
                    if (IsFacingNorth())
                    {
                        Direction = "E";
                    }
                    else if (IsFacingSouth())
                    {
                        Direction = "W";
                    }
                    else if (IsFacingWest())
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
                    if (IsFacingNorth())
                    {
                        Direction = "W";
                    }
                    else if (IsFacingSouth())
                    {
                        Direction = "E";
                    }
                    else if (IsFacingWest())
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

                    if (IsFacingNorth())
                    {
                        _y += displacement;
                    }
                    else if (IsFacingSouth())
                    {
                        _y -= displacement;
                    }
                    else if (IsFacingWest())
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

        private bool IsFacingWest() {
            return _direction.Value.Equals("W");
        }

        private bool IsFacingSouth() {
            return _direction.Value.Equals("S");
        }

        private bool IsFacingNorth() {
            return _direction.Value.Equals("N");
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

    struct Direction {
        private readonly string value;
        public string Value => value;
        
        public Direction(string value) {
            this.value = value;
        }
    }
}