namespace AdventOfCode.Map;

public enum Direction
{
    Up,
    UpRight,
    UpLeft,
    Down,
    DownRight,
    DownLeft,
    Left,
    Right
}

public record Position2D(int X, int Y)
{
    public static Position2D operator -(Position2D a, Position2D b) => new(a.X - b.X, a.Y - b.Y);
    public static Position2D operator +(Position2D a, Position2D b) => new(a.X + b.X, a.Y + b.Y);

    public static Position2D operator *(Position2D a, int b) => new(a.X * b, a.Y * b);
    public static Position2D operator /(Position2D a, int b) => new(a.X / b, a.Y / b);

    public Position2D Move(Direction direction) => direction switch
    {
        Direction.Up => this + new Position2D(0, -1),
        Direction.Down => this + new Position2D(0, 1),
        Direction.Left => this + new Position2D(-1, 0),
        Direction.Right => this + new Position2D(1, 0),
        Direction.UpLeft => this + new Position2D(-1, -1),
        Direction.UpRight => this + new Position2D(1, -1),
        Direction.DownLeft => this + new Position2D(-1, 1),
        Direction.DownRight => this + new Position2D(1, 1),
        _ => throw new NotImplementedException(),
    };

    public override string ToString() => $"({X}, {Y})";
}