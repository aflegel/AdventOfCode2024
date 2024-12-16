
namespace AdventOfCode;

public class Day06(string input) : IAdventDay
{
	private char[,] InputArray { get; } = input.Split("\n").To2DArray();

	public string Part1()
	{
		var current = FindStart();
		var currentDirection = Direction.Up;

		var set = Traverse(InputArray, current, currentDirection);
		return set.Select(s => s.Item1).Distinct().Count().ToString();
	}

	private static Direction Rotate(Direction direction) => direction switch
	{
		Direction.Up => Direction.Right,
		Direction.Down => Direction.Left,
		Direction.Left => Direction.Up,
		Direction.Right => Direction.Down,
		_ => throw new NotImplementedException()
	};

	private Position2D FindStart()
	{
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				if (InputArray[i, j] == '^')
					return new(i, j);
			}
		}
		throw new Exception("No position found");
	}

	public string Part2()
	{
		var start = FindStart();
		var current = start;
		var currentDirection = Direction.Up;

		var set = new HashSet<Position2D>();

		while (true)
		{
			var next = current.Move(currentDirection);
			if (InputArray.OutOfBounds(next))
				break;

			for (var i = 0; i < 2; i++)
			{
				if (InputArray[next.X, next.Y] == '#')
				{
					currentDirection = Rotate(currentDirection);
					next = current.Move(currentDirection);
				}
				else
					break;
			}
			if (next != start)
			{
				var blah = (char[,])InputArray.Clone();
				blah[next.X, next.Y] = '#';

				if (Traverse(blah, start, Direction.Up).Count == 0)
				{
					set.Add(next);
				}
			}
			current = next;
		}

		return set.Count.ToString();
	}

	private static HashSet<(Position2D, Direction)> Traverse(char[,] input, Position2D current, Direction direction)
	{
		var set = new HashSet<(Position2D, Direction)>();

		while (true)
		{
			if (set.Contains((current, direction)))
				return [];

			set.Add((current, direction));

			var next = current.Move(direction);

			if (input.OutOfBounds(next))
				return set;

			for (var i = 0; i < 2; i++)
			{
				if (input[next.X, next.Y] == '#')
				{
					direction = Rotate(direction);
					next = current.Move(direction);
				}
				else
					break;
			}

			current = next;
		}
	}
}